using Prism.Commands;

namespace Adopcat.Mobile.Models
{
    public class MenuItem
    {
        public string Text { get; set; }
        public string Icon { get; set; }
        public string GoTo { get; set; }
        public DelegateCommand CommandExecute { get; set; }
    }
}
