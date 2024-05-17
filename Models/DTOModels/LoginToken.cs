using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOModels
{
    public class LoginToken
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }

        public int UserId { get; set; }

        public bool IsAuthorized { get; set; }

        public UserDto User { get; set; }
    }
}
