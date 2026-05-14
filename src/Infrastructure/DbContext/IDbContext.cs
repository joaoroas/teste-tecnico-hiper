using System.Data;

namespace Infrastructure.DbContext
{
    public interface IDbContext
    {
        IDbConnection CreateConnection();
    }
}
