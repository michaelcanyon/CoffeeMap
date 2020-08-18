using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeMapServer.Pages.Admin.TagViews
{
    public class DeleteTagModel : PageModel
    {
        private readonly ITagRepository tagRepository;
        private readonly IRoasterTagRepository roasterTagRepository;
        public DeleteTagModel(ITagRepository _tagRepository, IRoasterTagRepository _roasterTagRepository)
        {
            tagRepository = _tagRepository;
            roasterTagRepository = _roasterTagRepository;
        }
        public async Task<IActionResult> OnGet(int? id)
        {
            try
            {
                var tagId = Convert.ToInt32(id);
                var removablePairs = await roasterTagRepository.GetPairsByTagId(tagId);
                if(removablePairs.Count()>0)
                foreach (var i in removablePairs)
                        await roasterTagRepository.Delete(i.RoasterId, i.TagId);
                await tagRepository.Delete(tagId);
                return RedirectToPage("Tags");
            }
            catch 
            {
                return RedirectToPage("Tags");
            }   
        }
    }
}
