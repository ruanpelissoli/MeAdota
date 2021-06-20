using MeAdota.Data.Interfaces;
using MeAdota.Model;

namespace MeAdota.Data.Repository
{
    public class PetPictureRepository : BaseRepository<PetPicture>, IPetPictureRepository
    {
        public PetPictureRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }
    }
}
