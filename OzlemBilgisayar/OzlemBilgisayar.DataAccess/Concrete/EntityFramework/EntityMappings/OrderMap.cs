using OzlemBilgisayar.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzlemBilgisayar.DataAccess.Concrete.EntityFramework.EntityMappings
{
    public class OrderMap:AuditableEntiyBaseMap<Order>
    {
        public OrderMap()
        {
            ToTable(@"Orders", @"dbo");

            Property(x => x.IsComplete)
                .HasColumnName("IsComplete")
                .HasColumnType("bit")
                .IsRequired();

            Property(x => x.MovieId)
                .HasColumnName("MovieId")
                .HasColumnType("int")
                .IsRequired();

            HasRequired(x => x.Movie)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.MovieId)
                .WillCascadeOnDelete(false);
        }
    }
}
