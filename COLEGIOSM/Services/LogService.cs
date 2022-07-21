using COLEGIOSM.Models;
using COLEGIOSM.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COLEGIOSM.Services
{
    public class LogService
    {
        private COLEGIOSMContext _dbContext;

        public LogService(COLEGIOSMContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task SaveLog(LogModel model)
        {
            var log = new Logs
            {
                Url = model.Url,
                Request = model.Request,
                Exception = model.Exception,
                Date = DateTime.UtcNow
            };

            this._dbContext.Logs.Add(log);
            await this._dbContext.SaveChangesAsync();
        }
    }
}
