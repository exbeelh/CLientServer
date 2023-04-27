using EmployeeApp.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Server.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "First Name")]
        [RegularExpression("^[A-Z][a-z]*$", ErrorMessage = "Invalid input name.")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public EnumGender Gender { get; set; }

        [Display(Name = "Email Address"), EmailAddress]
        public string Email { get; set; } = null!;

        [Display(Name = "Phone Number"), Phone, StringLength(12)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number must be number.")]
        public string PhoneNumber { get; set; } = null!;

        public string Major { get; set; } = null!;

        public string Degree { get; set; } = null!;

        [Range(0, 4, ErrorMessage = "The {0} must not less than {1} and more than {2}.")]
        public decimal GPA { get; set; }

        [Display(Name = "University Name")]
        public string UniversityName { get; set; } = null!;

        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and Confirmed Password do not match.")]
        public string ConfirmedPassword { get; set; } = null!;
    }
}
