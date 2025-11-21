using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int LotId { get; set; }
        public int UserId { get; set; }
        public string TransactionType { get; set; } // "IN", "OUT", "DISPOSE"
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; }

        // (JOIN해서 가져올 정보들)
        public string Username { get; set; }
        public string ProductName { get; set; }
    }
}
