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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public DataHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                new ApplicationUser {UserName="susan@gmail.com", Age=48, Role="Subscriber", Email="susan@gmail.com"},
                new ApplicationUser {UserName="viktor@gmail.com", Age=15, Role="Subscriber", Email="viktor@gmail.com"},
                new ApplicationUser {UserName="xerxes@gmail.com", Email="xerxes@gmail.com"}
            };
            return users;
        }

        public void RemoveAllUsers()
        {
            var allUsers = _userManager.Users.ToList();
            for (int i = 0; i < allUsers.Count(); i++)
            {
                _userManager.DeleteAsync(allUsers[i]);
            }
        }

        async public Task CreateUsers()
        {
            var users = UsersToCreate();
            for (int i = 0; i < users.Length; i++)
            {
                await _userManager.CreateAsync(users[i]);
                if (users[i].PublishRights != null)
                {
                    foreach (var publishRight in users[i].PublishRights)
                    {
                        await _userManager.AddClaimAsync(users[i], new Claim("PublishRights", publishRight));
                    }
                }
                if (users[i].Age != null)
                    await _userManager.AddClaimAsync(users[i], new Claim("Age", users[i].Age.ToString()));
                if (users[i].Role != null)
                    await _userManager.AddToRoleAsync(users[i], users[i].Role);
            }
        }

    }
}
