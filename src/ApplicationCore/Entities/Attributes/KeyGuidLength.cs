using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.Attributes
{
    public class KeyGuidLength : MaxLengthAttribute
    {
        public KeyGuidLength() : base(40)
        {

        }
    }
}
