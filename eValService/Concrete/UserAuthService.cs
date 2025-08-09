using eValService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eValService.Concrete
{
    public class UserAuthService : IUserAuthService
    {
        public UserAuthService() { }

        public Task<bool> AuthenticateUser()
        {
            throw new NotImplementedException();
        }
    }
}
