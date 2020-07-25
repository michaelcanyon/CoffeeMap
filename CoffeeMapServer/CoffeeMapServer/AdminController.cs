using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeMapServer
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly IRoasterRepository _roasterRepository;
        public AdminController(IRoasterRepository roasterRepository)
        {
            _roasterRepository = roasterRepository;
        }
        [HttpGet]
        [Route("GetRoasters")]
        public async Task<IActionResult> GetRoasters()
        {
            try
            {
                var roasters= await _roasterRepository.GetList();
                ViewData["RoastersList"] = roasters;
                return View();
                //return Ok(roasters);

            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpPut]
        [Route("Roasters")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRoaster(Roaster roaster)
        {
            try
            {
                await _roasterRepository.Create(roaster);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpPost]
        [Route("Roaster")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateRoaster(Roaster roaster)
        {
            try
            {
                await _roasterRepository.Update(roaster);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpDelete]
        [Route("Roaster")]
        [ProducesResponseType(typeof(void),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteRoaster(int roasterId)
        {
            try
            {
                await _roasterRepository.Delete(roasterId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpPost]
        [Route("SingleRoaster")]
        public async Task<IActionResult> GetSingleRoaster(int roasterId)
        {
            try
            {
                var roaster= await _roasterRepository.GetSingle(roasterId);
                ViewData["SingleRoaster"] = roaster;
                return View();
                //return Ok(roaster);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }




    }
}
