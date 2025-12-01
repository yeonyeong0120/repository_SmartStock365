using StockManager.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagerDAL
{
    public class TransactionRepository
    {
        private string connstr = ConfigurationManager.ConnectionStrings["MyStockDbConnection"].ConnectionString;
       
        public bool AddNewTransaction(Transaction tx)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                string sql = @"INSERT INTO Transactions (LotId, UserId, TransactionType, Quantity, TransactionDate) 
                             VALUES (@LotId, @UserId, @TransactionType, @Quantity, @TransactionDate)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@LotId", tx.LotId);
                cmd.Parameters.AddWithValue("@UserId", tx.UserId);
                cmd.Parameters.AddWithValue("@TransactionType", tx.TransactionType);
                cmd.Parameters.AddWithValue("@Quantity", tx.Quantity);
                cmd.Parameters.AddWithValue("@TransactionDate", tx.TransactionDate);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public List<Transaction> GetAllTransactions()
        {
            List<Transaction> list = new List<Transaction>();
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                // Users 테이블도 조인해주기 (누가 했는지 알 수 있게)
                string sql = @"
            SELECT 
                t.TransactionId, t.TransactionDate, t.TransactionType, 
                t.Quantity, t.LotId, 
                p.ProductName, 
                u.Username
            FROM Transactions t
            JOIN StockLots s ON t.LotId = s.LotId
            JOIN Products p ON s.ProductId = p.ProductId
            LEFT JOIN Users u ON t.UserId = u.UserId
            ORDER BY t.TransactionDate DESC"; // 최신 날짜가 위로 오게

                SqlCommand cmd = new SqlCommand(sql, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Transaction tx = new Transaction();
                        tx.TransactionId = (int)reader["TransactionId"];
                        tx.TransactionDate = (DateTime)reader["TransactionDate"];
                        tx.TransactionType = (string)reader["TransactionType"];
                        tx.Quantity = (int)reader["Quantity"];
                        tx.LotId = (int)reader["LotId"];

                        tx.ProductName = (string)reader["ProductName"];
                        tx.Username = reader["Username"] == DBNull.Value ? "Unknown" : (string)reader["Username"];

                        list.Add(tx);
                    }
                }
            }
            return list;
        }

        // 검색기능 // 일부 검색기능으로 바ㄱㄲㅁ
        public List<Transaction> SearchTransactions(DateTime startDate, DateTime endDate, string type, string productName)
        {
            List<Transaction> list = new List<Transaction>();
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                string sql = @"
            SELECT 
                t.TransactionId, t.TransactionDate, t.TransactionType, 
                t.Quantity, t.LotId, 
                p.ProductName, 
                u.Username
            FROM Transactions t
            JOIN StockLots s ON t.LotId = s.LotId
            JOIN Products p ON s.ProductId = p.ProductId
            LEFT JOIN Users u ON t.UserId = u.UserId
            WHERE t.TransactionDate >= @Start AND t.TransactionDate < @End";

                if (type != "전체")
                {
                    sql += " AND t.TransactionType = @Type";
                }

                if (!string.IsNullOrEmpty(productName))
                {
                    sql += " AND p.ProductName LIKE @ProductName";
                }

                // 최신순
                sql += " ORDER BY t.TransactionDate DESC";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Start", startDate);
                cmd.Parameters.AddWithValue("@End", endDate);    

                if (type != "전체") // 이부분 수정이 필요할듯;;
                {
                    string dbType = type.Contains("IN") ? "IN" : "OUT";
                    cmd.Parameters.AddWithValue("@Type", dbType);
                }

                if (!string.IsNullOrEmpty(productName))
                {
                    cmd.Parameters.AddWithValue("@ProductName", "%" + productName + "%");
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Transaction tx = new Transaction();
                        tx.TransactionId = (int)reader["TransactionId"];
                        tx.TransactionDate = (DateTime)reader["TransactionDate"];
                        tx.TransactionType = (string)reader["TransactionType"];
                        tx.Quantity = (int)reader["Quantity"];
                        tx.LotId = (int)reader["LotId"];
                        tx.ProductName = (string)reader["ProductName"];
                        tx.Username = reader["Username"] == DBNull.Value ? "Unknown" : (string)reader["Username"];
                        list.Add(tx);
                    }
                }
            }
            return list;
        }

        // 기간 조회  // 6개월로 변경
        public Dictionary<string, int> GetTopProducts(DateTime startDate, DateTime endDate)
        {
            var stats = new Dictionary<string, int>();
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                string sql = @"
            SELECT TOP 5 p.ProductName, SUM(t.Quantity) as TotalQty
            FROM Transactions t
            JOIN StockLots s ON t.LotId = s.LotId
            JOIN Products p ON s.ProductId = p.ProductId
            WHERE t.TransactionType = 'OUT' 
              AND t.TransactionDate >= @Start AND t.TransactionDate <= @End
            GROUP BY p.ProductName
            ORDER BY TotalQty DESC"; // 많이 팔린순

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Start", startDate);
                cmd.Parameters.AddWithValue("@End", endDate);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        stats.Add(reader["ProductName"].ToString(), (int)reader["TotalQty"]);
                    }
                }
            }
            return stats;
        }

        // 입출고 로그 전용
        public List<Transaction> GetRecentTransactions()
        {
            List<Transaction> list = new List<Transaction>();
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                string sql = @"
            SELECT TOP 5 
                t.TransactionId, t.TransactionDate, t.TransactionType, t.Quantity, 
                p.ProductName, u.Username
            FROM Transactions t
            JOIN StockLots s ON t.LotId = s.LotId
            JOIN Products p ON s.ProductId = p.ProductId
            LEFT JOIN Users u ON t.UserId = u.UserId
            ORDER BY t.TransactionDate DESC"; // 최신순

                SqlCommand cmd = new SqlCommand(sql, conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Transaction tx = new Transaction();
                        tx.TransactionDate = (DateTime)reader["TransactionDate"];
                        tx.TransactionType = (string)reader["TransactionType"];
                        tx.Quantity = (int)reader["Quantity"];
                        tx.ProductName = (string)reader["ProductName"];
                        tx.Username = reader["Username"] == DBNull.Value ? "-" : (string)reader["Username"];
                        list.Add(tx);
                    }
                }
            }
            return list;
        }

        // 매입매출 현황용...
        // dgv용 검색 조건에 맞는 입출고 가져오기
        public DataTable SearchTransactions(DateTime start, DateTime end, string keyword)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                string sql = @"
            SELECT 
                t.TransactionDate,
                t.TransactionType,
                p.ProductName,
                t.Quantity,
                CASE 
                    WHEN t.TransactionType = 'IN' THEN s.PurchasePrice 
                    WHEN t.TransactionType = 'OUT' THEN p.SellingPrice 
                    ELSE 0 
                END AS Price,
                CASE 
                    WHEN t.TransactionType = 'IN' THEN s.PurchasePrice * t.Quantity
                    WHEN t.TransactionType = 'OUT' THEN p.SellingPrice * t.Quantity
                    ELSE 0 
                END AS TotalAmount,
                c.CustomerName
            FROM Transactions t
            JOIN StockLots s ON t.LotId = s.LotId
            JOIN Products p ON s.ProductId = p.ProductId
            LEFT JOIN Customers c ON s.SupplierId = c.CustomerId
            WHERE (t.TransactionDate BETWEEN @Start AND @End)
              AND (p.ProductName LIKE '%' + @Keyword + '%') -- 검색어 필터
            ORDER BY t.TransactionDate DESC";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Start", start.Date);
                cmd.Parameters.AddWithValue("@End", end.Date.AddDays(1).AddSeconds(-1));
                cmd.Parameters.AddWithValue("@Keyword", keyword);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // 파이차트용 // 매출만
        public Dictionary<string, long> GetSalesShare(DateTime start, DateTime end, string keyword)
        {
            var result = new Dictionary<string, long>();
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                string sql = @"
            SELECT 
                p.ProductName,
                SUM(t.Quantity * p.SellingPrice) as TotalSales
            FROM Transactions t
            JOIN StockLots s ON t.LotId = s.LotId
            JOIN Products p ON s.ProductId = p.ProductId
            WHERE t.TransactionType = 'OUT' 
              AND (t.TransactionDate BETWEEN @Start AND @End)
              AND (p.ProductName LIKE '%' + @Keyword + '%')
            GROUP BY p.ProductName
            ORDER BY TotalSales DESC"; // 매출 높은 순
        
        SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Start", start.Date);
                cmd.Parameters.AddWithValue("@End", end.Date.AddDays(1).AddSeconds(-1));
                cmd.Parameters.AddWithValue("@Keyword", keyword);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = reader["ProductName"].ToString();
                        long sales = Convert.ToInt64(reader["TotalSales"]);
                        result.Add(name, sales);
                    }
                }
            }
            return result;
        }

        // 월별 계산 // 0=매출 // 1=매입 / 2=순수익
        public long[] GetMonthlySummary()
        {
            long[] result = new long[3]; // 배열=[매출, 매입, 순수익]

            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();

                // 이 달 1일 ~ 현재까지
                DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime end = DateTime.Now;

                string sql = @"
                SELECT 
                    SUM(CASE WHEN t.TransactionType = 'OUT' THEN t.Quantity * p.SellingPrice ELSE 0 END) as TotalSales,
                    SUM(CASE WHEN t.TransactionType = 'IN' THEN t.Quantity * s.PurchasePrice ELSE 0 END) as TotalCost
                FROM Transactions t
                JOIN StockLots s ON t.LotId = s.LotId
                JOIN Products p ON s.ProductId = p.ProductId
                WHERE t.TransactionDate BETWEEN @Start AND @End";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Start", start);
                cmd.Parameters.AddWithValue("@End", end);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        long sales = reader["TotalSales"] == DBNull.Value ? 0 : Convert.ToInt64(reader["TotalSales"]);
                        long cost = reader["TotalCost"] == DBNull.Value ? 0 : Convert.ToInt64(reader["TotalCost"]);

                        result[0] = sales;       // 매출
                        result[1] = cost;        // 매입
                        result[2] = sales - cost; // 순수익
                    }
                }
            }
            return result;
        }

        // 꺾은선용.... 최근 6개월 매출만
        public Dictionary<string, long> GetMonthlyTrend()
        {
            var result = new Dictionary<string, long>();

            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                string sql = @"
                SELECT 
                    FORMAT(t.TransactionDate, 'yyyy-MM') as Month,
                    SUM(t.Quantity * p.SellingPrice) as MonthlySales
                FROM Transactions t
                JOIN StockLots s ON t.LotId = s.LotId
                JOIN Products p ON s.ProductId = p.ProductId
                WHERE t.TransactionType = 'OUT' 
                    AND t.TransactionDate >= DATEADD(month, -6, GETDATE())
                GROUP BY FORMAT(t.TransactionDate, 'yyyy-MM')
                ORDER BY Month ASC";

                SqlCommand cmd = new SqlCommand(sql, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string month = reader["Month"].ToString();
                        long sales = Convert.ToInt64(reader["MonthlySales"]);
                        result.Add(month, sales);
                    }
                }
            }
            return result;
        }


        // 여기에 나중에 ㄱ기능추가
    }
}
