using DGMLD3.Data;
using DGMLD3.QuickType;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace DGMLD3.Services
{
    public static class GraphMapperService
    {
        public static (List<GraphNode>, List<GraphLink>) GenerateD3NetworkDBSchema(Stream file, string DGML_Type_ID)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            string json = JsonConvert.SerializeXmlNode(doc);
            RootObject sss = RootObject.FromJson(json);
            List<GraphNode> nodes = new List<GraphNode>();
            List<GraphLink> links = new List<GraphLink>();
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

            return (nodes, links);
        }


        public static Graph MapToNewGraphInDB(List<GraphNode> nodes, List<GraphLink> links)
        {
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

            return newGraph;
        }

        public static (List<GraphNode>, List<GraphLink>) MapGraphToDTOs(Graph graph)
        {
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

            return (nodes, links);
        }
    }
}
