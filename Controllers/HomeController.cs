using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DGMLD3.Models;
using System.Xml;
using Newtonsoft.Json;
using DGMLD3.QuickType;
using DGMLD3.Data;

namespace DGMLD3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("ChannelManager_HPE_QA.schema.dgml");
            string json = JsonConvert.SerializeXmlNode(doc);
            RootObject sss = RootObject.FromJson(json);
            List<GraphNode> nodes = new List<GraphNode>();
            List<GraphLink> links = new List<GraphLink>();
            Dictionary<string, bool> bag = new Dictionary<string, bool>();
            for (int i = 2; i < sss.DirectedGraph.Nodes.Node.Length+2; i++)
            {
                    nodes.Add(new GraphNode { group = "MAIN", id = sss.DirectedGraph.Nodes.Node[i - 2].Id, name = sss.DirectedGraph.Nodes.Node[i - 2].Id });
            }
            for (int i = 0; i < sss.DirectedGraph.Links.Link.Length; i++)
            {

                    links.Add(new GraphLink { 
                        source = sss.DirectedGraph.Links.Link[i].Source,
                        target = sss.DirectedGraph.Links.Link[i].Target
                    });
            }
            ViewBag.NODES = JsonConvert.SerializeObject(nodes);
            ViewBag.LINKS = JsonConvert.SerializeObject(links);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
