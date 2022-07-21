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
    public class ApoderadoController : ControllerBase
    {
        COLEGIOSMContext bd = new COLEGIOSMContext();
        
        // GET: api/<ApoderadoController>
        [HttpGet]
        public IEnumerable<Apoderado> Get()
        {
            return bd.Apoderado.ToList();
        }

        [HttpGet("report")]
        public ActionResult DownloadReport()
        {
            var apoderados = bd.Apoderado.ToList();

            byte[] report;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Apoderados");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "IDApoderado";
                worksheet.Cell(currentRow, 2).Value = "TipoRelacion";
                worksheet.Cell(currentRow, 3).Value = "Nombres";
                worksheet.Cell(currentRow, 4).Value = "Apellidos";
                worksheet.Cell(currentRow, 5).Value = "DNI";
                worksheet.Cell(currentRow, 6).Value = "Sexo";
                worksheet.Cell(currentRow, 7).Value = "Direccion";

                foreach (var x in apoderados)
                {
                    currentRow++;

                    worksheet.Cell(currentRow, 1).Value = x.Idapoderado;
                    worksheet.Cell(currentRow, 2).Value = x.TipoRelacion;
                    worksheet.Cell(currentRow, 3).Value = x.Nombres;
                    worksheet.Cell(currentRow, 4).Value = x.Apellidos;
                    worksheet.Cell(currentRow, 5).Value = x.Dni;
                    worksheet.Cell(currentRow, 6).Value = x.Sexo;
                    worksheet.Cell(currentRow, 7).Value = x.Direccion;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    report = content;
                }
            }

            return File(report, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Reporte_Apoderados_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx");
        }

        // GET api/<ApoderadoController>/5
        [HttpGet("{id}")]
        public Apoderado Get(int id)
        {
            var value = bd.Apoderado.Find(id);
            return value;
        }

        // POST api/<ApoderadoController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Apoderado value)
        {
            try
            {
                value.Estado = true;
                value.FechaRegistro = DateTime.Now;
                bd.Apoderado.Add(value);
                bd.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                bd = new COLEGIOSMContext();
                var request = Newtonsoft.Json.JsonConvert.SerializeObject(value);

                var log = new Logs
                {
                    Url = "api/apoderado/",
                    Request = request,
                    Exception = ex.Message.ToString(),
                    Method = "POST",
                    Date = DateTime.UtcNow
                };

                this.bd.Logs.Add(log);
                await this.bd.SaveChangesAsync();

                return StatusCode(500, "Error " + ex.Message.ToString());
            }
        }

        // PUT api/<ApoderadoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Apoderado value)
        {
            bd.Apoderado.Update(value);
            bd.SaveChanges();
        }

        // DELETE api/<ApoderadoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var value = bd.Apoderado.Find(id);
            bd.Apoderado.Remove(value);
            bd.SaveChanges();
        }
    }
}
