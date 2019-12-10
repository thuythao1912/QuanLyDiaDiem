using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QLDD.Models
{
    public class AccountModel
    {
        QUANLYDIADIEMEntities db = new QUANLYDIADIEMEntities();

        public bool Login(string UserName, string Password)
        {
            object[] sqlParas = {
                new SqlParameter("@UserName", UserName),
                new SqlParameter("@Password", Password),
            };

            var res = db.Database.SqlQuery<bool>("Sp_Account_Login @UserName, @Password", sqlParas).SingleOrDefault();
            return res;
        }

        public KHACHHANG GetTK(string tk)
        {
            return db.KHACHHANGs.SingleOrDefault(n => n.KHACHHANG_ID == tk);
        }
    }
}