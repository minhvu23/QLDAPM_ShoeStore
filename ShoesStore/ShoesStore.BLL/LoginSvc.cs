using ShoesStore.Common.BLL;
using ShoesStore.Common.Rsp;
using ShoesStore.DAL;
using ShoesStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesStore.BLL
{
    public class LoginSvc : GenericSvc<LoginRep, User>
    {
        private readonly LoginRep _loginRep;

        public LoginSvc()
        {
            _loginRep = new LoginRep();
        }

        public SingleRsp Authenticate(string username, string password)
        {
            var res = new SingleRsp();
            var user = _loginRep.Authenticate(username, password);

            if (user != null)
            {
                res.SetData("OK", user);
            }
            else
            {
                res.SetError("ERR_LOGIN", "Invalid username or password.");
            }

            return res;
        }
    }
}
