using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace LittleBackstage.Infrastructure.Navigation
{
    public class NavigationBuilder
    {
        private List<AdminMenu> Contained { get; set; }

        public NavigationBuilder AddMenu(AdminMenu menu)
        {
            if (Contained == null)
            {
                Contained = new List<AdminMenu>();
            }

            Contained.Add(menu);

            return this;
        }

        public IHtmlString Build(IEnumerable<string> permissions)
        {
            var config = new TemplateServiceConfiguration();
            config.TemplateManager = new WatchingResolvePathTemplateManager(new[] { HttpContext.Current.Server.MapPath("~/Views/Shared") }, new InvalidatingCachingProvider());
            var service = RazorEngineService.Create(config);
            Engine.Razor = service;

            var viewBag = new DynamicViewBag();
            viewBag.AddValue("Permissions", permissions);

            string outputString = Engine.Razor.RunCompile("AdminMenu.cshtml", null,
                Contained.Where(
                    x => permissions.Contains(x.Permission) ||
                         permissions.Contains("*") ||
                         string.IsNullOrEmpty(x.Permission)), viewBag);

            return new HtmlString(outputString);
        }
    }
}
