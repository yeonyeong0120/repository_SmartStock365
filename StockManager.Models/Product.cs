using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.Models
{
    public class Product
    {
        // Products 테이블의 컬럼들
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string StorageType { get; set; }
        public string Unit { get; set; }
        public int SafetyStock { get; set; }
        public int SellingPrice { get; set; } // 판매가 추가

        // CategoryId로 실제 CategoryName을 함께 조회해서 담아둘 때 사용
        // (JOIN 쿼리를 사용할 때 필요함)
        public string CategoryName { get; set; }
    }
}
