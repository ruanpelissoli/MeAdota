using Adopcat.Data.Interfaces;
using Adopcat.Model;

namespace Adopcat.Data.Repository
{
    public class PosterRepository : BaseRepository<Poster>, IPosterRepository
    {
        public PosterRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }
    }
}
