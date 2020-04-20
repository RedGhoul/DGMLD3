using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using DGMLD3.Data;
using DGMLD3.Models;
using DGMLD3.QuickType;
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
        private readonly IDistributedCache _distributedCache;
        public DGMLController(ApplicationDbContext context, IDistributedCache distributedCache)
        {
            _context = context;
            _distributedCache = distributedCache;
        }
        public IActionResult Upload()
        {

            UploadViewModel model = new UploadViewModel();
            model.DGML_Types = new List<SelectListItem>()
            {
                new SelectListItem("CODE-MAP","CODE"),
                new SelectListItem("DB-MAP","DB")
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Upload(UploadViewModel model)
        {
            var fileName = Path.GetFileName(model.file.FileName);
            using (var uploadedFile = model.file.OpenReadStream())
            {
                //uploadedFile.CopyTo(localFile);
                List<GraphNode> nodes;
                List<GraphLink> links;
                GenerateD3Network(uploadedFile, out nodes, out links, model.DGML_Type_ID);
                Graph newGraph = new Graph();
                newGraph.Name = Guid.NewGuid().ToString();
                newGraph.Nodes = new List<Data.Node>();
                newGraph.Links = new List<Data.Link>();
                foreach (var item in nodes)
                {
                    newGraph.Nodes.Add(new Data.Node
                    {
                        group = item.group,
                        name = item.name,
                        color = item.color,
                    });
                }
                foreach (var item in links)
                {
                    newGraph.Links.Add(new Data.Link
                    {
                        source = item.source,
                        target = item.target,
                    });
                }
                _context.Graphs.Add(newGraph);
                await _context.SaveChangesAsync();
                string NODES = JsonConvert.SerializeObject(nodes);
                string LINKS = JsonConvert.SerializeObject(links);
                
                ViewBag.NODES = NODES;
                ViewBag.LINKS = LINKS;
                
                var options = new DistributedCacheEntryOptions();
                options.SetSlidingExpiration(TimeSpan.FromDays(400));
                
                string cacheKeyLink = "GRAPH_LINKS" + newGraph.Name;
                _distributedCache.SetString(cacheKeyLink, LINKS, options);

                string cacheKeyNodes = "GRAPH_NODES" + newGraph.Name;
                _distributedCache.SetString(cacheKeyNodes, NODES, options);

                ViewBag.LINK_URL = "https://"+Request.Host.Value + "/DGML/ViewNetwork?graphName=" + newGraph.Name;
            }
            return View("Network");
        }

        [HttpGet]
        [ResponseCache(Duration = 30)]
        public IActionResult ViewNetwork([FromQuery]string graphName)
        {
            string cacheKeyLink = "GRAPH_LINKS" + graphName;
            string Links = _distributedCache.GetString(cacheKeyLink);

            string cacheKeyNodes = "GRAPH_NODES" + graphName;
            string Nodes = _distributedCache.GetString(cacheKeyNodes);
            ViewBag.LINK_URL = "https://" + Request.Host.Value + "/DGML/ViewNetwork?graphName=" + graphName;
            if (!string.IsNullOrEmpty(Links) && !string.IsNullOrEmpty(Nodes))
            {
                ViewBag.NODES = Nodes;
                ViewBag.LINKS = Links;
            }
            else
            {
                Graph graph = _context.Graphs.Where(x => x.Name.Equals(graphName)).Include(x => x.Links).
                   Include(x => x.Nodes).
                   FirstOrDefault();

                List<GraphNode> nodes = new List<GraphNode>();
                List<GraphLink> links = new List<GraphLink>();

                foreach (var item in graph.Links)
                {
                    links.Add(new GraphLink
                    {
                        source = item.source,
                        target = item.target
                    });
                }

                foreach (var item in graph.Nodes)
                {
                    nodes.Add(new GraphNode
                    {
                        group = item.group,
                        name = item.name,
                        color = item.color,
                    });
                }
                ViewBag.NODES = JsonConvert.SerializeObject(nodes);
                ViewBag.LINKS = JsonConvert.SerializeObject(links);
                
                
            }
            return View("Network");

        }

        private static void GenerateD3Network(Stream file, out List<GraphNode> nodes, out List<GraphLink> links, string DGML_Type_ID)
        {
            XmlDocument doc = new XmlDocument();
            //MemoryStream stream = new MemoryStream(fil);

            doc.Load(file);
            string json = JsonConvert.SerializeXmlNode(doc);
            RootObject sss = RootObject.FromJson(json);
            nodes = new List<GraphNode>();
            links = new List<GraphLink>();
            Dictionary<string, bool> bag = new Dictionary<string, bool>();
            for (int i = 0; i < sss.DirectedGraph.Nodes.Node.Length; i++)
            {
                if (DGML_Type_ID.Equals("DB"))
                {
                    if (sss.DirectedGraph.Nodes.Node[i].Category == Id.Table)
                    {
                        nodes.Add(new GraphNode
                        {
                            color = "#7a89de",
                            group = "1",
                            id = sss.DirectedGraph.Nodes.Node[i].Id,
                            name = sss.DirectedGraph.Nodes.Node[i].Id
                        });
                    }
                    else if (sss.DirectedGraph.Nodes.Node[i].Category == Id.ForeignKey)
                    {
                        nodes.Add(new GraphNode
                        {
                            color = "#e3176f",
                            group = "1",
                            id = sss.DirectedGraph.Nodes.Node[i].Id,
                            name = sss.DirectedGraph.Nodes.Node[i].Id
                        });
                    }
                    else
                    {
                        nodes.Add(new GraphNode
                        {
                            color = "#9f85a6",
                            group = "1",
                            id = sss.DirectedGraph.Nodes.Node[i].Id,
                            name = sss.DirectedGraph.Nodes.Node[i].Id
                        });
                    }
                }
                else
                {
                    nodes.Add(new GraphNode
                    {
                        color = "#7a89de",
                        group = "1",
                        id = sss.DirectedGraph.Nodes.Node[i].Id,
                        name = sss.DirectedGraph.Nodes.Node[i].Id
                    });
                }



            }
            for (int i = 0; i < sss.DirectedGraph.Links.Link.Length; i++)
            {

                links.Add(new GraphLink
                {
                    source = sss.DirectedGraph.Links.Link[i].Source,
                    target = sss.DirectedGraph.Links.Link[i].Target
                });
            }
        }
    }
}