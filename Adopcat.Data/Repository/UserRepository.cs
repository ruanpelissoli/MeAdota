using Adopcat.Data.Interfaces;
using Adopcat.Model;

namespace Adopcat.Data.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }
    }
}
