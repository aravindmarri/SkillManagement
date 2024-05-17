using Models.ViewModels;
using Models.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DBModels;
using Microsoft.AspNetCore.Identity;

namespace Services.Internfaces
{
    public interface IMainService
    {
        public Task<LoginToken> Login(string UserName, string Password);
        public Task<IdentityUser> GetUserById(string Id);
    }
}
