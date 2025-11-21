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
    public class UserRepository
    {
        private string connstr = ConfigurationManager.ConnectionStrings["MyStockDbConnection"].ConnectionString;

        // 로그인 확인 (가장 많이 쓰일 기능)
        // 사용자 이름으로 사용자 정보 가져오기
        public User GetUserByUsername(string username)
        {
            User user = null; // 못찾으면 null 반환
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                // SQL Injection 공격을 방지하기 위해 파라미터를 사용
                string sql = "SELECT UserId, Username, PasswordHash, Role FROM Users WHERE Username = @Username";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Username", username);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) // 데이터가 있으면 한 줄만 읽음
                    {
                        user = new User();
                        user.UserId = (int)reader["UserId"];
                        user.Username = (string)reader["Username"];
                        user.PasswordHash = (string)reader["PasswordHash"];
                        user.Role = (string)reader["Role"];
                    }
                }
            }
            return user;
        }

        // (여기에 나중에 AddNewUser, GetAllUsers 등 CRUD 추가)
    }
}
