﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Library
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                null,
                "",
                new
                {
                    controller = "Book",
                    action = "List",
                    genre = (string)null,
                    page = 1
                }
                );

            routes.MapRoute(
                null,
                "page{page}",
                new { controller = "Book", action = "List", genre = (string)null },
                new { page = @"\d+" }
                );

            routes.MapRoute(
                null,
                "{genre}",
                new { controller = "Book", action = "List", page = 1 }
                );

            routes.MapRoute(
                null,
                "{genre}/page{page}",
                new { controller = "Book", action = "List" },
                new { page = @"\d+" }
                );

            routes.MapRoute(
            null,
            "{controller}/{action}/{ID}",
            new { controller = "Book", action = "Description", ID = UrlParameter.Optional }
            );

            routes.MapRoute(
                null,
                "{controller}/{action}"
                );
        }
    }
}
