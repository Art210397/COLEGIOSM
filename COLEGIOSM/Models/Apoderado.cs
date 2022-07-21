using System;
using System.Collections.Generic;

namespace COLEGIOSM.Models
{
    public partial class Apoderado
    {
        public Apoderado()
        {
            Alumno = new HashSet<Alumno>();
            Matricula = new HashSet<Matricula>();
        }

        public int Idapoderado { get; set; }
        public string TipoRelacion { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Dni { get; set; }
        public string Sexo { get; set; }
        public string Direccion { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaRegistro { get; set; }

        public virtual ICollection<Alumno> Alumno { get; set; }
        public virtual ICollection<Matricula> Matricula { get; set; }
    }
}
