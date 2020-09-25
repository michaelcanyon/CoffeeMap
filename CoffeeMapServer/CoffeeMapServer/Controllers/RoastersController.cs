using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services;
using CoffeeMapServer.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeMapServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoastersController : ControllerBase
    {
        private readonly IRoasterService _roasterService;
        public RoastersController(IRoasterService roasterService)
        {
            _roasterService = roasterService;
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<Roaster>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RoastersInfo()
        {
            var roasters = await _roasterService.GetRoasters();
            return Ok(roasters);
        }

        [HttpGet]
        [ProducesResponseType(typeof(RoasterInfoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RoasterInfoById(int id)
        {
            var roaster = await _roasterService.GetSingleRoaster(id);
            return Ok(roaster);
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Roaster([FromBody]RoasterRequest roasterRequest)
        {
            await _roasterService.PostRoasterRequest(roasterRequest);
            return Ok();
        }

    }
}
