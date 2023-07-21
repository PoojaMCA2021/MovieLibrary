using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Model
{
    [Serializable]
    public class Movie
    {
        

        public string Title { get;  }
        public string Genere { get; }
        public string Director{ get;  }
        public int YearOfRelease { get; }

        public Movie(string title, string genere, string director, int yearOfRelease)
        {
            Title = title;
            Genere = genere;
            Director = director;
            YearOfRelease = yearOfRelease;
        }

        public Movie()
        {
        }
    }
}
