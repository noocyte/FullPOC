using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using AutoMapper;
using Lokad.Cloud.Storage;
using RiskyWeb.Common;
using RiskyWeb.Models.Analysis;

namespace RiskyWeb.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        static string ConnectionString = ConfigurationManager.AppSettings["atsConnectionString"];
        static CloudStorageProviders Providers = CloudStorage.ForAzureConnectionString(ConnectionString).BuildStorageProviders();
        const string PartitionKey = "OneCustomer";
        const string TableName = "RiskyWeb_Analysis";



        public ActionResult Index(string partKey = PartitionKey)
        {

            var entities = new CloudTable<Analysis>(Providers.TableStorage, TableName);
            var analyses = entities.Get(partKey);
            var analysesView = AutoMapper.Mapper.Map<IEnumerable<CloudEntity<Analysis>>, IEnumerable<AnalysisView>>(analyses);
            return View(analysesView);
        }

        //
        // GET: /Home/Details/{guid}

        public ActionResult Details(string id, string partKey = PartitionKey)
        {
            AnalysisView view = GetOneAnalysis(id, partKey);
            return View(view);
        }

        //
        // GET: /Home/CreateMassive

        public ActionResult CreateMassive()
        {
            return View();
        }


        //
        // POST: /Home/CreateMassive
        [HttpPost]
        public ActionResult CreateMassive(string partKey)
        {
            for (int i = 0; i < 100; i++)
            {
                var analysis = new Analysis()
                {
                    Created = DateTime.UtcNow,
                    Title = "Some title - " + i.ToString(),
                    Description = "Some description - " + i.ToString()
                };
                SaveAnalysis(analysis, partKey);
            }
            return View();
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Home/Create

        [HttpPost]
        public ActionResult Create(AnalysisCreate createdAnalysis)
        {
            try
            {
                var analysis = Mapper.Map<Analysis>(createdAnalysis);
                analysis.Created = DateTime.UtcNow;
                SaveAnalysis(analysis);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private static void SaveAnalysis(Analysis analysis, string partKey = PartitionKey)
        {
            var rowkey = ShortGuid.NewGuid().Value;
            var cloudObject = new CloudEntity<Analysis>
            {
                PartitionKey = partKey,
                RowKey = rowkey,
                Value = analysis
            };

            var entities = new CloudTable<Analysis>(Providers.TableStorage, TableName);
            entities.Insert(cloudObject);
        }

        //
        // GET: /Home/Edit/{guid}

        public ActionResult Edit(string id, string partKey = PartitionKey)
        {
            AnalysisView view = GetOneAnalysis(id, partKey);
            return View(view);
        }

        private static AnalysisView GetOneAnalysis(string id, string partKey)
        {
            AnalysisView view = null;
            var entities = new CloudTable<Analysis>(Providers.TableStorage, TableName);
            var analysis = entities.Get(partKey, id);
            if (analysis.HasValue)
                view = Mapper.Map<AnalysisView>(analysis.Value);
            return view;
        }

        //
        // POST: /Home/Edit/5

        [HttpPost]
        public ActionResult Edit(AnalysisView updatedAnalysis)
        {
            try
            {
                var analysis = Mapper.Map<Analysis>(updatedAnalysis);
                var cloudObject = new CloudEntity<Analysis>()
                {
                    PartitionKey = updatedAnalysis.AnalysisPartKey,
                    RowKey = updatedAnalysis.AnalysisRowKey,
                    Value = analysis
                };
                var entities = new CloudTable<Analysis>(Providers.TableStorage, TableName);
                entities.Update(cloudObject);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Home/Delete/{guid}

        public ActionResult Delete(string id, string partKey = PartitionKey)
        {
            AnalysisView view = GetOneAnalysis(id, partKey);
            return View(view);
        }

        //
        // POST: /Home/Delete/{guid}

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteEntity(string id, string partKey = PartitionKey)
        {
            try
            {
                var entities = new CloudTable<Analysis>(Providers.TableStorage, TableName);
                entities.Delete(partKey, id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
