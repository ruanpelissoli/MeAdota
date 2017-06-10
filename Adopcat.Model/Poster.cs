using Adopcat.Model.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adopcat.Model
{
    public class Poster
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public PetPicture PetPicture { get; set; }
        public EPetType PetType { get; set; }
        public bool Castrated { get; set; }
        public bool Dewormed { get; set; }
        public bool DeliverToAdopter { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public bool IsAdopted { get; set; }
        public int AdopterId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("AdopterId")]
        public virtual User AdoptedBy { get; set; }
    }
}
