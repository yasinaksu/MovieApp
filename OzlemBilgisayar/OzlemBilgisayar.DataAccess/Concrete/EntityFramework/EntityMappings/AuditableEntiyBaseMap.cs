using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzlemBilgisayar.DataAccess.Concrete.EntityFramework.EntityMappings
{
    public class AuditableEntiyBaseMap<TEntity> : EntityTypeConfiguration<TEntity> where TEntity : AuditableEntity, IEntity, new()
    {
        public AuditableEntiyBaseMap()
        {
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("Id")
                .HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(x => x.Created)
                .HasColumnName("Created")
                .HasColumnType("datetime")
                .IsRequired();

            Property(x => x.Modified)
                .HasColumnName("Modified")
                .HasColumnType("datetime")
                .IsRequired();

            Property(x => x.IsActive)
                .HasColumnName("IsActive")
                .HasColumnType("bit")
                .IsRequired();
        }
    }
}
