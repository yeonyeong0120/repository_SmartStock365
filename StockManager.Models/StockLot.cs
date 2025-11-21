using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.Models
{
    public class StockLot
    {
        public int LotId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PurchasePrice { get; set; } // 소수점을 포함할 수 있으므로 decimal
        public DateTime ExpirationDate { get; set; } // 유통기한
        public int SupplierId { get; set; } // 매입처 ID
        public string SupplierName { get; set; } // 매입처 이름
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
    }
}
