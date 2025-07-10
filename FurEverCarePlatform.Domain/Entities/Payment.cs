using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FurEverCarePlatform.Domain.Enums;

namespace FurEverCarePlatform.Domain.Entities;

public class Payment : BaseEntity
{
    public Guid? OrderId { get; set; }

    public Guid? BookingId { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string? Code { get; set; }

    [Required]
    public float Amount { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Booking? Booking { get; set; }
}
