using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        [Required]
        [KeyGuidLength]
        public string Id { get; set; } = null!;
        protected BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
        }
        private ICollection<INotification>? _events;
        public IEnumerable<INotification>? Events => _events;
        public void RaiseEvent(INotification baseEvent)
        {
            _events = _events ?? [];
            _events.Add(baseEvent);
        }
        public void RemoveEvent(INotification baseEvent)
        {
            _events?.Remove(baseEvent);
        }
        public void ClearEvents()
        {
            _events?.Clear();
        }
    }
}
