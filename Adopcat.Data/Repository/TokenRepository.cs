using Adopcat.Data.Interfaces;
using Adopcat.Model;

namespace Adopcat.Data.Repository
{
    public class TokenRepository : BaseRepository<Token>, ITokenRepository
    {
        public TokenRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
