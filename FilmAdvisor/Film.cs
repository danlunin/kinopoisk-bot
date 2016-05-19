using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmAdvisor {
    public class Film : IFilm {
        public string Name { get; set; }

        public string Year { get; set; }

        public string Country { get; set; }

        public string[] Genre { get; set; }

        public string Rate { get; set; }

        public string Producer { get; set; }

        public Film(string name, string year) {
            Name = name;
            year = Year;
        }
    }
}
