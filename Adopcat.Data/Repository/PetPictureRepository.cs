using Adopcat.Data.Interfaces;
using Adopcat.Model;

namespace Adopcat.Data.Repository
{
    public class PetPictureRepository : BaseRepository<PetPicture>, IPetPictureRepository
    {
        public PetPictureRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }
    }
}
