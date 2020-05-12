using Core.DataAccess.EntityFramework;
using OzlemBilgisayar.DataAccess.Abstract;
using OzlemBilgisayar.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzlemBilgisayar.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, OzlemBilgisayarDbContext>, IUserDal
    {
        public User AddWithRoles(User user, params int[] roleIds)
        {
            using (var context = new OzlemBilgisayarDbContext())
            {
                if (roleIds != null && roleIds.Length > 0)
                {
                    foreach (var roleId in roleIds)
                    {
                        user.Roles.Add(context.Roles.SingleOrDefault(x => x.Id == roleId));
                    }
                }
                context.Users.Add(user);
                context.SaveChanges();
                return user;
            }
        }

        public User UpdateWithRoles(User user, params int[] roleIds)
        {
            using (var context = new OzlemBilgisayarDbContext())
            {

                var entity = context.Users.Include("Roles").SingleOrDefault(x => x.Id == user.Id);
                entity.Created = user.Created;
                entity.Image = user.Image;
                entity.FirstName = user.FirstName;
                entity.IsActive = user.IsActive;
                entity.Modified = user.Modified;
                entity.Email = user.Email;
                entity.LastName = user.LastName;
                entity.UserName = user.UserName;
                entity.Password = user.Password;

                if (roleIds != null && roleIds.Length > 0)
                {
                    entity.Roles.Clear();
                    foreach (var roleId in roleIds)
                    {
                        entity.Roles.Add(context.Roles.SingleOrDefault(x => x.Id == roleId));
                    }
                }
                context.SaveChanges();
                return entity;
            }
        }
    }
}
