using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication5.Models;

namespace MvcApplication5.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index(SimpleModel model)
        {
			string content = string.Format("Controller: {0}<br/>Action: {1}", model.Controller, model.Action);
			return new RawContentResult(content);
        }

    }

	public class RawContentResult : ActionResult
	{
		public string RawData { get; private set; }

		public RawContentResult(string rawData)
		{
			RawData = rawData;
		}

		public override void ExecuteResult(ControllerContext context)
		{
			context.RequestContext.HttpContext.Response.Write(RawData);
		}
	}
}
