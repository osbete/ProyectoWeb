using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public String NombreCategoria { get; set; }
        public String Descripcion { get; set; }

        public Categoria() { }

        public Categoria(int IdCategoria, 
            String NombreCategoria, String Descripcion) {
            this.IdCategoria = IdCategoria;
            this.NombreCategoria = NombreCategoria;
            this.Descripcion = Descripcion;
        }
        
    }
}
