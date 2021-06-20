using MeAdota.Data.Interfaces;
using MeAdota.Model;

namespace MeAdota.Data.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }
    }
}
