using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using DGMLD3.Data;
using DGMLD3.Models;
using DGMLD3.QuickType;
using DGMLD3.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace DGMLD3.Controllers
{
    public class DGMLController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly GraphRedisService _graphRedisService;
        public DGMLController(ApplicationDbContext context, GraphRedisService graphRedisService)
        {
            _context = context;
            _graphRedisService = graphRedisService;
        }
        public IActionResult Upload()
        {
            UploadViewModel model = new UploadViewModel();

            return View(model);
        }


        [HttpPost] 
        public async Task<IActionResult> Upload(UploadViewModel model)
        {
            var fileName = Path.GetFileName(model.file.FileName);
            if(Path.GetExtension(fileName).Equals(".dgml")){
                try
                {
                    using (var uploadedFile = model.file.OpenReadStream())
                    {
                        (List<GraphNode> nodes, List<GraphLink> links) = GraphMapperService.GenerateD3NetworkDBSchema(uploadedFile, model.DGML_Type_ID);

                        Graph newGraph = GraphMapperService.MapToNewGraphInDB(nodes, links);
                        _context.Graphs.Add(newGraph);
                        await _context.SaveChangesAsync();

                        string NODES = JsonConvert.SerializeObject(newGraph);
                        string LINKS = JsonConvert.SerializeObject(links);

                        ViewBag.NODES = NODES;
                        ViewBag.LINKS = LINKS;

                        await _graphRedisService.SaveGraphToCache(newGraph);

                        ViewBag.LINK_URL = "https://" + Request.Host.Value + "/DGML/ViewNetwork?graphName=" + newGraph.Name;
                    }
                }
                catch (Exception e)
                {
                    model.ErrorMsg = "Could not read file";
                    return View("Upload");
                }
               
            }
            else
            {
                model.ErrorMsg = "Please Enter a valid .dgml";
                return View("Upload");
            }
            
            return View("Network");
        }


        [HttpGet]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> ViewNetwork([FromQuery]string graphName)
        {
            ViewBag.LINK_URL = "https://" + Request.Host.Value + "/DGML/ViewNetwork?graphName=" + graphName;

            var (Links,Nodes) = await _graphRedisService.GetGraphFromCache(graphName);
            if (!string.IsNullOrEmpty(Links) && !string.IsNullOrEmpty(Nodes))
            {
                ViewBag.NODES = Nodes;
                ViewBag.LINKS = Links;
            }
            else
            {
                Graph graph = await _context.Graphs.Where(x => x.Name.Equals(graphName)).Include(x => x.Links).
                   Include(x => x.Nodes).
                   FirstOrDefaultAsync();
                (List<GraphNode> nodes, List<GraphLink> links) = GraphMapperService.MapGraphToDTOs(graph);
                ViewBag.NODES = JsonConvert.SerializeObject(nodes);
                ViewBag.LINKS = JsonConvert.SerializeObject(links);
            }
            return View("Network");

        }
    }
}