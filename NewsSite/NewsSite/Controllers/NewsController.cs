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
        public IActionResult OpenNews()
        {
            return Ok();
        }

        [Authorize(Policy = "HiddenNews"), HttpGet, Route("hiddennews")]
        public IActionResult HiddenNews()
        {
            return Ok();
        }

        [Authorize(Policy = "AgeLimit"), HttpGet, Route("agelimitnews")]
        public IActionResult AgeLimitNews()
        {
            return Ok();
        }

        [Authorize(Policy = "SportsNews"), HttpGet, Route("sportsNews")]
        public IActionResult SportsNews()
        {
            return Ok();
        }

        [Authorize(Policy = "CultureNews"), HttpGet, Route("cultureNews")]
        public IActionResult CultureNews()
        {
            return Ok();
        }
    }
}
