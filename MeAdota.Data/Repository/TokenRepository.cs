using MeAdota.Data.Interfaces;
using MeAdota.Model;

namespace MeAdota.Data.Repository
{
    public class TokenRepository : BaseRepository<Token>, ITokenRepository
    {
        public TokenRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
