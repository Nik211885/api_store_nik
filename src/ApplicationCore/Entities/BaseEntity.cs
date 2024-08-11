using ApplicationCore.Entities.Attributes;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        [Required]
        [KeyGuidLength]
        public string Id { get; private set; } = null!;
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
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        public override bool Equals(object? obj)
        {
            return obj is BaseEntity;
        }
        public bool Equals(BaseEntity other)
        {
            return Id == other.Id;
        }
        public static bool operator ==(BaseEntity left, BaseEntity right) 
        {
            return left.Equals(right);
        }
        public static bool operator !=(BaseEntity left, BaseEntity right)
        {
            return !left.Equals(right);
        }
    }
}
