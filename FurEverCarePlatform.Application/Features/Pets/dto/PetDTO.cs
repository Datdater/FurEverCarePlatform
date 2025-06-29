using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Pets.dto
{
	public class PetDTO
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Image { get; set; }
		public DateTime Dob { get; set; }
		public float Weight { get; set; }
		public bool PetType { get; set; }
		public Guid UserId { get; set; }
		public string? Color { get; set; }
		public string? SpecialRequirement { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
