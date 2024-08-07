using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using CineVault.Models;

namespace CineVault.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieDbContext db;

        public MoviesController(MovieDbContext context)
        {
            db = context;
        }

        // GET: Movies
        public IActionResult Index(string sortOrder)
        {
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.FormatSortParm = sortOrder == "Format" ? "format_desc" : "Format";
            ViewBag.LengthSortParm = sortOrder == "Length" ? "length_desc" : "Length";
            ViewBag.ReleaseYearSortParm = sortOrder == "ReleaseYear" ? "year_desc" : "ReleaseYear";
            ViewBag.RatingSortParm = sortOrder == "Rating" ? "rating_desc" : "Rating";

            var movies = from m in db.Movies
                         select m;

            switch (sortOrder)
            {
                case "title_desc":
                    movies = movies.OrderByDescending(m => m.Title);
                    break;
                case "Format":
                    movies = movies.OrderBy(m => m.Format);
                    break;
                case "format_desc":
                    movies = movies.OrderByDescending(m => m.Format);
                    break;
                case "Length":
                    movies = movies.OrderBy(m => m.Length);
                    break;
                case "length_desc":
                    movies = movies.OrderByDescending(m => m.Length);
                    break;
                case "ReleaseYear":
                    movies = movies.OrderBy(m => m.ReleaseYear);
                    break;
                case "year_desc":
                    movies = movies.OrderByDescending(m => m.ReleaseYear);
                    break;
                case "Rating":
                    movies = movies.OrderBy(m => m.Rating);
                    break;
                case "rating_desc":
                    movies = movies.OrderByDescending(m => m.Rating);
                    break;
                default:
                    movies = movies.OrderBy(m => m.Title);
                    break;
            }

            return View(movies.ToList());
        }

        // GET: Movies/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var movie = db.Movies
                .FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,Format,Length,ReleaseYear,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Add(movie);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var movie = db.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Title,Format,Length,ReleaseYear,Rating")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(movie);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var movie = db.Movies
                .FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return db.Movies.Any(e => e.Id == id);
        }
    }
}
