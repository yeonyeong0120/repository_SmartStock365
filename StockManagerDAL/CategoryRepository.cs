using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockManager.Models; // 데이터는 여기서 관리 // 모르는 클래스 있으면 여기서 찾아보셈
using System.Data.SqlClient; // SQL Server 연결 도구
using System.Configuration; // connstr을 다른 파일로 관리할 수 있도록

namespace StockManagerDAL
{
    public class CategoryRepository
    {
        // DB 연결 App.config로 빼놨음
        private string connstr 
            = ConfigurationManager.ConnectionStrings["MyStockDbConnection"].ConnectionString;

        // "모든 카테고리 목록을 C# 바구니(List)에 담아서 돌려줘" 라는 기능(메서드)
        // 이 리스트는 Category 타입의 객체들을 담을것임!
        public List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();

            // using 구문은 DB 연결을 자동으로 열고 닫아줌 // 임시 출입증?
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open(); // DB 연결 열기
                // Categories 테이블에서 컬럼 두개 정보 가져오는 sql문
                string sql = "SELECT CategoryId, CategoryName FROM Categories ORDER BY CategoryId";
                SqlCommand cmd = new SqlCommand(sql, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // db에 데이터가 있으면(true) 계쏙 실행
                    while (reader.Read()) // DB에서 한 줄씩 읽어옴
                    {
                        // 일단 새 바구니(Category)객체를 만들고
                        Category category = new Category();

                        // DB에서 읽은 데이터를 C# 바구니에 담습니다.
                        category.CategoryId = (int)reader["CategoryId"];
                        category.CategoryName = (string)reader["CategoryName"];

                        // 완성된 바구니를 전체 목록에 추가
                        categories.Add(category);
                    }
                }
            }

            return categories; // 전체 목록 반환
        }

        // 카테고리 DB insert 함수
        public bool AddNewCategory(Category category)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                string sql = "INSERT INTO Categories (CategoryName) VALUES (@CategoryName)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);

                // INSERT 실행 및 영향받은 행의 수 반환
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        // 카테고리 DB 업데이트 메서드
        public bool UpdateCategory(Category category)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                string sql = "UPDATE Categories SET CategoryName = @CategoryName WHERE CategoryId = @CategoryId";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                cmd.Parameters.AddWithValue("@CategoryId", category.CategoryId);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        // 카테고리 DB 삭제
        public bool DeleteCategory(int categoryId)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                string sql = "DELETE FROM Categories WHERE CategoryId = @CategoryId";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@CategoryId", categoryId);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

    }
}
