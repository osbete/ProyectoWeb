using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Materia
    {
        public int IdMateria { get; set; }
        public String NombreMateria { get; set; }
        public String Creditos { get; set; }
        public int idCarrera { get; set; }

        public Materia() { }

        public Materia(int IdMateria, String NombreMateria, String Creditos, int idCarrera)
        {
            this.IdMateria = IdMateria;
            this.NombreMateria = NombreMateria;
            this.Creditos = Creditos;
            this.idCarrera = idCarrera;
        }
    }
}
