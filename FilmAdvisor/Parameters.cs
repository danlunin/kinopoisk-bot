
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmAdvisor {
    public class Parameters : IParameters {
        private Dictionary<string, object> parameters { get; }

        public Parameters() {
            //эти нужны всегда
            parameters = new Dictionary<string, object> {
                ["level"] = 7,
                ["from"] = "forma",
                ["result"] = "adv",
                ["m_act[from]"] = "forma",
                ["m_act[what]"] = "content"
            };
        }

        public void SetName(string name) {
            parameters["m_act[find]"] = name;
        }

        public void SetYear(int year) {
            parameters["m_act[year]"] = year;
        }

        public void SetGenre(int genre) {
            parameters["m_act[genre][]"] = genre;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() {
            return ((IEnumerable<KeyValuePair<string, object>>)parameters).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return ((IEnumerable<KeyValuePair<string, object>>)parameters).GetEnumerator();
        }
    }
}
