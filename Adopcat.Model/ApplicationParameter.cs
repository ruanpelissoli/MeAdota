using System.ComponentModel.DataAnnotations;

namespace Adopcat.Model
{
    public class ApplicationParameter
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        [Required]
        [StringLength(64)]
        public string Key { get; set; }
        [Required]
        [StringLength(512)]
        public string Value { get; set; }
    }
}
