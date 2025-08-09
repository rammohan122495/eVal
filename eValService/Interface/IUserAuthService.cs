using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eValService.Interface
{
    public interface IUserAuthService
    {
        Task<bool> AuthenticateUser();
    }
}
