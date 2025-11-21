using StockManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace StockManagerDAL
{
    public class CustomerRepository
    {
        private string connstr = ConfigurationManager.ConnectionStrings["MyStockDbConnection"].ConnectionString;

        // 모든 거래처 목록 가져오기
        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                string sql 
                    = "SELECT CustomerId, CustomerName, ContactPerson, PhoneNumber, Address, Notes, CustomerType FROM Customers";
                SqlCommand cmd = new SqlCommand(sql, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer();
                        customer.CustomerId = (int)reader["CustomerId"];
                        customer.CustomerName = (string)reader["CustomerName"];

                        // DB의 NULL 값을 C#의 null로 안전하게 처리
                        customer.ContactPerson = reader["ContactPerson"] == DBNull.Value ? null : (string)reader["ContactPerson"];
                        customer.PhoneNumber = reader["PhoneNumber"] == DBNull.Value ? null : (string)reader["PhoneNumber"];
                        customer.Address = reader["Address"] == DBNull.Value ? null : (string)reader["Address"];
                        customer.Notes = reader["Notes"] == DBNull.Value ? null : (string)reader["Notes"];
                        customer.CustomerType = reader["CustomerType"] == DBNull.Value ? null : (string)reader["CustomerType"];

                        customers.Add(customer);
                    }
                }
            }
            return customers;
        }

        public bool AddNewCustomer(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                string sql = @"INSERT INTO Customers (CustomerName, ContactPerson, PhoneNumber, Address, Notes, CustomerType) 
                       VALUES (@Name, @Person, @Phone, @Address, @Notes, @CustomerType)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Name", customer.CustomerName);
                cmd.Parameters.AddWithValue("@Person", (object)customer.ContactPerson ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Phone", (object)customer.PhoneNumber ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Address", (object)customer.Address ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Notes", (object)customer.Notes ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CustomerType", customer.CustomerType);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
        // 거래처 수정 (Update)
        public bool UpdateCustomer(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                string sql = @"UPDATE Customers 
                       SET CustomerName=@Name, ContactPerson=@Person, PhoneNumber=@Phone, 
                           Address=@Address, Notes=@Notes, CustomerType=@CustomerType
                       WHERE CustomerId=@Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Name", customer.CustomerName);
                cmd.Parameters.AddWithValue("@Person", (object)customer.ContactPerson ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Phone", (object)customer.PhoneNumber ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Address", (object)customer.Address ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Notes", (object)customer.Notes ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Id", customer.CustomerId);
                cmd.Parameters.AddWithValue("@CustomerType", customer.CustomerType);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // 거래처 삭제 (Delete)
        public bool DeleteCustomer(int customerId)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                string sql = "DELETE FROM Customers WHERE CustomerId = @Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", customerId);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // 매입처, 매출처 구분하는 메서듷
        public List<Customer> GetCustomersByType(string customerType)
        {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                // WHERE 조건으로 CustomerType 필터링
                string sql = "SELECT * FROM Customers WHERE CustomerType = @CustomerType";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@CustomerType", customerType);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer();
                        customer.CustomerId = (int)reader["CustomerId"];
                        customer.CustomerName = (string)reader["CustomerName"];
                        customer.ContactPerson = reader["ContactPerson"] == DBNull.Value ? null : (string)reader["ContactPerson"];
                        customer.PhoneNumber = reader["PhoneNumber"] == DBNull.Value ? null : (string)reader["PhoneNumber"];
                        customer.Address = reader["Address"] == DBNull.Value ? null : (string)reader["Address"];
                        customer.Notes = reader["Notes"] == DBNull.Value ? null : (string)reader["Notes"];
                        customer.CustomerType = (string)reader["CustomerType"];
                        customers.Add(customer);
                    }
                }
            }
            return customers;
        }

        // 계쏙 추가예정
    }
}
