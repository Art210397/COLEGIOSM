using System;
using System.Collections.Generic;

namespace COLEGIOSM.Models
{
    public partial class Matricula
    {
        public int Idmatricula { get; set; }
        public int Idapoderado { get; set; }
        public int Idalumno { get; set; }
        public string Codigo { get; set; }
        public string Ieprocedencia { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaRegistro { get; set; }

        public virtual Apoderado IdapoderadoNavigation { get; set; }
    }
}
