using ApplicationCore.Entities.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public class Message : BaseEntity
    {
        [Required]
        [KeyGuidLength]
        public string? MessageFrom { get; private set; } = null;
        [Required]
        [KeyGuidLength]
        public string? MessageTo { get;private set; } = null;
        [Required]
        public string Content { get; private set; }
        public DateTime TimeSpan { get; private set; }
        public Message(string messageFrom, string messageTo, string content)
        {
            MessageFrom = messageFrom;
            MessageTo = messageTo;
            Content = content;
            TimeSpan = DateTime.Now;
        }
    }
}
