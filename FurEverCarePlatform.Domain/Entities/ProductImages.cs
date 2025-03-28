using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Domain.Entities
{
    public class ProductImages : BaseEntity
    {
        public string URL { get; set; }
        public Guid ProductId { get; set; }
        //navigation
        public virtual Product Product { get; set; }
    }
}
