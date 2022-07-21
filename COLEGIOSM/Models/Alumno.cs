using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace COLEGIOSM.Models
{
    public partial class Alumno
    {
        //public Alumno()
        //{
        //    Asistencia = new HashSet<Asistencia>();
        //}

        public int Idalumno { get; set; }
        public int Idapoderado { get; set; }
        public string Codigo { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Sexo { get; set; }
        public string Ciudad { get; set; }
        public string Dni { get; set; }
        public bool? Estado { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public byte[] Imagen { get; set; }
        [NotMapped]
        public IFormFile file { get; set; }

        public virtual Apoderado IdapoderadoNavigation { get; set; }
        //public virtual ICollection<Asistencia> Asistencia { get; set; }
    }
}
