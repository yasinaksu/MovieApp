using OzlemBilgisayar.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzlemBilgisayar.DataAccess.Concrete.EntityFramework.EntityMappings
{
    public class CategoryMap : AuditableEntiyBaseMap<Category>
    {
        public CategoryMap()
        {
            ToTable(@"Categories", @"dbo");

            Property(x => x.Name)
                .HasColumnName(@"Name")
                .HasColumnType("nvarchar")
                .IsRequired()
                .HasMaxLength(50);            
        }
    }
}
