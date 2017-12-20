using NewsSite.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;
using System.Collections.Generic;

namespace NewsSite.Controllers
{

    [Route("check")]
    public class CheckController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly DataHandler dhandler;


        public CheckController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            DataHandler dhandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.dhandler = dhandler;
        }

        [HttpGet, Route("openNews")]
        public IActionResult ViewOpenNews()
        {
            return Ok("");
        }

        [HttpPost, Route("recreateUsers")]
        async public Task<IActionResult> RecreateAllUsers()
        {
            dhandler.RemoveAllUsers();
            await dhandler.CreateUsers();
            return Ok("");
        }

        [Authorize(Policy = "HiddenNews"), HttpGet, Route("hiddennews")]
        public IActionResult GetHiddenNews()
        {
            return Ok("Kan se");
        }

        [HttpGet, Route("allUsers")]
        public IActionResult GetAllUsers()
        {
            var allUserEmails = _userManager.Users.Select(user => user.UserName);
            return Ok(allUserEmails);
        }

        [HttpGet, Route("signin")]
        async public Task<IActionResult> SignIn(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            await _signInManager.SignInAsync(user, false);
            return Ok(user);
        }

        [Authorize(Policy = "AgeLimit"), HttpGet, Route("agelimitnews")]
        public IActionResult AgeLimitNews()
        {
            return Ok("Kan se");
        }

        [HttpGet, Route("usersWithClaims")]
        public IActionResult GetAllUsersWithClaims()
        {
            var allUsers = _userManager.Users.ToList();
            List<object> claims = new List<object>();
            foreach (var user in allUsers)
            {
                user.Claims = _userManager.GetClaimsAsync(user).Result;
                //claims.Add(_userManager.GetClaimsAsync(user).Result);
            }
            return Ok(allUsers);
        }

        [Authorize(Policy = "SportsNews"), HttpGet, Route("sportsNews")]
        public IActionResult SportsNews()
        {
            return Ok("Kan se");
        }

        [Authorize(Policy = "CultureNews"), HttpGet, Route("cultureNews")]
        public IActionResult CultureNews()
        {
            return Ok("Kan se");
        }

    }
}
