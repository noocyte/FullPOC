using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RiskyWeb.Models.Analysis
{
    [DataContract]
    public class Analysis
    {
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public DateTime Created { get; set; }
        [DataMember]
        public List<Category> Categories { get; set; }
        [DataMember]
        public List<string> ActionIds { get; set; }

    }

    [DataContract]
    public class Category
    {
        [DataMember]
        public string Description { get; set; }
    }
}
