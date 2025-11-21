using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }  // id로 사용
        public string PasswordHash { get; set; }
        public string Role { get; set; }
    }
}
