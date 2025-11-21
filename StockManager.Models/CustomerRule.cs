using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.Models
{
    public class CustomerRule
    {
        public int RuleId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Required_REDW_days { get; set; }

        // (JOIN해서 가져올 이름들)
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
    }
}
