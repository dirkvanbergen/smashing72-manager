using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using smashing72_manager.Models;

namespace smashing72_manager.ViewModels
{
    public class UserVm
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }

        public static UserVm FromApplicationUser(ApplicationUser user)
        {
            return new UserVm()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };
        }
    }
}