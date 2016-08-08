using System;
using System.Collections.Generic;

namespace LittleBackstage.Infrastructure.Navigation
{
    public class AdminMenu
    {
        public List<AdminMenu> Contained { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Permission { get; set; }
        public AdminMenu AddMenu(AdminMenu menu)
        {
            if (Contained == null)
            {
                Contained = new List<AdminMenu>();
            }

            Contained.Add(menu);

            return this;
        }
    }
}
