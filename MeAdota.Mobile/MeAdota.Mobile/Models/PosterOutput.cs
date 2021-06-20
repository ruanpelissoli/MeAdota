using System.Collections.Generic;

namespace MeAdota.Mobile.Models
{
    public class PosterOutput
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PetName { get; set; }
        public int PetType { get; set; }
        public bool Castrated { get; set; }
        public bool Dewormed { get; set; }
        public bool DeliverToAdopter { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public bool IsReported { get; set; }
        public bool IsAdopted { get; set; }
        public int? AdopterId { get; set; }
        public string MainPictureUrl { get; set; }

        public List<PetPicture> PetPictures { get; set; }
        public User User { get; set; }
        public User AdoptedBy { get; set; }
    }
}
