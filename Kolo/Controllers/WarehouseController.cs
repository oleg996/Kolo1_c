using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Tutorial9.Controllers
{
    [Route("api/warehouce")]
    [ApiController]
    public class WarehouceController : ControllerBase
    {
        private readonly IConfiguration  _confyguration;


        public WarehouceController(IConfiguration configuration){
            _confyguration = configuration;
        }



        [HttpPost]
        public async Task<IActionResult> addItem(){
            return Ok(_confyguration.GetConnectionString("default"));
        }



    }
}