﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.ServiceCategory.Queries.GetServiceCategories
{
    public class ServiceCategoriesDto
    {
		public Guid Id { get; set; }
		public required string Name { get; set; }
	}
}
