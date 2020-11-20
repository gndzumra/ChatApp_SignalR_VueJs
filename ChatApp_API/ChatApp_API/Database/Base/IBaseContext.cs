using System.Threading.Tasks;

namespace ChatApp_API.Database
{
    public interface IBaseContext
    {
        public DBResult Save();
        public Task<DBResult> SaveAsync();
    }
}