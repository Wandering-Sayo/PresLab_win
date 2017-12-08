using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace PresLab.DAL
{
    public class PresLabConfiguration : DbConfiguration
    {
        public PresLabConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}