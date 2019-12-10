using QLDD.Common;
using QLDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDD.Code
{
    public class UserDao
    {
        private QUANLYDIADIEMEntities db = null;

        public UserDao()
        {
            db = new QUANLYDIADIEMEntities();
        }

        public string Insert(KHACHHANG entity)
        {
            
                db.KHACHHANGs.Add(entity);
                db.SaveChanges();
                return entity.KHACHHANG_ID;
            
            

        }

        public bool Update(KHACHHANG entity)
        {
            try
            {
                var user = db.KHACHHANGs.Find(entity.KHACHHANG_ID);
                user.KHACHHANG_TEN = entity.KHACHHANG_TEN;
                if (!string.IsNullOrEmpty(entity.KHACHHANG_PASSWORD))
                {
                    user.KHACHHANG_PASSWORD = entity.KHACHHANG_PASSWORD;
                }

                user.KHACHHANG_EMAIL = entity.KHACHHANG_EMAIL;


                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public KHACHHANG GetById(string KH_TK)
        {
            return db.KHACHHANGs.SingleOrDefault(x => x.KHACHHANG_ID == KH_TK);
        }
        public int Login(string userName, string passWord, bool isLoginAdmin = false)
        {
            var result = db.KHACHHANGs.SingleOrDefault(x => x.KHACHHANG_ID == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (isLoginAdmin == true)
                {
                    if (result.KHACHHANG_ID == CommonConstants.USER_SESSION)
                    {
                        if (result.KHACHHANG_PASSWORD == passWord)
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
                        if (result.KHACHHANG_PASSWORD == passWord)
                            return 1;
                        else
                            return -2;
                 
                }
            }
        }

        public bool CheckKH(string TenKH)
        {
            return db.KHACHHANGs.Count(x => x.KHACHHANG_ID == TenKH) > 0;
        }

        public bool CheckEmail(string Email)
        {
            return db.KHACHHANGs.Count(x => x.KHACHHANG_EMAIL == Email) > 0;
        }
    }
}