using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeAdota.Model
{
    public class PetPicture
    {
        [Key]
        public int Id { get; set; }
        public int PosterId { get; set; }
        public string Url { get; set; }

        [ForeignKey("PosterId")]
        public virtual Poster Poster { get; set; }
    }
}
