using System;
using System.Web.Http;
using System.Web.Mvc;
using WebApiTest.Areas.HelpPage.ModelDescriptions;

namespace WebApiTest.Areas.HelpPage.Controllers
{
    public class HelpController : Controller
    {
        private const string ErrorViewName = "Error";


        public HelpController()
            : this(GlobalConfiguration.Configuration)
        {
        }
        public HelpController(HttpConfiguration config)
        {
            Configuration = config;
        }


        // PROPERTIES /////////////////////////////////////////////////////////////////////////////
        public HttpConfiguration Configuration { get; }


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        public ActionResult Index()
        {
            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
            return View(Configuration.Services.GetApiExplorer().ApiDescriptions);
        }
        public ActionResult Api(string apiId)
        {
            if (!String.IsNullOrEmpty(apiId))
            {
                var apiModel = Configuration.GetHelpPageApiModel(apiId);
                if (apiModel != null)
                {
                    return View(apiModel);
                }
            }

            return View(ErrorViewName);
        }
        public ActionResult ResourceModel(string modelName)
        {
            if (!String.IsNullOrEmpty(modelName))
            {
                var modelDescriptionGenerator = Configuration.GetModelDescriptionGenerator();
                ModelDescription modelDescription;
                if (modelDescriptionGenerator.GeneratedModels.TryGetValue(modelName, out modelDescription))
                {
                    return View(modelDescription);
                }
            }

            return View(ErrorViewName);
        }
    }
}