using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzlemBilgisayar.Entities.Domains
{
    public class Movie : AuditableEntity, IEntity
    {
        public Movie()
        {
            Categories = new List<Category>();
            Orders = new List<Order>();
        }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ImdbRate { get; set; }
        public string StoryLine { get; set; }
        public string Image { get; set; }
        public string Trailer { get; set; }
        public List<Order> Orders { get; set; }
        public virtual List<Category> Categories { get; set; }        
    }
}
