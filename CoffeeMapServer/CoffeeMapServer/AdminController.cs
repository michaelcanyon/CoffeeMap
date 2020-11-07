using System;
using System.Collections.Generic;
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
                var roasters = await _roasterRepository.GetListAsync();
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
                _roasterRepository.Add(roaster);
                await _roasterRepository.SaveChangesAsync();
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
                _roasterRepository.Update(roaster);
                await _roasterRepository.SaveChangesAsync();
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
        public async Task<IActionResult> DeleteRoaster(Guid roasterId)
        {
            try
            {
                var roaster = await _roasterRepository.GetSingleAsync(roasterId);
                _roasterRepository.Delete(roaster);
                await _roasterRepository.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpPost]
        [Route("SingleRoaster")]
        public async Task<IActionResult> GetSingleRoaster(Guid roasterId)
        {
            try
            {
                var roaster = await _roasterRepository.GetSingleAsync(roasterId);
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
                _userRepository.Add(user);
                await _roasterRepository.SaveChangesAsync();
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
                var user1 = await _userRepository.GetSingleAsync(user.Id);
                _userRepository.Delete(user1);
                await _userRepository.SaveChangesAsync();
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
        public async Task<IList<User>> GetUsers()
        {
            try
            {
                return await _userRepository.GetListAsync();
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
        public async Task<IList<RoasterRequest>> GetRoasterRequests()
        {
            try
            {
                return await _roasterRequestRepository.GetListAsync();
            }
            catch (Exception)
            {

                return null;
            }
        }
        [HttpGet]
        [Route("GetSingleRoasterRequest")]
        [ProducesResponseType(typeof(RoasterRequest), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<RoasterRequest> GetSingleRoasterRequest(Guid id)
        {
            try
            {
                return await _roasterRequestRepository.GetSingleAsync(id);
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
        public async Task<IActionResult> AddRoasterRequest([FromBody] RoasterRequest roasterRequest)
        {
            try
            {
                _roasterRequestRepository.Add(roasterRequest);
                await _roasterRequestRepository.SaveChangesAsync();
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
        public async Task<IActionResult> DeleteRoasterRequest(Guid id)
        {
            try
            {
                var roasterReq = await _roasterRequestRepository.GetSingleAsync(id);
                _roasterRequestRepository.Delete(roasterReq);
                await _roasterRequestRepository.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest("Unable to delete row! Wrong RoasterRequest format!");
            }
        }
    }
}