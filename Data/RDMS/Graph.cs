﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DGMLD3.Data.RDMS
{
    public class Graph
    {
        public Graph()
        {
            this.DateCreated = DateTime.Now;
        }
        public int Id { get; set; }
        public ApplicationUser Creator { get; set; }
        public ICollection<Node> Nodes { get; set; }
        public ICollection<Link> Links { get; set; }
        public string Name { get; set; }
        public string ReadableName { get; set; }
        public DateTime DateCreated { get; set; }
        public string GraphLinkURL { get; set; }
        public bool IsPublic { get; set; }
        public string SVG { get; set; }
    }
}
