using System;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Lokad.Cloud.Storage;
using RiskyWeb.Models.Analysis;
using RiskyWeb.Models.Action;

namespace RiskyWeb.Controllers
{
    public class ActionController : Controller
    {
        static string ConnectionString = ConfigurationManager.AppSettings["atsConnectionString"];
        static CloudStorageProviders Providers = CloudStorage.ForAzureConnectionString(ConnectionString).BuildStorageProviders();
        const string PartitionKey = "OneCustomer";
        const string TableName = "RiskyWeb_Actions";

        //
        // GET: /Action/

        public ActionResult Index(string partKey = PartitionKey)
        {

            var entities = new CloudTable<AnalysisAction>(Providers.TableStorage, TableName);
            var actions = entities.Get(partKey);
            var actionsView = AutoMapper.Mapper.Map<IEnumerable<CloudEntity<AnalysisAction>>, IEnumerable<AnalysisActionView>>(actions);
            return View(actionsView);
        }
    }
}
