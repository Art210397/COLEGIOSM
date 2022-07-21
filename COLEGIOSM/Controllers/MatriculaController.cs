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
    public class MatriculaController : ControllerBase
    {
        COLEGIOSMContext bd = new COLEGIOSMContext();
        
        // GET: api/<MatriculaController>
        [HttpGet]
        public IEnumerable<Matricula> Get()
        {
            return bd.Matricula.ToList();
        }

        [HttpGet("report")]
        public ActionResult DownloadReport()
        {
            var matricula = bd.Matricula.ToList();

            byte[] report;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Matricula");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "IDMatricula";
                worksheet.Cell(currentRow, 2).Value = "IDApoderado";
                worksheet.Cell(currentRow, 3).Value = "IDAlumno";
                worksheet.Cell(currentRow, 4).Value = "Codigo";
                worksheet.Cell(currentRow, 5).Value = "IEProcedencia";

                foreach (var x in matricula)
                {
                    currentRow++;

                    worksheet.Cell(currentRow, 1).Value = x.Idmatricula;
                    worksheet.Cell(currentRow, 2).Value = x.Idapoderado;
                    worksheet.Cell(currentRow, 3).Value = x.Idalumno;
                    worksheet.Cell(currentRow, 4).Value = x.Codigo;
                    worksheet.Cell(currentRow, 5).Value = x.Ieprocedencia;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    report = content;
                }
            }

            return File(report, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Reporte_Matricula_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx");
        }

        //GET api/<MatriculaController>/5
        [HttpGet("{id}")]
        public Matricula Get(int id)
        {
            var value = bd.Matricula.Find(id);
            return value;
        }

        //POST api/<MatriculaController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Matricula value)
        {
            try
            {
                value.Estado = true;
                value.FechaRegistro = DateTime.Now;
                bd.Matricula.Add(value);
                bd.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                bd = new COLEGIOSMContext();
                var request = Newtonsoft.Json.JsonConvert.SerializeObject(value);

                var log = new Logs
                {
                    Url = "api/matricula/",
                    Request = request,
                    Exception = ex.Message.ToString(),
                    Method = "POST",
                    Date = DateTime.UtcNow
                };

                this.bd.Logs.Add(log);
                await this.bd.SaveChangesAsync();

                return StatusCode(500, "Error");
            }
        }

        //PUT api/<MatriculaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Matricula value)
        {
            bd.Matricula.Update(value);
            bd.SaveChanges();
        }

        //DELETE api/<MatriculaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var value = bd.Matricula.Find(id);
            bd.Matricula.Remove(value);
            bd.SaveChanges();
        }
    }
}
