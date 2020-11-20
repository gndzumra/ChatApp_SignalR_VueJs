using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace ChatApp_API.Database
{
    public class BaseContext : DbContext, IBaseContext
    {
        public IConfiguration Configuration { get; set; }
        public bool IsDevelopment { get; set; }
        public BaseContext(IConfiguration configuration)
        {
            IsDevelopment = bool.Parse(configuration.GetSection("IsDevelopment").Value);
            Configuration = configuration;
        }

        public DBResult Save()
        {
            DBResult response;
            try
            {
                int result = SaveChanges();
                response = new DBResult(true, result);
            }
            catch (Exception ex)
            {
                response = new DBResult(ex);
            }

            return response;
        }
        public async Task<DBResult> SaveAsync()
        {
            Task<DBResult> response;
            try
            {
                int result = await SaveChangesAsync();
                response = Task.FromResult(new DBResult(true, result));
            }
            catch (Exception ex)
            {
                response = Task.FromResult(new DBResult(ex));
            }

            return await response;
        }
    }
}