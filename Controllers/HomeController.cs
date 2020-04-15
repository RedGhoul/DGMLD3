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
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public IActionResult Upload(UploadViewModel model)
        {
            // Extract file name from whatever was posted by browser
            var fileName = System.IO.Path.GetFileName(model.file.FileName);

            // If file with same name exists delete it
            if (System.IO.File.Exists(fileName))
            {
                System.IO.File.Delete(fileName);
            }

            // Create new local file and copy contents of uploaded file
            //using (var localFile = System.IO.File.OpenWrite(fileName))
            using (var uploadedFile = model.file.OpenReadStream())
            {
                //uploadedFile.CopyTo(localFile);
                List<GraphNode> nodes;
                List<GraphLink> links;
                GenerateD3Network(uploadedFile, out nodes, out links, model.DGML_Type_ID);
                ViewBag.NODES = JsonConvert.SerializeObject(nodes);
                ViewBag.LINKS = JsonConvert.SerializeObject(links);
            }

            ViewBag.Message = "File successfully uploaded";

            return View("ViewNetwork");
        }

        public IActionResult ViewNetwork()
        {

            return View();
        }

        private static void GenerateD3Network(Stream file,out List<GraphNode> nodes, out List<GraphLink> links, string DGML_Type_ID)
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
