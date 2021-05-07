using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces;
using CoffeeMapServer.Services.Interfaces.Admin;
using CoffeeMapServer.ViewModels;
using CoffeeMapServer.ViewModels.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeMapServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoastersController : ControllerBase
    {
        private readonly IRoasterService _roasterService;
        private readonly IRoasterRequestService _roasterRequestService;

        public RoastersController(IRoasterService roasterService,
                                  IRoasterRequestService roasterRequestService)
        {
            _roasterService = roasterService ?? throw new ArgumentNullException(nameof(IRoasterService));
            _roasterRequestService = roasterRequestService ?? throw new ArgumentNullException(nameof(IRoasterRequestService));
        }

        [HttpGet]
        [Route("All")]
        [ProducesResponseType(typeof(List<RoasterInfoViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RoastersInfo()
        {
            var roasters = await _roasterService.GetRoastersAsync();
            return Ok(roasters);
        }

        [HttpGet]
        [Route("Single")]
        [ProducesResponseType(typeof(RoasterInfoViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RoasterInfoById(Guid id)
        {
            var roaster = await _roasterService.GetRoasterViewModel(id);
            return Ok(roaster);
        }

        [HttpPost]
        [Route("PostRequest")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Roaster([FromBody]RoasterRequestDT roasterRequest)
        {
            await _roasterRequestService.SendRoasterRequest(roasterRequest);
            return Ok();
        }

    }
}