using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FurEverCarePlatform.Application.Features.Pets.Commands.DeletePet
{
    public class DeletePetCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
