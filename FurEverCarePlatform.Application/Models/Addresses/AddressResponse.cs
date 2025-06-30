using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Models.Addresses
{
    public class AddressResponse
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string AddressType { get; set; }
        public bool IsDefault { get; set; }
        public string FullAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
