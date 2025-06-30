using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Models.Addresses
{
    public class AddressRequest
    {
        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Ward { get; set; }

        [Required]
        public string District { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public string Name { get; set; }
        public bool IsDefault { get; set; } = false;
    }
}
