using Microsoft.AspNetCore.Mvc.TagHelpers;
using ShoesStore.Common.DAL;
using ShoesStore.Common.Rsp;
using ShoesStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoesStore.DAL
{
    public class UsersRep : GenericRep<qldaContext, User>
    {
        public UsersRep()
        {

        }

        // Sửa get by ID
        public SingleRsp GetUserByID(int id)
        {
            var res = new SingleRsp();

            using (var context = new qldaContext())
            {
                var user = context.Users.Find(id);
                try
                {
                    if (user == null)
                    {
                        res.SetError("Nhân viên không được tìm thấy"); // Thông báo lỗi nếu không tìm thấy nhân viên
                    }    
                    else
                    {
                        res.Data = user; // Lưu trữ thông tin vào thuộc tính data của biến res
                    }
                }
                catch (Exception ex)
                {
                    res.SetError(ex.StackTrace);
                }
            }
            return res;
        }

        public int Remove(int id)
        {
            var m = base.All.First(i => i.UserId == id);
            m = base.Delete(m);
            return m.UserId;
        }

        public SingleRsp DeleteUser(User user)
        {
            var res = new SingleRsp();
            using (var context = new qldaContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.Users.Remove(user);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }

        public SingleRsp CreateUser(User user)
        {
            var res = new SingleRsp();
            using (var context = new qldaContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.Users.Add(user);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }

        public SingleRsp UpdateUser(User user)
        {
            var res = new SingleRsp();
            using (var context = new qldaContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.Users.Update(user);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }
    }
}
