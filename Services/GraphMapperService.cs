using DGMLD3.Data;
using DGMLD3.Data.DTO;
using DGMLD3.Data.RDMS;
using DGMLD3.QuickType;
using DGMLD3.QuickType.CodeMapConversion;
using DGMLD3.QuickType.DBMapConversion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Category1 = DGMLD3.QuickType.CodeMapConversion.Category;

namespace DGMLD3.Services
{
    public static class GraphMapperService
    {
        public static (List<GraphNodeDTO>, List<GraphLinkDTO>) GenerateD3Network(Stream file, string DGML_Type_ID)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            string json = JsonConvert.SerializeXmlNode(doc);
            List<GraphNodeDTO> nodes = new List<GraphNodeDTO>();
            List<GraphLinkDTO> links = new List<GraphLinkDTO>();
            if (DGML_Type_ID.Equals("DB"))
            {
                DbMap CurrentDBMap = DbMap.FromJson(json);
             
                for (int i = 0; i < CurrentDBMap.DirectedGraph.Nodes.Node.Length; i++)
                {
                   
                        if (CurrentDBMap.DirectedGraph.Nodes.Node[i].Category == Id.Table)
                        {
                            nodes.Add(new GraphNodeDTO
                            {
                                color = "#7a89de",
                                group = "1",
                                id = CurrentDBMap.DirectedGraph.Nodes.Node[i].Id,
                                name = CurrentDBMap.DirectedGraph.Nodes.Node[i].Id
                            });
                        }
                        else if (CurrentDBMap.DirectedGraph.Nodes.Node[i].Category == Id.ForeignKey)
                        {
                            nodes.Add(new GraphNodeDTO
                            {
                                color = "#e3176f",
                                group = "1",
                                id = CurrentDBMap.DirectedGraph.Nodes.Node[i].Id,
                                name = CurrentDBMap.DirectedGraph.Nodes.Node[i].Id
                            });
                        }
                        else
                        {
                            nodes.Add(new GraphNodeDTO
                            {
                                color = "#9f85a6",
                                group = "1",
                                id = CurrentDBMap.DirectedGraph.Nodes.Node[i].Id,
                                name = CurrentDBMap.DirectedGraph.Nodes.Node[i].Id
                            });
                        }
                }
                for (int i = 0; i < CurrentDBMap.DirectedGraph.Links.Link.Length; i++)
                {

                    links.Add(new GraphLinkDTO
                    {
                        source = CurrentDBMap.DirectedGraph.Links.Link[i].Source,
                        target = CurrentDBMap.DirectedGraph.Links.Link[i].Target
                    });
                }
            }
            else if (DGML_Type_ID.Equals("CODE"))
            {
                CodeMap CurrentDBMap = CodeMap.FromJson(json);
                for (int i = 0; i < CurrentDBMap.DirectedGraph.Nodes.Node.Length; i++)
                {
                    nodes.Add(new GraphNodeDTO
                    {
                        color = "#7a89de",
                        group = "1",
                        id = CurrentDBMap.DirectedGraph.Nodes.Node[i].Id,
                        name = CurrentDBMap.DirectedGraph.Nodes.Node[i].Label
                    });
                    
                }
                for (int i = 0; i < CurrentDBMap.DirectedGraph.Links.Link.Length; i++)
                {

                    links.Add(new GraphLinkDTO
                    {
                        source = CurrentDBMap.DirectedGraph.Links.Link[i].Source,
                        target = CurrentDBMap.DirectedGraph.Links.Link[i].Target
                    });
                }

               
            }
            return (nodes, links);
        }


        public static Graph MapToNewGraphInDB(List<GraphNodeDTO> nodes, List<GraphLinkDTO> links)
        {
            Graph newGraph = new Graph
            {
                Name = Guid.NewGuid().ToString(),
                Nodes = new List<Data.RDMS.Node>(),
                Links = new List<Data.RDMS.Link>()
            };
            foreach (var item in nodes)
            {
                newGraph.Nodes.Add(new Data.RDMS.Node
                {
                    group = item.group,
                    name = item.name,
                    color = item.color,
                    NodeId = item.id
                });
            }
            foreach (var item in links)
            {
                newGraph.Links.Add(new Data.RDMS.Link
                {
                    source = item.source,
                    target = item.target,
                });
            }

            return newGraph;
        }

        public static (List<GraphNodeDTO>, List<GraphLinkDTO>) MapGraphToDTOs(Graph graph)
        {
            List<GraphNodeDTO> nodes = new List<GraphNodeDTO>();
            List<GraphLinkDTO> links = new List<GraphLinkDTO>();
            foreach (var item in graph.Links)
            {
                links.Add(new GraphLinkDTO
                {
                    source = item.source,
                    target = item.target
                });
            }

            foreach (var item in graph.Nodes)
            {
                nodes.Add(new GraphNodeDTO
                {
                    id = item.NodeId,
                    group = item.group,
                    name = item.name,
                    color = item.color,
                });
            }

            return (nodes, links);
        }
    }
}
