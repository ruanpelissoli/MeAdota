namespace MeAdota.Mobile.Models
{
    public class Filter
    {
        public int? PetType { get; set; }
        public bool? Castrated { get; set; }
        public bool? Dewormed { get; set; }
        public bool? DeliverToAdopter { get; set; }
        public string City { get; set; }
    }
}
