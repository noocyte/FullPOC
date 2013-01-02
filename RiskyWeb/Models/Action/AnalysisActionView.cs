using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RiskyWeb.Models.Analysis;

namespace RiskyWeb.Models.Action
{
    public class AnalysisActionView
    {
        public string AnalysisActionRowKey { get; set; }
        public string AnalysisActionPartKey { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deadline { get; set; }
        public string CategoryName { get; set; }
        public List<string> AnalysisIds { get; set; }
    }
}