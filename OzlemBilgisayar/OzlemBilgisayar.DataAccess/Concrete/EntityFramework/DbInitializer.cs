using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using OzlemBilgisayar.Entities.Domains;

namespace OzlemBilgisayar.DataAccess.Concrete.EntityFramework
{
    public class DbInitializer : CreateDatabaseIfNotExists<OzlemBilgisayarDbContext>
    {
        protected override void Seed(OzlemBilgisayarDbContext context)
        {
            
            var roles = new List<Role>()
            {
                new Role{Created=DateTime.Now,Modified=DateTime.Now,IsActive=true, Name="SuperAdmin"},
                new Role{Created=DateTime.Now,Modified=DateTime.Now,IsActive=true, Name="Admin" },
                new Role{Created=DateTime.Now,Modified=DateTime.Now,IsActive=true, Name="Client" }
            };
            foreach (var role in roles)
            {
                context.Roles.Add(role);
            }
            context.SaveChanges();

            var users = new List<User>()
            {
                new User
                {
                    Created=DateTime.Now,
                    Email="yasinaksu@gmail.com",
                    FirstName="Yasin",
                    Image="default.jpg",
                    IsActive=true,
                    LastName="Aksu",
                    Modified=DateTime.Now,
                    Password="8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92",
                    Roles=context.Roles.Where(x=>x.Name=="SuperAdmin").ToList(),
                    UserName="yasinaksu88"
                },
                new User
                {
                    Created=DateTime.Now,
                    Email="cetin@gmail.com",
                    FirstName="Çetin",
                    Image="default.jpg",
                    IsActive=true,
                    LastName="Cedimoğlu",
                    Modified=DateTime.Now,
                    Password="8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92",
                    Roles=context.Roles.Where(x=>x.Name!="SuperAdmin").ToList(),
                    UserName="bycetin"
                },
                new User
                {
                    Created=DateTime.Now,
                    Email="recep@gmail.com",
                    FirstName="Recep",
                    Image="default.jpg",
                    IsActive=true,
                    LastName="Aydın",
                    Modified=DateTime.Now,
                    Password="8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92",
                    Roles=context.Roles.Where(x=>x.Name!="SuperAdmin").ToList(),
                    UserName="recepaydin"
                }
            };
            foreach (var user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();            
            base.Seed(context);
        }
    }
}
