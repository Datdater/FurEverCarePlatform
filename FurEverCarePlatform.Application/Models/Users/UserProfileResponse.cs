using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Models.Users
{
    public class UserProfileResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string Wallet { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class UpdateProfileRequest
    {
        public string? Name { get; set; }

        public string? PhoneNumber { get; set; }

        public string? ProfilePictureUrl { get; set; }
    }
}
