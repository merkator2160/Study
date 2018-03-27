using System.ComponentModel.DataAnnotations;
using Notificator.Core.Model;

namespace Notificator.Site.Models 
{
    public class EventViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name required")]
        [Display(Name = "Event name")]
        public string Name { get; set; }

        public EventViewModel()
        {
            
        }

        public EventViewModel(Event @event)
        {
            Id = @event.Id;
            Name = @event.Name;
        }
    }
}