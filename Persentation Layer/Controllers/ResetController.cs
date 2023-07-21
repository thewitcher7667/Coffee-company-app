using Business_Logic_Layer.Services.Contracts;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Persentation_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResetController : Controller
    {
        private readonly IResetServices resetServices;

        public ResetController(IResetServices resetServices)
        {
            this.resetServices = resetServices;
        }

        [HttpGet]
        [Route("/Home/BuyCoffee")]
        public IActionResult Index() 
        {
            ViewData["UserId"] = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return View();
        }

        [HttpGet]
        [Route("/Home/BuyCoffee/Reset")]
        public async Task<IActionResult> Reset()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<Reset> userResets = await resetServices.GetResetListByUserId(userId);

            return View("~/Views/Reset/Resets.cshtml",userResets);
        }

        [HttpGet]
        [Route("/Home/BuyCoffee/Reset/{resetId}")]
        public IActionResult ReseByIdt(int resetId)
        {
            Reset reset = resetServices.GetReset(resetId);

            return View("~/Views/Reset/ResetItems.cshtml", reset);
        }

        // GET: api/<ResetController>
        [HttpGet]
        public async Task<List<Reset>> Get()
        {
            return await resetServices.GetResets();
        }

        // POST api/<ResetController>
        [HttpPost]
        public async Task<ActionResult<Reset>> Post([FromBody] List<ItemsBuyed> value,[FromQuery]string userId)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    return await resetServices.AddReset(value, userId);
                }
                catch
                {
                    return BadRequest();
                }
            }

            return BadRequest();
        }

    }
}
