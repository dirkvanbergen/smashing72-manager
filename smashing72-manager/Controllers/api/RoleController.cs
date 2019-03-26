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
    [Authorize, RoutePrefix("api/role")]
    public class RoleController : ApiController
    {
        public RoleManager<IdentityRole> RoleManager => new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
        
        [Route("")]
        // GET: api/role
        public IEnumerable<RoleVm> GetUsers()
        {
            var roles = RoleManager.Roles.AsEnumerable().Select(RoleVm.FromIdentityRole);

            return roles;
        }
    }
}