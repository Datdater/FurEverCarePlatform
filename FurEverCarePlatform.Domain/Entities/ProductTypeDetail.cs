﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace FurEverCarePlatform.Domain.Entities;

public class ProductTypeDetail : BaseEntity
{
	[MaxLength(255)]
	public string Name { get; set; }
    public Guid? ProductTypeId { get; set; }

	//navigation
    public virtual ProductType ProductType { get; set; } 
	public ProductTypeDetail() : base()
    {

	}
}
