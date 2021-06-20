using MeAdota.Data.Interfaces;
using MeAdota.Model;

namespace MeAdota.Data.Repository
{
    public class ReportsRepository : BaseRepository<Reports>, IReportsRepository
    {
        public ReportsRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }
    }
}
