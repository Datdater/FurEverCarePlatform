using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Domain.Entities
{
    public class Wallet : BaseEntity
    {
        public float Price { get; set; }

        public Store? Store { get; set; }   
    }
}
