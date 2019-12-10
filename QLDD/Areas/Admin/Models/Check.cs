using QLDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDD.Areas.Admin.Models
{
    public class Check
    {
        private QUANLYDIADIEMEntities db = new QUANLYDIADIEMEntities();

        public bool CheckHinhDD(int dd, string ha)
        {
            return db.HINHDIADIEMs.Count(n => n.DIADIEM_ID == dd && n.HDD_LINK.ToString()== ha) > 0;
        }

        public bool CheckHinhSP(int dd, string ha)
        {
            return db.HINHSANPHAMs.Count(n => n.SANPHAM_ID == dd && n.HSP_LINK.ToString() == ha) > 0;
        }
    }
}