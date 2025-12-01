using StockManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration; // App.config 읽는 도구
using System.Data.SqlClient; // SQL Server 연결 도구

namespace StockManagerDAL
{
    public class ProductRepository
    {
        // App.config 파일에서 "MyStockDbConnection" 이름표를 가진 연결 문자열을 찾아옵니다.
        private string connstr = ConfigurationManager.ConnectionStrings["MyStockDbConnection"].ConnectionString;

        // "모든 상품 목록을 C# 바구니(List)에 담아서 돌려줘" 라는 기능(메서드)
        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();

                // Products 테이블과 Categories 테이블을 JOIN 해서 모든 열 가져오기
                string sql = @"SELECT 
                                p.ProductId, p.ProductName,
                                c.CategoryName,
                                p.StorageType, p.Unit, p.SafetyStock,
                                p.SellingPrice,
                                p.CategoryId
                             FROM Products p
                             JOIN Categories c ON p.CategoryId = c.CategoryId";

                SqlCommand cmd = new SqlCommand(sql, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // 새 'Product 바구니'를 만들고
                        Product product = new Product();

                        // DB에서 읽은 데이터를 C#에 담기
                        product.ProductId = (int)reader["ProductId"];
                        product.ProductName = (string)reader["ProductName"];
                        product.CategoryId = (int)reader["CategoryId"];
                        product.StorageType = reader["StorageType"] == DBNull.Value ? null : (string)reader["StorageType"];
                        product.Unit = reader["Unit"] == DBNull.Value ? null : (string)reader["Unit"];
                        product.SafetyStock = (int)reader["SafetyStock"];
                        product.CategoryName = (string)reader["CategoryName"]; // JOIN해온 카테고리 이름
                        product.SellingPrice = (int)reader["SellingPrice"];

                        // 완성된 바구니를 전체 목록에 추가
                        products.Add(product);
                    }
                }
            }

            return products; // 전체 목록 반환
        }

        public bool AddNewProduct(Product product)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();

                // DB에 Insert 해주는 쿼리
                string sql = @"INSERT INTO Products 
                         (ProductName, CategoryId, StorageType, Unit, SafetyStock, SellingPrice) 
                       VALUES 
                         (@ProductName, @CategoryId, @StorageType, @Unit, @SafetyStock, @Price)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                cmd.Parameters.AddWithValue("@StorageType", product.StorageType);
                cmd.Parameters.AddWithValue("@Unit", product.Unit);
                cmd.Parameters.AddWithValue("@SafetyStock", product.SafetyStock);
                cmd.Parameters.AddWithValue("@Price", product.SellingPrice);

                // 영향받은 쿼리 수 int값으로 보여주기
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // 상품 수정 (UPDATE)
        public bool UpdateProduct(Product product)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                string sql = @"UPDATE Products 
                       SET ProductName=@ProductName, CategoryId=@CategoryId, 
                           StorageType=@StorageType, Unit=@Unit, SafetyStock=@SafetyStock, SellingPrice=@Price
                       WHERE ProductId=@ProductId";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                cmd.Parameters.AddWithValue("@StorageType", product.StorageType);
                cmd.Parameters.AddWithValue("@Unit", product.Unit);
                cmd.Parameters.AddWithValue("@SafetyStock", product.SafetyStock);
                cmd.Parameters.AddWithValue("@ProductId", product.ProductId); // WHERE절 조건
                cmd.Parameters.AddWithValue("@Price", product.SellingPrice);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
        // 상품 삭제 (DELETE)
        public bool DeleteProduct(int productId)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                string sql = "DELETE FROM Products WHERE ProductId = @ProductId";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ProductId", productId);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // 품명 중복 검사 // 공백 상관없
        public bool IsDuplicateProductName(string productName)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM Products WHERE REPLACE(ProductName, ' ', '') = @Name";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Name", productName.Replace(" ", ""));

                int count = (int)cmd.ExecuteScalar();
                return count > 0; // 0보다 크면 이미 있다는 뜻
            }
        }



        // 기능 추가 예쩡
    } // 클래스
}
