using System.ComponentModel.DataAnnotations;

namespace Adopcat.Model
{
    public class PetPicture
    {
        [Key]
        public int Id { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
