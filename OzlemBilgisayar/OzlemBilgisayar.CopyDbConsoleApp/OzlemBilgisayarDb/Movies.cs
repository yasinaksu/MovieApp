namespace OzlemBilgisayar.CopyDbConsoleApp.OzlemBilgisayarDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Movies
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Movies()
        {
            Categories = new HashSet<Categories>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        [Required]
        [StringLength(3)]
        public string ImdbRate { get; set; }

        [Required]
        public string StoryLine { get; set; }

        [Required]
        [StringLength(255)]
        public string Image { get; set; }

        [Required]
        public string Trailer { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public bool IsActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Categories> Categories { get; set; }
    }
}
