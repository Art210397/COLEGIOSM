using COLEGIOSM.Models;
using COLEGIOSM.Services;
using COLEGIOSM.Services.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace COLEGIOSM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        COLEGIOSMContext bd = new COLEGIOSMContext();
        //private readonly LogService _logService;

        //public AdminController(LogService logService)
        //{
        //    _logService = logService;
        //}

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Admin> Get()
        {
            return bd.Admin.ToList();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Admin Get(int id)
        {
            var value = bd.Admin.Find(id);
            return value;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Admin value)
        {
            value.AdminId = 10;
            try
            {
                bd.Admin.Add(value);
                bd.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                bd = new COLEGIOSMContext();
                var request = Newtonsoft.Json.JsonConvert.SerializeObject(value);

                var log = new Logs
                {
                    Url = "api/admin/",
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

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Admin value)
        {
            bd.Admin.Update(value);
            bd.SaveChanges();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var value = bd.Admin.Find(id);
            bd.Admin.Remove(value);
            bd.SaveChanges();
        }
    }
}
