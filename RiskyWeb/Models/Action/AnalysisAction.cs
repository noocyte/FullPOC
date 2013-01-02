using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using RiskyWeb.Models.Analysis;

namespace RiskyWeb.Models.Action
{
    [DataContract]
    public class AnalysisAction
    {
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public DateTime Created { get; set; }
        [DataMember]
        public DateTime Deadline { get; set; }
        [DataMember]
        public Category Category { get; set; }
        [DataMember]
        public List<string> AnalysisIds { get; set; }
    }
}