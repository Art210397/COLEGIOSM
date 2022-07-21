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
    public class LogsController : ControllerBase
    {

        COLEGIOSMContext bd = new COLEGIOSMContext();

        // GET: api/<LogsController>
        [HttpGet]
        public IEnumerable<Logs> Get()
        {
            return bd.Logs.ToList();
        }

        [HttpGet("report")]

        public ActionResult DownloadReport()
        {
            var logs = bd.Logs.ToList();

            byte[] report;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Logs");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "Url";
                worksheet.Cell(currentRow, 2).Value = "Request";
                worksheet.Cell(currentRow, 3).Value = "Date";
                worksheet.Cell(currentRow, 4).Value = "Exception";
                worksheet.Cell(currentRow, 5).Value = "Method";

                foreach (var x in logs)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = x.Url;
                    worksheet.Cell(currentRow, 2).Value = x.Request;
                    worksheet.Cell(currentRow, 3).Value = x.Date;
                    worksheet.Cell(currentRow, 4).Value = x.Exception;
                    worksheet.Cell(currentRow, 5).Value = x.Method;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    report = content;
                }
            }

            return File(report, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Reporte_Errores_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx");
        }

        // GET api/<LogsController>/5
        [HttpGet("{id}")]
        public Logs Get(int id)
        {
            var value = bd.Logs.Find(id);
            return value;
        }

        // POST api/<LogsController>
        [HttpPost]
        public void Post([FromBody] Logs value)
        {
            bd.Logs.Add(value);
            bd.SaveChanges();
        }

        // PUT api/<LogsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Logs value)
        {
            bd.Logs.Update(value);
            bd.SaveChanges();
        }

        // DELETE api/<LogsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var value = bd.Logs.Find(id);
            bd.Logs.Remove(value);
            bd.SaveChanges();
        }
    }
}
