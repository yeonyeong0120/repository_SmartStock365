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
    public class CustomerRuleRepository
    {
        private string connstr = ConfigurationManager.ConnectionStrings["MyStockDbConnection"].ConnectionString;

        // 모든 규칙 목록 가져오기 (JOIN 포함)
        public List<CustomerRule> GetAllCustomerRules()
        {
            List<CustomerRule> rules = new List<CustomerRule>();
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                // 3개 테이블을 JOIN 해서 규칙과 함께 거래처명, 상품명을 가져옴
                string sql = @"SELECT 
                                r.RuleId, r.CustomerId, r.ProductId, r.Required_REDW_days,
                                c.CustomerName, 
                                p.ProductName
                             FROM CustomerRules r
                             JOIN Customers c ON r.CustomerId = c.CustomerId
                             JOIN Products p ON r.ProductId = p.ProductId";
                SqlCommand cmd = new SqlCommand(sql, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CustomerRule rule = new CustomerRule();
                        rule.RuleId = (int)reader["RuleId"];
                        rule.CustomerId = (int)reader["CustomerId"];
                        rule.ProductId = (int)reader["ProductId"];
                        rule.Required_REDW_days = (int)reader["Required_REDW_days"];
                        rule.CustomerName = (string)reader["CustomerName"];
                        rule.ProductName = (string)reader["ProductName"];

                        rules.Add(rule);
                    }
                }
            }
            return rules;
        }

        public bool AddNewRule(CustomerRule rule)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                string sql = @"INSERT INTO CustomerRules (CustomerId, ProductId, Required_REDW_days) 
                       VALUES (@CId, @PId, @Days)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@CId", rule.CustomerId);
                cmd.Parameters.AddWithValue("@PId", rule.ProductId);
                cmd.Parameters.AddWithValue("@Days", rule.Required_REDW_days);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public bool UpdateRule(CustomerRule rule)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                string sql = @"UPDATE CustomerRules 
                       SET CustomerId=@CId, ProductId=@PId, Required_REDW_days=@Days 
                       WHERE RuleId=@RId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@CId", rule.CustomerId);
                cmd.Parameters.AddWithValue("@PId", rule.ProductId);
                cmd.Parameters.AddWithValue("@Days", rule.Required_REDW_days);
                cmd.Parameters.AddWithValue("@RId", rule.RuleId);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
        // 규칙 삭제
        public bool DeleteRule(int ruleId)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                string sql = "DELETE FROM CustomerRules WHERE RuleId=@RId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@RId", ruleId);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // 출고처가 요구하는 상품 입고기준일 가져오기
        public int GetRedwDays(int customerId, int productId)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                string sql = @"SELECT Required_REDW_days 
                       FROM CustomerRules 
                       WHERE CustomerId = @CId AND ProductId = @PId";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@CId", customerId);
                cmd.Parameters.AddWithValue("@PId", productId);

                object result = cmd.ExecuteScalar();

                // 규칙이 있으면 날짜 반환.... 없으면 -1
                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return -1;
                }
            }
        }

        // 계속 추갛예정
    }
}
