using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzlemBilgisayar.Entities.Domains
{
    public class Category : AuditableEntity, IEntity
    {
        public Category()
        {
            Movies = new List<Movie>();
        }
        public string Name { get; set; }

        public virtual List<Movie> Movies { get; set; }
    }
}
