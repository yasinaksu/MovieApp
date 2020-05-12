using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To=OzlemBilgisayar.CopyDbConsoleApp.OzlemBilgisayarDb;
using From=OzlemBilgisayar.CopyDbConsoleApp.MovieListDb;

namespace OzlemBilgisayar.CopyDbConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
			try
			{
                var dbFrom = new From.MovieListDbModel();
                var dbTo = new To.OzlemBilgisayarDbContext();

                var movieFroms = dbFrom.Movie.ToList();
                foreach (var from in movieFroms)
                {
                    var to = new To.Movies()
                    {
                        Created = DateTime.Now,
                        Image = from.Photo.Replace("/Images/", ""),
                        IsActive = true,
                        ImdbRate = "7",
                        Modified = DateTime.Now,
                        ReleaseDate = from.ReleaseDate.Value,
                        StoryLine = from.Summary,
                        Title = from.Name,
                        Trailer = "empty"
                    };

                    to.Categories.Add(dbTo.Categories.SingleOrDefault(x => x.Id == from.CategoryId));

                    dbTo.Movies.Add(to);
                }
                
                dbTo.SaveChanges();

                Console.WriteLine("filmler Taşındı");


                Console.ReadLine();
            }
			catch (Exception ex)
			{
                Console.WriteLine(ex.Message);

                Console.ReadLine();
				
			}

        }
    }
}
