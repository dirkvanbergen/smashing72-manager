using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using smashing72_manager.Models;

[assembly: OwinStartupAttribute(typeof(smashing72_manager.Startup))]
namespace smashing72_manager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
        }

        private void CreateRoles()
        {
            var context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rule   
                var role = new IdentityRole {Name = "Admin"};
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website
                var adminEmail = "dirkvanbergen@gmail.com";
                var user = userManager.FindByEmail(adminEmail);
                if (user == null)
                {
                    user = new ApplicationUser {UserName = adminEmail, Email = adminEmail };
                    var adminPwd = "{D1rk}1234";
                    userManager.Create(user, adminPwd);
                }
                userManager.AddToRole(user.Id, "Admin");
            }

            var roles = new[] { "PageEdit_All", "PageEdit_Self", "NewsEdit_All", "NewsEdit_Self", "TeamEdit_All", "TeamEdit_Self" };
            foreach (var roleName in roles)
            {
                if (roleManager.RoleExists(roleName)) continue;

                var role = new IdentityRole { Name = roleName };
                roleManager.Create(role);
            }
        }
    }
}
