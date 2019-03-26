using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using smashing72_manager.Models;

namespace smashing72_manager.ViewModels
{
    public class RoleVm
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public static RoleVm FromIdentityRole(IdentityRole role)
        {
            return new RoleVm()
            {
                Id = role.Id,
                Name = role.Name
            };
        }
    }
}