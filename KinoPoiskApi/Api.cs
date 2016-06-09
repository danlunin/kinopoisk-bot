using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoPoiskApi {
    public class Api {
        private Parameters searchParameters;
        private List<RequiredValueType> requiredValues;
        private Requester requester;
        private Parser parser;

        public Api() {
            searchParameters = new Parameters();
            requiredValues = new List<RequiredValueType>();
            requester = new Requester();
            parser = new Parser();
        }

        public Api AddSearchParameter(SearchParameterType type, string value) {
            searchParameters.Add(type, value);
            return this;
        }

        public Api AddRequiredValue(RequiredValueType valueType) {
            requiredValues.Add(valueType);
            return this;
        }

        public IEnumerable<M> Get<M>(int v) where M : IMovie {
            var response = requester.Search(searchParameters);
            var movieDicts = parser.GetMovies(response, requiredValues);
            foreach (var dict in movieDicts) {
                yield return DictTo<M>(dict);
            }
        }

        public M DictTo<M>(Dictionary<RequiredValueType, string> dict) where M : IMovie {
            throw new NotImplementedException();
        }
    }
}
