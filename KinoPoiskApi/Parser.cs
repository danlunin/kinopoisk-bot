﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using RestSharp;

namespace KinoPoiskApi
{
<<<<<<< HEAD
    public class Parser : IParser{
=======
    public class Parser {
        public readonly Regex filmPageUrl;
        
        public Parser() {
            filmPageUrl = new Regex(@"^/film/\d+/$");
        }

>>>>>>> 11f97291ef6ecd77a81e84c318a0e80a02676f5e
        public IEnumerable<Dictionary<RequiredValueType, string>> GetMovies(IRestResponse response, IEnumerable<RequiredValueType> requiredValues) {
            if (IsMoviePage(response.ResponseUri))
                yield return ParseMoviePage(response, requiredValues);
            else
                foreach (var dict in ParseSearchResultPage(response, requiredValues))
                    yield return dict;
            throw new NotImplementedException();
        }

        private IEnumerable<Dictionary<RequiredValueType, string>> ParseSearchResultPage(IRestResponse response, IEnumerable<RequiredValueType> requiredValues) {
            throw new NotImplementedException();
        }

        private Dictionary<RequiredValueType, string> ParseMoviePage(IRestResponse response, IEnumerable<RequiredValueType> requiredValues) {
            throw new NotImplementedException();
        }

        public bool IsMoviePage(Uri url) {
            return filmPageUrl.IsMatch(url.AbsolutePath);
        }
    }
}
