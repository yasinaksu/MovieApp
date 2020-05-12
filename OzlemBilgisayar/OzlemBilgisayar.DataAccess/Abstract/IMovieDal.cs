using Core.DataAccess;
using OzlemBilgisayar.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzlemBilgisayar.DataAccess.Abstract
{
    public interface IMovieDal : IEntityRepository<Movie>
    {
        Movie AddWithCategories(Movie movie, params int[] categoryIds);
        Movie UpdateWithCategories(Movie movie, params int[] categoryIds);
    }
}
