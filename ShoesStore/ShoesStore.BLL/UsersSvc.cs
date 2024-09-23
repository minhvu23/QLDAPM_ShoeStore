using Microsoft.EntityFrameworkCore.Metadata;
using ShoesStore.Common.BLL;
using ShoesStore.Common.Req;
using ShoesStore.Common.Rsp;
using ShoesStore.DAL;
using ShoesStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShoesStore.BLL
{
    public class UsersSvc : GenericSvc<UsersRep, User>
    {
        

        UsersRep req = new UsersRep();
        private UsersRep usersRep;
        public UsersSvc()
        {
            usersRep = new UsersRep();
        }

        public SingleRsp GetUserByID(int id)
        {
            var res = new SingleRsp();
            res.Data = _rep.GetUserByID(id);
            return res;
        }

        public override SingleRsp Update(User m)
        {
            var res = new SingleRsp();

            var m1 = m.UserId > 0 ? _rep.Read(m.UserId) : _rep.Read(m.UserId);
            if (m1 == null)
            {
                res.SetError("EZ103", "No data.");
            }
            else
            {
                res = base.Update(m);
                res.Data = m;
            }

            return res;
        }
        #region -- Methods --
        public SingleRsp CreateUser(CreateUserReq userReq)
        {
            var res = new SingleRsp();
            if (!IsValidEmail(userReq.Email))
            {
                res.SetError("Invalid Email Format");
                return res;
            }
            if (!IsValidPhoneNumber(userReq.PhoneNumber))
            {
                res.SetError("Invalid Phone Number Format");
                return res;
            }
            User user = new User();
            user.Username = userReq.Username;
            user.Email = userReq.Email;
            user.Password = userReq.Password;
            user.Fullname = userReq.Fullname;
            user.Address = userReq.Address;
            user.PhoneNumber = userReq.PhoneNumber;
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = user.CreatedAt;
            res = req.CreateUser(user);
            return res;
        }

        public SingleRsp UpdateUser(UserReq userReq)
        {
            var res = new SingleRsp();
            User user = new User();
            var check = base.All.Where(i => i.UserId == userReq.UserId).FirstOrDefault();
            if (check != null)
            {
                user.UserId = userReq.UserId;
                user.Username = userReq.Username;
                user.Email = userReq.Email;
                user.Password = userReq.Password;
                user.Fullname = userReq.Fullname;
                user.Address = userReq.Address;
                user.PhoneNumber = userReq.PhoneNumber;
                user.UpdatedAt = DateTime.Now;
                res = req.UpdateUser(user);
            }
            else
            {
                res.SetError("Cannot Find User!");
            }
            return res;
        }

        public SingleRsp DeleteUser(int id)
        {
            var res = new SingleRsp();
            User user = new User();
            var check = base.All.Where(i => i.UserId == id).FirstOrDefault();
            if (check != null)
            {
                user = base.All.FirstOrDefault(i => i.UserId == id);
                res = usersRep.DeleteUser(user);
            }
            else
            {
                res.SetError("Cannot Find User");
            }
            return res;
        }

        public object SearchUser(SearchUserReq searchUserReq)
        {
            var users = All.Where(x => x.Fullname.Contains(searchUserReq.Keyword));
            var offset = (searchUserReq.Page - 1) * searchUserReq.Size;
            var total = users.Count();
            int totalPage = (total / searchUserReq.Size) == 0 ? (int)(total / searchUserReq.Size) :
                (int)(1 + (total / searchUserReq.Size));
            var data = users.OrderBy(x => x.UserId).Skip(offset).Take(searchUserReq.Size).ToList();
            var res = new
            {
                Data = data,
                TotalRecord = total,
                TotalPages = totalPage,
                Page = searchUserReq.Page,
                Size = searchUserReq.Size
            };
            return res;
        }
        #endregion

        #region -- Validations --

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return Regex.IsMatch(email, emailPattern);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            try
            {
                var phonePattern = @"^[+]?[0-9]{10,15}$";
                return Regex.IsMatch(phoneNumber, phonePattern);
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}
