using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using smashing72_manager.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using smashing72_manager.ViewModels;

namespace smashing72_manager.Controllers.api
{
    [Authorize, RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        public ApplicationUserManager UserManager =>  HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        public RoleManager<IdentityRole> RoleManager => new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
        
        [Route("")]
        // GET: api/Users
        public IEnumerable<UserVm> GetUsers()
        {
            var users = UserManager.Users.AsEnumerable().Select(UserVm.FromApplicationUser).ToList();
            foreach (var user in users)
            {
                user.Roles = UserManager.GetRoles(user.Id).ToList();
            }
            return users;
        }

        [Route("{id}")]
        public UserVm GetUser(string id)
        {
            var user = UserManager.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return null;

            var viewModel = UserVm.FromApplicationUser(user);

            viewModel.Roles = UserManager.GetRoles(user.Id).ToList();

            return viewModel;
        }
        
        [HttpPost, Route("addrole/{userId}")]
        public UserVm AddRole(string userId, string role)
        {
            var result = UserManager.AddToRole(userId, role);

            var user = UserManager.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) return null;

            var viewModel = UserVm.FromApplicationUser(user);

            viewModel.Roles = UserManager.GetRoles(user.Id).ToList();

            return viewModel;
        }

        [HttpPost, Route("removerole/{userId}")]
        public UserVm RemoveRole(string userId, string role)
        {
            var result = UserManager.RemoveFromRole(userId, role);

            var user = UserManager.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) return null;

            var viewModel = UserVm.FromApplicationUser(user);

            viewModel.Roles = UserManager.GetRoles(user.Id).ToList();

            return viewModel;
        }
    }
}