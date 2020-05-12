using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzlemBilgisayar.Entities.Domains
{
    public class Order : AuditableEntity, IEntity
    {
        public int MovieId { get; set; }
        public bool IsComplete { get; set; }
        public Movie Movie { get; set; }
    }
}
