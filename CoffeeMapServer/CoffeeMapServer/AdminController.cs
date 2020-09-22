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
    public class AdminController : Controller
    {
        private readonly IRoasterRepository _roasterRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoasterRequestRepository _roasterRequestRepository;
        public AdminController(IRoasterRepository roasterRepository, IUserRepository userRepository,
            IRoasterRequestRepository roasterRequestRepository)
        {
            _roasterRepository = roasterRepository;
            _userRepository = userRepository;
            _roasterRequestRepository = roasterRequestRepository;
        }
        [HttpGet]
        [Route("GetRoasters")]
        public async Task<IActionResult> GetRoasters()
        {
            try
            {
                var roasters = await _roasterRepository.GetList();
                ViewData["RoastersList"] = roasters;
                return Ok(roasters);

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
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
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
                var roaster = await _roasterRepository.GetSingle(roasterId);
                ViewData["SingleRoaster"] = roaster;
                return View();
                //return Ok(roaster);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPost]
        [Route("AddUser")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser(User user)
        {
            try
            {
                await _userRepository.Create(user);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }

        }

        [HttpDelete]
        [Route("DeleteUser")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUser(User user)
        {
            try
            {
                await _userRepository.Delete(user.Id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }

        }

        [HttpGet]
        [Route("GetUsers")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<List<User>> GetUsers()
        {
            try
            {
                return await _userRepository.GetList();
            }
            catch
            {
                return null;
            }

        }
        [HttpGet]
        [Route("GetRoasterRequests")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<List<RoasterRequest>> GetRoasterRequests()
        {
            try
            {
                return await _roasterRequestRepository.GetList();            }
            catch (Exception)
            {

                return null;
            }
        }
        [HttpGet]
        [Route("GetSingleRoasterRequest")]
        [ProducesResponseType(typeof(RoasterRequest), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<RoasterRequest> GetSingleRoasterRequest(int id)
        {
            try
            {
                return await _roasterRequestRepository.GetSingle(id);
            }
            catch (Exception)
            {

                return null;
            }
        }
        [HttpPost]
        [Route("AddRoasterRequest")]
        [ProducesResponseType(typeof(RoasterRequest), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddRoasterRequest([FromBody]RoasterRequest roasterRequest)
        {
            try
            {
                await _roasterRequestRepository.Create(roasterRequest);
                return Ok();
            }
            catch
            {
                return BadRequest("Unable to delete row! Wrong RoasterRequest format!");
               
            }
        }
        [HttpDelete]
        [Route("DeleteRoasterRequest")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteRoasterRequest(int id)
        {
            try
            {
                await _roasterRequestRepository.Delete(id);
                return Ok();
            }
            catch
            {
                return BadRequest("Unable to delete row! Wrong RoasterRequest format!");
            }
        }
    }
}