using OzlemBilgisayar.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzlemBilgisayar.DataAccess.Concrete.EntityFramework.EntityMappings
{
    public class UserMap : AuditableEntiyBaseMap<User>
    {
        public UserMap()
        {
            ToTable(@"Users", @"dbo");

            Property(x => x.Email)
                .HasColumnName("Email")
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.FirstName)
                .HasColumnName("FirstName")
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();

            Property(x => x.Image)
                .HasColumnName("Image")
                .HasColumnType("nvarchar")
                .HasMaxLength(255)
                .IsRequired();

            Property(x => x.LastName)
                .HasColumnName("LastName")
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();

            Property(x => x.Password)
                .HasColumnName("Password")
                .HasColumnType("nvarchar")
                .HasMaxLength(255)
                .IsRequired();

            Property(x => x.UserName)
                .HasColumnName("UserName")
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            HasMany(x => x.Roles).WithMany(x => x.Users).Map(m =>
            {
                m.ToTable("UserRoles");
                m.MapLeftKey("UserId");
                m.MapRightKey("RoleId");
            });
        }
    }
}
