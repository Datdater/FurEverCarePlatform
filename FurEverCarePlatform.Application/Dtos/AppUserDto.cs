using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Dtos
{
    public class AppUserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? CreationDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Guid? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }

        // Navigation properties
        public Store Store { get; set; }
        public List<Order> Orders { get; set; }
        public List<Booking> Bookings { get; set; }
        public List<Pet> Pets { get; set; }
        public List<Address> Address { get; set; }
        public List<Notification> Notifications { get; set; }
        public List<Feedback> Feedback { get; set; }
        public List<ChatMessage> ChatMessage { get; set; }
        public List<Report> Reports { get; set; }
    }
}
