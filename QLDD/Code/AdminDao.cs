using QLDD.Common;
using QLDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDD.Code
{
    public class AdminDao
    {
        private QUANLYDIADIEMEntities db = null;

        public AdminDao()
        {
            db = new QUANLYDIADIEMEntities();
        }

        public string Insert(NHANVIEN entity)
        {
            db.NHANVIENs.Add(entity);
            db.SaveChanges();
            return entity.NHANVIEN_ID;
        }

        public bool Update(NHANVIEN entity)
        {
            try
            {
                var user = db.NHANVIENs.Find(entity.NHANVIEN_ID);
                user.NHANVIEN_ID = entity.NHANVIEN_ID;
                if (!string.IsNullOrEmpty(entity.NHANVIEN_SDT))
                {
                    user.NHANVIEN_SDT = entity.NHANVIEN_SDT;
                }

                user.NHANVIEN_EMAIL = entity.NHANVIEN_EMAIL;


                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public NHANVIEN GetById(string KH_TK)
        {
            return db.NHANVIENs.SingleOrDefault(x => x.NHANVIEN_ID == KH_TK);
        }
        public int Login(string userName, string passWord, bool isLoginAdmin = false)
        {
            var result = db.NHANVIENs.SingleOrDefault(x => x.NHANVIEN_ID == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (isLoginAdmin == true)
                {
                    if (result.NHANVIEN_ID == CommonConstants.USER_SESSION)
                    {
                        if (result.NHANVIEN_SDT == passWord)
                        {
                            return 1;
                        }
                        else
                        {
                            return -2;
                        }
                    }
                    else
                    {
                        return -3;
                    }
                }
                else
                {
                    if (result.NHANVIEN_SDT == passWord)
                        return 1;
                    else
                        return -2;

                }
            }
        }

        public bool CheckNV(string TenKH)
        {
            return db.NHANVIENs.Count(x => x.NHANVIEN_ID == TenKH) > 0;
        }

        public bool CheckEmail(string Email)
        {
            return db.NHANVIENs.Count(x => x.NHANVIEN_EMAIL == Email) > 0;
        }
    }
}