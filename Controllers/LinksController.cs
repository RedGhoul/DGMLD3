using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DGMLD3.Data.CONTEXT;
using DGMLD3.Data.RDMS;
using DGMLD3.Data.VIEW;

namespace DGMLD3.Controllers
{
    [Authorize]
    public class LinksController : BaseController
    {

        public LinksController(ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager):base(context, userManager)
        {
        }

        // GET: Links
        public async Task<IActionResult> Index(string currentGraphId)
        {
            var curUser = await GetCurrentUser();
            
            var graph = await _context.Graphs.Include(x => x.Creator)
                .Where(x => x.Id == Int32.Parse(currentGraphId) 
                && x.Creator.Id.Equals(curUser.Id)).FirstOrDefaultAsync();

            var links = graph.Links.ToList();

            LinkTableViewModel vm = new LinkTableViewModel
            {
                GraphName = graph.ReadableName,
                GraphLink = graph.GraphLinkURL,
                GraphId = graph.Id,
                Links = links
            };

            return View(vm);
        }

        // GET: Links
        public async Task<IActionResult> Search([FromQuery] string SearchString, [FromQuery] string currentGraphId)
        {
            LinkTableViewModel vm = new LinkTableViewModel();
            ApplicationUser user = await GetCurrentUser();
            var curUser = await GetCurrentUser();

            var graph = await _context.Graphs.Include(x => x.Creator)
                .Where(x => x.Id == Int32.Parse(currentGraphId)
                && x.Creator.Id.Equals(curUser.Id)).FirstOrDefaultAsync();

            var links = graph.Links.ToList();
            return View(vm);
        }
    }
}
