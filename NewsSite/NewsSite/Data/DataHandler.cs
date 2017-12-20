using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewsSite.Data
{
    public class DataHandler
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext context;


        public DataHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager
            , RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.context = context;

            context.Database.EnsureCreated();
            roleManager.CreateAsync(new IdentityRole { Name = "Administrator" }).Wait();
            roleManager.CreateAsync(new IdentityRole { Name = "Publisher" }).Wait();
            roleManager.CreateAsync(new IdentityRole { Name = "Subscriber" }).Wait();
        }


        async public Task AddRolesIfTheyDontExist()
        {
            string[] roles = new string[]
            {
                "Administrator",
                "Publisher",
                "Subscriber"
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var newRole = new IdentityRole
                    {
                        Name = role
                    };
                    await roleManager.CreateAsync(newRole);
                }
            }
        }

        private ApplicationUser[] UsersToCreate()
        {
            ApplicationUser[] users = new ApplicationUser[]
            {
                new ApplicationUser {UserName="adam@gmail.com",  Age=40, Role="Administrator", Email="adam@gmail.com"},
                new ApplicationUser {UserName="peter@gmail.com", Age=40, Role="Publisher", Email="peter@gmail.com",
                PublishRights =  new string[]{"Sports"}},
                new ApplicationUser {UserName="britt@gmail.com", Age=40, Role="Publisher", Email="britt@gmail.com",
                PublishRights =  new string[]{"Culture"}},
                new ApplicationUser {UserName="claes@gmail.com", Age=40, Role="Publisher", Email="claes@gmail.com",
                PublishRights =  new string[]{"Sports","Culture"}},
                new ApplicationUser {UserName="susan@gmail.com", Age=48, Role="Subscriber", Email="susan@gmail.com"},
                new ApplicationUser {UserName="viktor@gmail.com", Age=15, Role="Subscriber", Email="viktor@gmail.com"},
                new ApplicationUser {UserName="xerxes@gmail.com", Email="xerxes@gmail.com"}
            };
            return users;
        }

        async public Task RemoveAllUsers()
        {
            var allUsers = userManager.Users.ToList();
            for (int i = 0; i < allUsers.Count(); i++)
            {
                await userManager.DeleteAsync(allUsers[i]);
            }
        }

        async public Task CreateUsers()
        {
            var users = UsersToCreate();
            for (int i = 0; i < users.Length; i++)
            {
                await userManager.CreateAsync(users[i]);
                if (users[i].PublishRights != null)
                {
                    foreach (var publishRight in users[i].PublishRights)
                    {
                        await userManager.AddClaimAsync(users[i], new Claim("PublishRights", publishRight));
                    }
                }
                if (users[i].Age != null)
                    await userManager.AddClaimAsync(users[i], new Claim("Age", users[i].Age.ToString()));
                if (users[i].Role != null)
                    await userManager.AddToRoleAsync(users[i], users[i].Role);
            }
        }

    }
}
