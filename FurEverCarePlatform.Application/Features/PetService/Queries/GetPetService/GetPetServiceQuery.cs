﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.Queries.GetPetService
{
    public class GetPetServiceQuery : IRequest<PetServiceDto>
	{
		public Guid Id { get; set; }
	}
}
