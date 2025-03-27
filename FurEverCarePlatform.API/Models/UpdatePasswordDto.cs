using System.ComponentModel.DataAnnotations;

namespace FurEverCarePlatform.API.Models
{
    public class UpdatePasswordDto
    {
        [Required(ErrorMessage = "Old password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Old password must be at least 6 characters long")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "New password must be at least 6 characters long")]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
            ErrorMessage = "New password must contain at least one uppercase letter, one lowercase letter, one number, and one special character."
        )]
        public string NewPassword { get; set; }
    }
}
