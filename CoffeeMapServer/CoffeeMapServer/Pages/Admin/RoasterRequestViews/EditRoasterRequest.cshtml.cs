using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CoffeeMapServer.builders;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Admin.RoasterRequestViews
{
    public class EditRoasterRequestModel : PageModel
    {
        private readonly IRoasterRequestService _roasterRequestService;

        [BindProperty]

        public RoasterRequest _Request { get; set; }

        [BindProperty]
        public IFormFile Picture { get; set; }

        public string Role { get; set; }

        public IList<string> TagsList { get; set; }

        [BindProperty]
        public string Tags { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Latitude { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Longitude { get; set; }

        public EditRoasterRequestModel(IRoasterRequestService roasterRequestService)
            => _roasterRequestService = roasterRequestService ?? throw new ArgumentNullException(nameof(IRoasterRequestService));

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadata.role"].ToString();
            _Request = await _roasterRequestService.FetchSingleRoasterRequestByIdAsync(id);
                Latitude = _Request.Address.Latitude.ToString();
                Longitude = _Request.Address.Longitude.ToString();
            TagsList = _Request.TagString == null ? new List<string>() :
                                                    new List<string>(_Request.TagString.Split('#'));
            return _Request == null ? BadRequest() : (IActionResult)Page();
        }

        public async Task<IActionResult> OnPostProcessAsync()
        {
            try
            {
                if (!String.IsNullOrEmpty(Tags))
                {
                    _Request.TagString = Regex.Replace(Tags, @"[{:}#]", "");
                    _Request.TagString = _Request.TagString.Replace("value", "")
                               .Replace("[", "")
                               .Replace("]", "")
                               .Replace(@"\", string.Empty)
                               .Replace("\"", string.Empty)
                               .Replace(",", "#");
                }
                _Request.Address= AddressCoordinatesTransformer.ConvertCoordinates(_Request.Address, Latitude, Longitude);
                var respCode = await _roasterRequestService.UpdateRoasterRequestAsync(_Request, Picture);

                return respCode.Equals(0) ? RedirectToPage("RoasterRequests") : (IActionResult)BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}