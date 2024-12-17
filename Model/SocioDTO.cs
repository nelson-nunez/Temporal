using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SocioDTO
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Equipo { get; set; }


        // Constructor vacío
        public SocioDTO() { }

        // Constructor parametrizado
        public SocioDTO(string nombre, int edad, string equipo)
        {
            Nombre = nombre;
            Edad = edad;
            Equipo = equipo;
        }
    }
}
