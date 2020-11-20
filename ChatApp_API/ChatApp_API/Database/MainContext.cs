using ChatApp_API.Srv;
using Microsoft.EntityFrameworkCore;

namespace ChatApp_API.Database
{
    public class MainContext : BaseContext
    {
        public DbSet<IChatHub> IChatHub { get; set; }
    }
}
