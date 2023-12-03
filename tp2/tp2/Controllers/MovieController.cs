using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tp2.Models;

namespace tp2.Controllers
{
    public class MovieController : Controller
    {
        private readonly AppdbContext _appdbContext;
        public MovieController(AppdbContext appdbContext)
        {
            _appdbContext = appdbContext;
        }
        public IActionResult Index()
        {
            return View(getAllMovies());
        }

        //get request 
        public IActionResult Edit(int id)
        {
            // get movie by id
            var movieToEdit = _appdbContext.movies.Find(id);

            // check whether the movie exsists or not
            if (movieToEdit == null)
            {
                return Content("Movie Not Found");
            }

            return View(movieToEdit);
        }
        //post request
        [HttpPost]
        public IActionResult Edit(int id, Movie updatedMovie)
        {
            // get movie by id
            var movieToEdit = _appdbContext.movies.Find(id);

            if (movieToEdit == null)
            {
                return Content("Movie Not Found");
            }

            // update movie attribute values
            movieToEdit.Name = updatedMovie.Name;

            if (ModelState.IsValid)
            {
                // commiting changes
                _appdbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(movieToEdit);
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return Content("Id Not found");
            var testDetails=getAllMovies().FirstOrDefault(c=>c.Id == id);
            return View(testDetails);
        }
        private IEnumerable<Movie> getAllMovies()
        {
            var movies = _appdbContext.movies.ToList();
            return movies;
        }
        public IActionResult Create()

        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _appdbContext.movies.Add(movie);
                _appdbContext.SaveChanges();
                return RedirectToAction("Index"); // Redirect to a list of movies or another appropriate action
            }
            return View(movie); // return the same view in case of errors
        }
        public IActionResult Delete(int id)
        {
            // get movie by id
            var movieToDelete = _appdbContext.movies.Find(id);

            // check whether the movie exists or not
            if (movieToDelete == null)
            {
                return Content("Movie Not Found");
            }

            return View(movieToDelete);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Movie movie)
        {

            if (movie== null)
            {
                return Content("Movie Not Found");
            }

            // Rremoving the movie
            _appdbContext.movies.Remove(movie);

            // committing changes
            _appdbContext.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
