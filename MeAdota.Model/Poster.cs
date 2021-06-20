using MeAdota.Model.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeAdota.Model
{
    public class Poster
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PetName { get; set; }
        public EPetType PetType { get; set; }
        public bool Castrated { get; set; }
        public bool Dewormed { get; set; }
        public bool DeliverToAdopter { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public bool IsAdopted { get; set; }
        public int? AdopterId { get; set; }

        public virtual List<PetPicture> PetPictures { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("AdopterId")]
        public virtual User AdoptedBy { get; set; }
        
        public virtual List<Reports> Reports { get; set; }
    }
}
