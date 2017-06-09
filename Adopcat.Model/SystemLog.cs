using Adopcat.Model.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Adopcat.Model
{
    public class SystemLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public ELogType LogType { get; set; }

        public DateTime LogDate { get; set; }
    }
}
