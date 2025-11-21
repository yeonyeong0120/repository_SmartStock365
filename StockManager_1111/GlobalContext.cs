using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockManager.Models;

namespace StockManager_1111
{
    // 로그인 정보 한곳에서 저장
    public static class GlobalContext
    {
        // 지금 로그인한 사람 저장...
        public static User CurrentUser { get; set; }
    }
}
