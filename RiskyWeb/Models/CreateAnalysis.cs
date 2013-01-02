using System;
using System.Collections.Generic;
using System.Linq;
using Lokad.Cloud.Storage;
using RiskyWeb.Models.Action;
using RiskyWeb.Models.Analysis;
namespace RiskyWeb.Models
{
    static class CreateAnalysis
    {
        static List<CloudEntity<AnalysisAction>> actions;
        static List<CloudEntity<Category>> categories;
        static List<CloudEntity<RiskyWeb.Models.Analysis.Analysis>> analyses;

        internal static IEnumerable<CloudEntity<RiskyWeb.Models.Analysis.Analysis>> CreateAnalyses(int numberOfAnalyses)
        {
            categories = new List<CloudEntity<Category>>();
            actions = new List<CloudEntity<AnalysisAction>>();
            analyses = new List<CloudEntity<RiskyWeb.Models.Analysis.Analysis>>();
            PopulateCategories();
            PopulateActions();
            PopulateAnalyses();

            return analyses;
        }

        private static void PopulateAnalyses()
        {
            analyses.Add(new CloudEntity<RiskyWeb.Models.Analysis.Analysis>()
            {
                PartitionKey = "analyses",
                RowKey = new Guid().ToString(),
                Value = new RiskyWeb.Models.Analysis.Analysis()
                {
                    Categories = new List<Category>(categories.Select(c => c.Value).ToList()),
                    Created = DateTime.Now,
                    Description = "Analysis by JNY",
                    Title = "First",
                    ActionIds = new List<string>(actions.Select(a => a.RowKey))
                }
            });

            analyses.Add(new CloudEntity<RiskyWeb.Models.Analysis.Analysis>()
            {
                PartitionKey = "analyses",
                RowKey = new Guid().ToString(),
                Value = new RiskyWeb.Models.Analysis.Analysis()
                {
                    Categories = new List<Category>(categories.Select(c => c.Value).ToList()),
                    Created = DateTime.Now,
                    Description = "Analysis by ATL",
                    Title = "Second",
                    ActionIds = new List<string>(actions.Select(a => a.RowKey))
                }
            });

            // connect action and analysis
            foreach (var action in actions)
            {
                action.Value.AnalysisIds = new List<string>();
                action.Value.AnalysisIds.AddRange(analyses.Select(a => a.RowKey));
            }
        }

        private static void PopulateCategories()
        {
            categories.Add(new CloudEntity<Category>()
            {
                PartitionKey = "categories",
                RowKey = new Guid().ToString(),
                Value = new Category()
                {
                    Description = "First Category"
                }
            });

            categories.Add(new CloudEntity<Category>()
            {
                PartitionKey = "categories",
                RowKey = new Guid().ToString(),
                Value = new Category()
                {
                    Description = "Second Category"
                }
            });

            categories.Add(new CloudEntity<Category>()
            {
                PartitionKey = "categories",
                RowKey = new Guid().ToString(),
                Value = new Category()
                {
                    Description = "Third Category"
                }
            });
        }

        private static void PopulateActions()
        {

            actions.Add(new CloudEntity<AnalysisAction>()
            {
                PartitionKey = "actions",
                RowKey = new Guid().ToString(),
                Value = new AnalysisAction()
                {
                    Category = categories.First().Value,
                    Created = DateTime.Now,
                    Deadline = DateTime.Now.AddDays(14),
                    Title = "First action"
                }
            });


            actions.Add(new CloudEntity<AnalysisAction>()
            {
                PartitionKey = "actions",
                RowKey = new Guid().ToString(),
                Value = new AnalysisAction()
                {
                    Category = categories.Last().Value,
                    Created = DateTime.Now,
                    Deadline = DateTime.Now.AddDays(7),
                    Title = "Second action"
                }
            });
        }
    }
}