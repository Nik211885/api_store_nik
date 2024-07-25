using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public class KeyGuidLength : MaxLengthAttribute
    {
        public KeyGuidLength() : base(40)
        {
            
        }
    }
}
