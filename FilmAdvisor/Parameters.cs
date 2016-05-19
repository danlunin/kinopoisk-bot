
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

        public Parameters(IEnumerable<KeyValuePair<string, object>> p) {
            parameters = new Dictionary<string, object> {
                ["level"] = 7,
                ["from"] = "forma",
                ["result"] = "adv",
                ["m_act[from]"] = "forma",
                ["m_act[what]"] = "content"
            };
            foreach (var param in p) {
                Add(String.Format("m_act[{}]", param.Key), param.Value);
            }
        }

        public void Add(string name, object obj) {
            parameters.Add(name, obj);
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() {
            return ((IEnumerable<KeyValuePair<string, object>>)parameters).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return ((IEnumerable<KeyValuePair<string, object>>)parameters).GetEnumerator();
        }
    }
}
