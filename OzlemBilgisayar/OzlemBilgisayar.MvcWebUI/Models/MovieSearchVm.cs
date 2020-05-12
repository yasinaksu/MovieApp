using OzlemBilgisayar.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OzlemBilgisayar.MvcWebUI.Models
{
    public class MovieSearchVm
    {
        public List<Movie> Movies { get; set; }
        public Pager Pager { get; set; }
        public int MovieCount { get; set; }
        public string q { get; set; }
    }
}