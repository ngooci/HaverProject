using System.ComponentModel.DataAnnotations;

namespace HaverProject.ViewModel
{
    public class EmailAddress
    {
        public int id { get; set; }
        [Required(ErrorMessage = "You cannot leave the Name blank.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email address is required.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Your email address should be @gmail.com, @hotmail.com, @outlook.com, etc.")]
        public string Address { get; set; }
    }
}
