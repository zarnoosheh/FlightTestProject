using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Enums
{
    public enum GenderEnum
    {
        [Display(Name = "Female")]

        Female = 0,
        [Display(Name = "Male")]
        Male = 1,
    }
}
