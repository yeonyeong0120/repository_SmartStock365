using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.Models
{
    public class Category
    {
        // DB 테이블의 컬럼과 똑같이 속성(Properties) 만들기
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
