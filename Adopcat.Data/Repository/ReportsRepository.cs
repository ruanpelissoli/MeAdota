using Adopcat.Data.Interfaces;
using Adopcat.Model;

namespace Adopcat.Data.Repository
{
    public class ReportsRepository : BaseRepository<Reports>, IReportsRepository
    {
        public ReportsRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }
    }
}
