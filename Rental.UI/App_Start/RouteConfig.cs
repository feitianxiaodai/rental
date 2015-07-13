using Rental.UI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Rental.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //添加新的路由协议 默认的语言为中文
            routes.Add(new Route("{lang}/{controller}/{action}/{id}", new RouteValueDictionary(new
            {
                lang = "zh-CN",
                controller = "Home",
                action = "Index",
                id = UrlParameter.Optional
            }),
                new MuliLanguageRouteHandler()
            ));

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}