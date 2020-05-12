namespace OzlemBilgisayar.CopyDbConsoleApp.MovieListDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Movie")]
    public partial class Movie
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public string Summary { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string Photo { get; set; }

        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
