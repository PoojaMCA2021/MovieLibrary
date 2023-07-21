using MovieLibrary.Exceptions;
using MovieLibrary.Model;
using MovieLibrary.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary
{

    public class MovieManager
    {
        readonly string _filePath;
        readonly int MAX_MOVIE = 5;
        
        public List<Movie> movies;

        //Constructor
        public MovieManager(string path) 
        {
            _filePath = path;
            movies= new List<Movie>();
            LoadMovies();
        }
        // Property return maximum movies can store
        public int MAXMOVIE
        {
            get { return MAX_MOVIE; }
        }
        // populate movies list with file data

        public void LoadMovies()
        {
            movies = DataSerializer.BinaryDeserialize(_filePath);
        }
        // save movies list to file 
        public void SaveMovies() 
        {
            File.WriteAllText(_filePath, String.Empty);
            DataSerializer.BinarySerialize(movies, _filePath);
        }
        // Return list of movies if have any movie otherwise throw exception
        public List<Movie> GetMovies()
        {
            if(movies.Count>0) return movies;
            throw new NullAccessException("No Movie To Display!");
        }
        // Add movies to list if it does not meet limit
        public void AddMovie(string title,string genre,string directorName,int yearOfRelease)
        {
            if (checkAvalibilty())
            {
                movies.Add(new Movie(title, genre, directorName, yearOfRelease));
                SaveMovies();
            }
            else 
                throw new ListOverflowException("List Overflow! You have reached to maximum limit!!");
           
        }
        // Check space remaining to store movies

        private bool checkAvalibilty()
        {
            return (movies.Count) <MAXMOVIE;
        }
        // return movie count
        public int GetMovieCount()
        {
            return movies.Count;
        }
        // Find movie by year if present otherwise throw Null Exception
        public List<Movie> FindMovie(int yearOfReleae)
        {
           List<Movie> searchYear=movies.FindAll(m => m.YearOfRelease == yearOfReleae);
            if (searchYear.Count>0)
                return searchYear;
            throw new NullAccessException("Movie Not Found!");
            
        }
        // Find movie by name if present otherwise throw Null Exception
        public List<Movie> FindMovie(string title)
        {
            List<Movie> searchTitle = movies.FindAll(m => m.Title== title);
            if (searchTitle.Count>0)
                return searchTitle;
            throw new NullAccessException("Movie Not Found!");
          
        }
        //Remove movie by name if present 
        public bool ClearMovies(string title)
        {
            try
            {
                List<Movie> searchMovie = FindMovie(title);
                movies.RemoveAll(m=>searchMovie.Any(a=>a.Title == m.Title));
               
                SaveMovies();
                return true;

            }catch(NullAccessException e)
            {
                Console.Write("--------------------\n");
                Console.WriteLine(e.Message);
                Console.Write("--------------------\n");
                return false;
            }
        }
        //Delete all movies
        public bool DeleteAllMovies()
        {
            if (movies.Count > 0)
            {
                movies.Clear();
                SaveMovies();
                return true;
            }
            throw new NullAccessException("No Movie To Delete!");
        }
    }
}
