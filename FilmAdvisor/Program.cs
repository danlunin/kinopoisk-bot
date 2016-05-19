using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmAdvisor {
    class Program {
        static void Main(string[] args)
        {
            Request req = new Request(new[] {"abc", "dcv"}, 2000, 7.3, "США");
            var selector = new Selector();

            foreach (var e in selector.SelectWithPreference<Film>(req))
            {
                Console.WriteLine(e);
            }
            Console.Read();
        }
    }
}
