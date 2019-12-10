using QLDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDD.Code
{
    public class MenuDao
    {
        QUANLYDIADIEMEntities db = null;
        public MenuDao()
        {
            db = new QUANLYDIADIEMEntities();
        }

        public List<KHACHHANG> ListByGroupId(string groupId)
        {
            return db.KHACHHANGs.Where(x => x.KHACHHANG_ID == groupId).OrderBy(x => x.KHACHHANG_TEN).ToList();
        }
    }
}