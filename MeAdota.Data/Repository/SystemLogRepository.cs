using MeAdota.Data.Interfaces;
using MeAdota.Model;

namespace MeAdota.Data.Repository
{
    public class SystemLogRepository : BaseRepository<SystemLog>, ISystemLogRepository
    {
        public SystemLogRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
