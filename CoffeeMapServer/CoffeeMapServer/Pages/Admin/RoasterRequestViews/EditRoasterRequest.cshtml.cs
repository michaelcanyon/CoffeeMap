using System;
using System.Threading.Tasks;
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

        public RoasterRequest request { get; set; }

        [BindProperty]
        public IFormFile Picture { get; set; }

        public string Role { get; set; }

        public EditRoasterRequestModel(IRoasterRequestService roasterRequestService)
            => _roasterRequestService = roasterRequestService ?? throw new ArgumentNullException(nameof(IRoasterRequestService));

        public async Task OnGetAsync(Guid id)
        {
            Role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadata.role"].ToString();
            request = await _roasterRequestService.FetchSingleRoasterRequestByIdAsync(id);
        }

        public async Task<IActionResult> OnPostProcessAsync()
        {
            try
            {
                //TODO: following code illustrates errors handling 
                request.Roaster.Picture = builders.BytePictureBuilder.GetBytePicture(Picture);
                /*var result =*/
                await _roasterRequestService.UpdateRoasterRequestAsync(request);
                //if (!result.success)
                //    return badrequest("ошибка сохранения данных");
                return RedirectToPage("RoasterRequests");
            }
            catch (Exception)
            {
                // TODO: глобальное логирование
                return BadRequest();
            }
        }

        class ServiceResult
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public Exception Exception { get; set; }

            public static ServiceResult Fail(string message)
                => new ServiceResult { Success = false, Message = message };
        }
    }
}