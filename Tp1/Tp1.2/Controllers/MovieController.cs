using Microsoft.AspNetCore.Mvc;
using Tp1._2.Models;

namespace Tp1._2.Controllers
{
    [Route("/Movie")]
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            Movie movie = new Movie()
            {
                Name =
            "movie one"
            };
            List<Movie> movies = new List<Movie>()
            {
                new Movie{Name="movie two"},
                new Movie{Name="movie three"},
            };
            return View(movies);
        }
        [HttpGet("released/{year}/{month}")]
        public IActionResult released(int year, int month)
        {
            return Content("year " + year + " month " + month);
        }

    }
}
