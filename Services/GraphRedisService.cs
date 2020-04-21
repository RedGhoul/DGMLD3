using DGMLD3.Data;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DGMLD3.Services
{
    public class GraphRedisService
    {
        private readonly IDistributedCache _distributedCache;
        public GraphRedisService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public async Task SaveGraphToCache(Graph newGraph)
        {
            var options = new DistributedCacheEntryOptions();
            options.SetSlidingExpiration(TimeSpan.FromDays(400));
            string GRAPH = JsonConvert.SerializeObject(newGraph);
            string cacheGraphKey = "GRAPH_" + newGraph.Name;
            await _distributedCache.SetStringAsync(cacheGraphKey, GRAPH, options);
        }

        public async Task<(string, string)> GetGraphFromCache(string graphName)
        {
            string cacheKeyLink = "GRAPH_" + graphName;
            string GRAPH = await _distributedCache.GetStringAsync(cacheKeyLink);
            Graph graph = JsonConvert.DeserializeObject<Graph>(GRAPH);
            return (JsonConvert.SerializeObject(graph.Links),JsonConvert.SerializeObject(graph.Nodes));
        }
    }
}
