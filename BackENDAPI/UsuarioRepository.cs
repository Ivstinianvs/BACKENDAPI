using BackENDAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackENDAPI
{
    public class UsuarioRepository
    {
        public static Usuario Find(string login, string senha)
        {
            var users = new List<Usuario>()
            {
                new Usuario()
                {
                    Login = "Teste",
                    Senha = "123456"
                }
                ,
                new Usuario()
                {
                    Login = "Gabriel",
                    Senha = "123456"
                }
            };

            return users.Find(o => o.Login == login && o.Senha == senha);
        }

        public static List<Usuario> FindAll()
        {
            return new List<Usuario>()
            {
                new Usuario()
                {
                    Login = "Teste",
                    Senha = "123456"
                }
                ,
                new Usuario()
                {
                    Login = "Gabriel",
                    Senha = "123456"
                }
            };
        }
    }
}
