using RestSharp;
namespace FilmAdvisor
{
    public interface IRequester
    {
        IRestResponse Search(IParameters parameters);
    }
}
