using System.Collections.Generic;
using RestSharp;

namespace FilmAdvisor
{
    public interface IParser
    {
        IEnumerable<IFilm> GetFilms(IRestResponse response);
    }
}
