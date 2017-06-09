using Adopcat.Data.Interfaces;
using Adopcat.Model;

namespace Adopcat.Data.Repository
{
    public class SystemLogRepository : BaseRepository<SystemLog>, ISystemLogRepository
    {
        public SystemLogRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
