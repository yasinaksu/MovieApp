using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OzlemBilgisayar.MvcWebUI.Models
{
    public class GetMovieDetailsVm
    {
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string ImdbRate { get; set; }
        public string StoryLine { get; set; }
        public string Image { get; set; }
        public string Trailer { get; set; }
        public List<string> Categories { get; set; }
    }
}