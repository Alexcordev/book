using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace book
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Accueil",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Prets", action = "ListerPrets", id = UrlParameter.Optional}
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Membres", action = "listerTout", id = UrlParameter.Optional}
            );
            routes.MapRoute(
                name: "Ajouter",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Prets", action = "Ajouter", id = UrlParameter.Optional}
            );
            routes.MapRoute(
                name: "Ajout",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Membres", action = "Ajout", id = UrlParameter.Optional}
            );
            routes.MapRoute(
                name: "Retards",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Retards", action = "Ajouter", id = UrlParameter.Optional}
            );
            routes.MapRoute(
                name: "EditPrets",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Prets", action = "Edit", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "EditMembres",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Membres", action = "Edit", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "DelMembres",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Membres", action = "Delete", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "DelPret",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Prets", action = "Delete", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "EffacerMembre",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Membres", action = "Effacer", Code = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "EffacerPret",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Prets", action = "Effacer", Code = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "EditerPrets",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Prets", action = "Editer", Code = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "EditerMembres",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Membres", action = "Editer", Courriel = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ModifierPrets",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Prets", action = "Modifier", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ModifierMembres",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Membres", action = "Modifier", id = UrlParameter.Optional }
            );
            
        }
    }
}