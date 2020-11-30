using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackENDAPI.Auth
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Senha { get; set; }
    }
}
