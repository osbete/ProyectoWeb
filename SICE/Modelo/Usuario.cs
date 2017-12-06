using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public String User { get; set; }
        public String Password { get; }

        public String NombreCompleto { get; set; }

        public Usuario() { }

        public Usuario(int Id, String usu, String pass, String nomCom)
        {
            this.IdUsuario = Id;
            this.User = usu;
            this.Password = pass;
            this.NombreCompleto = nomCom;
        }
    }
}
