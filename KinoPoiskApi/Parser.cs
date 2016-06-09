using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using RestSharp;

namespace KinoPoiskApi
{
    public class Parser {
        public IEnumerable<Dictionary<RequiredValueType, string>> GetMovies(IRestResponse response, IEnumerable<RequiredValueType> requiredValues) {
            throw new NotImplementedException();
        }
    }
}
