using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adopcat.Mobile.Models
{
    public class PosterInput
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<byte[]> PetPictures { get; set; }
        public int PetType { get; set; }
        public bool Castrated { get; set; }
        public bool Dewormed { get; set; }
        public bool DeliverToAdopter { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public bool IsAdopted { get; set; }
        public int? AdopterId { get; set; }
    }
}
