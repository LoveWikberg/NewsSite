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

    [Route("news")]
    public class NewsController : Controller
    {

        [HttpGet, Route("openNews")]
        async public Task<IActionResult> OpenNews()
        {
            // TA BORT EFTER PUBLICERING | ÄVEN ASYNC
            //await dhandler.AddRolesIfTheyDontExist();
            return Ok("");
        }

        [Authorize(Policy = "HiddenNews"), HttpGet, Route("hiddennews")]
        public IActionResult HiddenNews()
        {
            return Ok("Kan se");
        }

        [Authorize(Policy = "AgeLimit"), HttpGet, Route("agelimitnews")]
        public IActionResult AgeLimitNews()
        {
            return Ok("Kan se");
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
