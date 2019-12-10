using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDD.Common
{
    [Serializable]
    public class AdminLogin
    {
        public long AdminID { set; get; }
        public string UserName { set; get; }
        public string GroupID { set; get; }
    }
}