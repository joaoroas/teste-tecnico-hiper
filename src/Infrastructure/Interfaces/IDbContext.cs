using System.Data;

namespace Infrastructure.Interfaces
{
    public interface IDbContext
    {
        IDbConnection CreateConnection();
    }
}
