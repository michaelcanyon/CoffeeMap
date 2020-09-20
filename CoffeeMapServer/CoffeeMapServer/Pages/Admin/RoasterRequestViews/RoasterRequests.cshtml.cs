using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Infrastructures.Repositories;
using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Admin.RoasterRequestViews
{
    public class RoasterRequestsModel : PageModel
    {
        private readonly IRoasterRequestRepository _roasterRequestRepository;
        private readonly IRoasterRepository _roasterRepository;
        private readonly IAddessRepository _addressRepository;
        public RoasterRequestsModel(IRoasterRequestRepository roasterRequestRepository, IRoasterRepository roasterRepository, IAddessRepository addressRepository)
        {
            _roasterRequestRepository = roasterRequestRepository;
            _roasterRepository = roasterRepository;
            _addressRepository = addressRepository;
        }
        [BindProperty]
        public List<RoasterRequest> roasterRequests { get; set; }
        public string nDeletableRequests { get; set; }
        [BindProperty]
        public string RequestId { get; set; }
        [BindProperty]
        public Address address { get; set; }
        [BindProperty]
        public Roaster roaster { get; set; }
        public string role { get; set; }
        private string[] tags { get; set; }
        public async Task OnGetAsync()
        {
            role = HttpContext.Request.Cookies[".AspNetCore.Meta.Metadta.role"].ToString();
            roasterRequests =await _roasterRequestRepository.GetList();
        }
        public async Task<IActionResult> OnPostDeleteAllAsync() {
           await _roasterRequestRepository.DeleteAll();
            return RedirectToPage("RoasterRecuests");
        }
    }
}
