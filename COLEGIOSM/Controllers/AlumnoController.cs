using ClosedXML.Excel;
using COLEGIOSM.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace COLEGIOSM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        COLEGIOSMContext bd = new COLEGIOSMContext();

        // GET: api/<AlumnoController>
        [HttpGet]
        public IEnumerable<Alumno> Get()
        {
            return bd.Alumno.ToList();
        }

        [HttpGet("report")]
        public ActionResult DownloadReport()
        {
            var alumnos = bd.Alumno.ToList();

            byte[] report;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Alumnos");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "IDAlumno";
                worksheet.Cell(currentRow, 2).Value = "IDApoderado";
                worksheet.Cell(currentRow, 3).Value = "Codigo";
                worksheet.Cell(currentRow, 4).Value = "Nombres";
                worksheet.Cell(currentRow, 5).Value = "Apellidos";
                worksheet.Cell(currentRow, 6).Value = "Direccion";
                worksheet.Cell(currentRow, 7).Value = "Ciudad";
                worksheet.Cell(currentRow, 8).Value = "Sexo";
                worksheet.Cell(currentRow, 9).Value = "DNI";

                foreach (var x in alumnos)
                {
                    currentRow++;

                    worksheet.Cell(currentRow, 1).Value = x.Idalumno;
                    worksheet.Cell(currentRow, 2).Value = x.Idapoderado;
                    worksheet.Cell(currentRow, 3).Value = x.Codigo;
                    worksheet.Cell(currentRow, 4).Value = x.Nombres;
                    worksheet.Cell(currentRow, 5).Value = x.Apellidos;
                    worksheet.Cell(currentRow, 6).Value = x.Direccion;
                    worksheet.Cell(currentRow, 7).Value = x.Ciudad;
                    worksheet.Cell(currentRow, 8).Value = x.Sexo;
                    worksheet.Cell(currentRow, 9).Value = x.Dni;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    report = content;
                }
            }

            return File(report, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Reporte_Alumnos_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx");
        }

        // GET api/<AlumnoController>/5
        [HttpGet("{id}")]
        public Alumno Get(int id)
        {
            var value = bd.Alumno.Find(id);
            return value;
        }

        // POST api/<AlumnoController>
        [HttpPost]
        public ActionResult Post([FromForm] Alumno value)
        {
            try
            {
                if (value.file is null)
                {
                    value.file = null;
                    value.Imagen = null;
                }
                else
                {
                    var ms = new MemoryStream();
                    value.file.CopyTo(ms);
                    var bytes = ms.ToArray();
                    value.Imagen = bytes;
                    bd.Alumno.Add(value);
                    bd.SaveChanges();
                }

                return Ok("Guardado con exito!");
            }
            catch (Exception ex)
            {
                bd = new COLEGIOSMContext();
                value.Imagen = null;
                var request = Newtonsoft.Json.JsonConvert.SerializeObject(value);

                var log = new Logs
                {
                    Url = "api/alumno/",
                    Request = request,
                    Exception = ex.Message.ToString(),
                    Method = "POST",
                    Date = DateTime.UtcNow
                };

                this.bd.Logs.Add(log);
                bd.SaveChanges();

                return StatusCode(500, "Error");
            }
        }

        public class ERRORMENSAJE
        {
            public string mensaje { get; set; }
        }

        // PUT api/<AlumnoController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromForm] Alumno value)
        {
            try
            {
                var alumno = this.bd.Alumno.FirstOrDefault(c => c.Idalumno == id);

                if (alumno == null)
                {
                    return NotFound("Alumno no existe");
                }

                if (value.file != null)
                {
                    var ms = new MemoryStream();
                    value.file.CopyTo(ms);
                    var bytes = ms.ToArray();

                    alumno.Imagen = bytes;
                }
                else
                {
                    alumno.Imagen = alumno.Imagen;
                }


                alumno.Idapoderado = value.Idapoderado;
                alumno.Codigo = value.Codigo;
                alumno.Nombres = value.Nombres;
                alumno.Apellidos = value.Apellidos;
                alumno.Direccion = value.Direccion;
                alumno.Sexo = value.Sexo;
                alumno.Dni = value.Dni;
                bd.Alumno.Update(alumno);
                bd.SaveChanges();

                return Ok(new ERRORMENSAJE { mensaje = "Guardado con exito!" });
            }
            catch (Exception arturo)
            {
                return BadRequest(new ERRORMENSAJE { mensaje = arturo.Message });
            }
        }


        // DELETE api/<AlumnoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var value = bd.Alumno.Find(id);
            bd.Alumno.Remove(value);
            bd.SaveChanges();
        }
    }
}
