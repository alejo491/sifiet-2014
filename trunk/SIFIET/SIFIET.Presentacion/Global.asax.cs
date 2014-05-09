using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
//using log4net;

namespace SIFIET.Presentacion
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //private static readonly ILog log = LogManager.GetLogger("Application");
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));
        }
        /*
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            og.Debug("-------------------------");
            log.Error("Exception -\n"+ex);
            log.Debug("-------------------------");
        }*/
    }
}
