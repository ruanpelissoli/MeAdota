using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeAdota.Model
{
    public class Reports
    {
        [Key]
        public int Id { get; set; }
        public int PosterId { get; set; }
        public string Motive { get; set; }
        public string Description { get; set; }

        [ForeignKey("PosterId")]
        public virtual Poster Poster { get; set; }
    }
}
