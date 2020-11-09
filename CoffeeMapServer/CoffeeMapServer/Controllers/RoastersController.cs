﻿using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces;
using CoffeeMapServer.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeMapServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoastersController : ControllerBase
    {
        private readonly IRoasterService _roasterService;
        public RoastersController(IRoasterService roasterService)
        {
            _roasterService = roasterService;
        }

        [HttpGet]
        [Route("All")]
        [ProducesResponseType(typeof(List<Roaster>), StatusCodes.Status200OK)]
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
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Roaster([FromBody] RoasterRequest roasterRequest)
        {
            var roasterRequestpar = RoasterRequest.New(roasterRequest.Name, roasterRequest.ContactNumber, roasterRequest.ContactEmail, roasterRequest.WebSiteLink, roasterRequest.VkProfileLink, roasterRequest.InstagramProfileLink, roasterRequest.TelegramProfileLink, roasterRequest.Description, roasterRequest.Picture);
            await _roasterService.SendRoasterRequest(roasterRequestpar);
            return Ok();
        }

    }
}
