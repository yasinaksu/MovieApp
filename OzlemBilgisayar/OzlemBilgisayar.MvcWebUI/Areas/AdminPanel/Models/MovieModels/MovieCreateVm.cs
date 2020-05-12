using OzlemBilgisayar.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Models.MovieModels
{
    public class MovieCreateVm
    {
        public bool IsActive { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ImdbRate { get; set; }
        public string StoryLine { get; set; }

        [AllowHtml]
        public string Trailer { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public int[] Categories { get; set; }

    }
}