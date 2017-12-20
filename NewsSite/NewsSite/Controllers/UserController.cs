using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsSite.Data;
using Microsoft.AspNetCore.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewsSite.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly DataHandler dhandler;


        public UserController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            DataHandler dhandler)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.dhandler = dhandler;
        }

        [HttpPost, Route("recreateUsers")]
        async public Task<IActionResult> RecreateAllUsers()
        {
            await dhandler.RemoveAllUsers();
            await dhandler.CreateUsers();
            // Return empty string to fix Firefix bug
            return Ok("");
        }

        [HttpGet, Route("allUsers")]
        public IActionResult GetAllUserNames()
        {
            var allUserEmails = userManager.Users.Select(user => user.UserName);
            return Ok(allUserEmails);
        }

        [HttpPost, Route("signin")]
        async public Task<IActionResult> SignIn(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            await signInManager.SignInAsync(user, false);
            return Ok(user);
        }

        [HttpGet, Route("usersWithClaims")]
        public IActionResult GetAllUsersWithClaims()
        {
            var allUsers = userManager.Users.ToList();
            foreach (var user in allUsers)
            {
                user.Claims = userManager.GetClaimsAsync(user).Result;
            }
            return Ok(allUsers);
        }
    }
}
