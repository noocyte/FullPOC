using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiskyWeb.Models.Analysis
{
    public class AnalysisView
    {
        public string AnalysisRowKey { get; set; }
        public string AnalysisPartKey { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
    }
}