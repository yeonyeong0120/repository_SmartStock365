using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockManager.Models;
using System.Configuration;
using System.Data.SqlClient;

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

        // 여기에 나중에 ㄱ기능추가
    }
}
