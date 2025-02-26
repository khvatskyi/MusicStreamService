using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStreamServiceApp.BLL.DTOs
{
    public class UserLoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
