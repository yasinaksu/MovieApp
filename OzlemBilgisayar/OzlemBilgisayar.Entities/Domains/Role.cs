﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzlemBilgisayar.Entities.Domains
{
    public class Role : AuditableEntity, IEntity
    {
        public Role()
        {
            Users = new List<User>();
        }
        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
