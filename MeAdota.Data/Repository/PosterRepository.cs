using MeAdota.Data.Interfaces;
using MeAdota.Model;

namespace MeAdota.Data.Repository
{
    public class PosterRepository : BaseRepository<Poster>, IPosterRepository
    {
        public PosterRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }
    }
}
