using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Rental.UI.Utility
{
    public class MuliLanguageRouteHandler : MvcRouteHandler
    {
        //重写了处理路由的方法，在处理时先获得路由的语言
        protected override IHttpHandler GetHttpHandler(System.Web.Routing.RequestContext requestContext)
        {
            string lang = requestContext.RouteData.Values["lang"].ToString();
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(lang);
            return base.GetHttpHandler(requestContext);
        }
    }
}