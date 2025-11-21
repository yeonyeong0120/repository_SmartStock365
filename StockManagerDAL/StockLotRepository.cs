using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockManager.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data; // SqlDbType 사용하기 위해...

namespace StockManagerDAL
{
    public class StockLotRepository
    {
        private string connstr = ConfigurationManager.ConnectionStrings["MyStockDbConnection"].ConnectionString;

        // 새 재고 입고
        public int AddNewStockLot(StockLot lot)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                string sql = @"INSERT INTO StockLots 
                                (ProductId, Quantity, PurchasePrice, ExpirationDate, SupplierId) 
                               VALUES 
                                (@ProductId, @Quantity, @PurchasePrice, @ExpirationDate, @SupplierId);
                                SELECT CAST(SCOPE_IDENTITY() AS int);";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ProductId", lot.ProductId);
                cmd.Parameters.AddWithValue("@Quantity", lot.Quantity);
                cmd.Parameters.AddWithValue("@PurchasePrice", lot.PurchasePrice);
                cmd.Parameters.Add("@ExpirationDate", SqlDbType.Date).Value = lot.ExpirationDate.Date;

                // 매입처 아이디 조회하기
                if (lot.SupplierId == 0)
                    cmd.Parameters.AddWithValue("@SupplierId", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@SupplierId", lot.SupplierId);

                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0; // 숫자로
            }
        }

        // 보유중인 재고 보여주기
        public List<StockLot> GetAllStockLots()
        {
            List<StockLot> stockLots = new List<StockLot>();
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();

                string sql = @"SELECT 
                                sl.LotId, sl.ProductId, sl.Quantity, 
                                sl.PurchasePrice, sl.ExpirationDate, sl.SupplierId,
                                p.ProductName,
                                c.CustomerName AS SupplierName,
                                cat.CategoryName  -- 여기서 cat을 씀
                            FROM StockLots sl
                            JOIN Products p ON sl.ProductId = p.ProductId
                            LEFT JOIN Customers c ON sl.SupplierId = c.CustomerId
                            JOIN Categories cat ON p.CategoryId = cat.CategoryId -- ★ 여기서 cat을 정의함!
                            WHERE sl.Quantity > 0"; // 재고가 0보다 큰 것만 조회?

                SqlCommand cmd = new SqlCommand(sql, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        StockLot lot = new StockLot();
                        lot.LotId = (int)reader["LotId"];
                        lot.ProductId = (int)reader["ProductId"];
                        lot.Quantity = (int)reader["Quantity"];
                        lot.PurchasePrice = (decimal)reader["PurchasePrice"];
                        lot.ExpirationDate = (DateTime)reader["ExpirationDate"];
                        lot.ProductName = (string)reader["ProductName"]; // 조인으로 가져온 상품명
                        lot.SupplierId = reader["SupplierId"] == DBNull.Value ? 0 : (int)reader["SupplierId"];
                        lot.SupplierName = reader["SupplierName"] == DBNull.Value ? "-" : (string)reader["SupplierName"];
                        lot.CategoryName = reader["CategoryName"].ToString();

                        stockLots.Add(lot);
                    }
                }
            }
            return stockLots;
        }

        public List<StockLot> GetValidStockLots(int productId, int minDays)
        {
            List<StockLot> stockLots = new List<StockLot>();
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();

                // DATEDIFF(day, GETDATE(), ExpirationDate) <- 이게 기본형식
                string sql = @"SELECT 
                         sl.LotId, sl.ProductId, sl.Quantity, 
                         sl.PurchasePrice, sl.ExpirationDate, sl.SupplierId,
                         p.ProductName,
                         c.CustomerName AS SupplierName,
                         cat.CategoryName
                     FROM StockLots sl
                     JOIN Products p ON sl.ProductId = p.ProductId
                     LEFT JOIN Customers c ON sl.SupplierId = c.CustomerId 
                     JOIN Categories cat ON p.CategoryId = cat.CategoryId 
                     WHERE sl.ProductId = @PId 
                       AND sl.Quantity > 0
                       AND DATEDIFF(day, GETDATE(), sl.ExpirationDate) >= @MinDays
                     ORDER BY sl.ExpirationDate ASC"; // 유통기한 짧은 순으루ㅠ 정렬

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@PId", productId);
                cmd.Parameters.AddWithValue("@MinDays", minDays);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        StockLot lot = new StockLot();
                        lot.LotId = (int)reader["LotId"];
                        lot.ProductId = (int)reader["ProductId"];
                        lot.Quantity = (int)reader["Quantity"];
                        lot.PurchasePrice = (decimal)reader["PurchasePrice"];
                        lot.ExpirationDate = (DateTime)reader["ExpirationDate"];
                        lot.ProductName = (string)reader["ProductName"];
                        lot.SupplierId = reader["SupplierId"] == DBNull.Value ? 0 : (int)reader["SupplierId"];
                        lot.SupplierName = reader["SupplierName"] == DBNull.Value ? "-" : (string)reader["SupplierName"];
                        lot.CategoryName = reader["CategoryName"].ToString();

                        stockLots.Add(lot);
                    }
                }
            }
            return stockLots;
        }

        // for 출고버튼 // 기존 수량 - 출고 수량
        public bool DecreaseStockQuantity(int lotId, int amount)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                string sql = "UPDATE StockLots SET Quantity = Quantity - @Amount WHERE LotId = @LotId";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Amount", amount);
                cmd.Parameters.AddWithValue("@LotId", lotId);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public DataTable GetLowStockProducts()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                string sql = @"
            SELECT p.ProductName, SUM(ISNULL(sl.Quantity, 0)) as CurrentStock, p.SafetyStock
            FROM Products p
            LEFT JOIN StockLots sl ON p.ProductId = sl.ProductId
            GROUP BY p.ProductId, p.ProductName, p.SafetyStock
            HAVING SUM(ISNULL(sl.Quantity, 0)) < p.SafetyStock";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);
            }
            return dt;
        }

        // 추가에정
    }  // 클래스
}
