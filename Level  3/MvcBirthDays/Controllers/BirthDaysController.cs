using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcBirthDays.Data;
using MvcBirthDays.Models;

namespace MvcBirthDays.Controllers
{
    public class BirthDaysController : Controller
    {
        private readonly MvcBirthDaysContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public BirthDaysController(MvcBirthDaysContext context, IWebHostEnvironment env)
        {
            _context = context;
            _appEnvironment = env;
        }

        // GET: BirthDays
        public async Task<IActionResult> Index()
        {
            if (_context.Person == null)
            {
                return Problem("Entity set MvcBirthDays.Person is null.");
            }

            var today = DateTime.Today;

            // Extract all persons from the database
            var persons = await _context.Person.ToListAsync();
            // Perform sorting in memory
            var sortedPersons = persons.Where(b =>
                {
                    var nextBirthday = b.Birthdate.AddYears(today.Year - b.Birthdate.Year);
                    if (nextBirthday < today)
                    {
                        nextBirthday = nextBirthday.AddYears(1);
                    }
                    return (nextBirthday - today).TotalDays < 10;
                })
                .OrderBy(b =>
                {
                    var nextBirthday = b.Birthdate.AddYears(today.Year - b.Birthdate.Year);
                    if (nextBirthday < today)
                    {
                        nextBirthday = nextBirthday.AddYears(1);
                    }
                    return (nextBirthday - today).TotalDays;
                })
                .ToList();
            return View(sortedPersons);
        }
        [HttpGet]
        public async Task<IActionResult> ShowAll()
        {
            if (_context.Person == null)
            {
                return Problem("Entity set MvcBirthDays.Person is null.");
            }

            return View(await _context.Person.ToListAsync());
        }

        // GET: BirthDays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: BirthDays/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BirthDays/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Birthdate")] Person person, IFormFile image)
        {
            // if (ModelState.IsValid)
            {
                if (image != null)
                {
                    byte[] p1 = null;
                    using (var fs1 = image.OpenReadStream())
                    using (var ms1 = new MemoryStream())
                    {
                        fs1.CopyTo(ms1);
                        p1 = ms1.ToArray();
                    }
                    person.ImageData = p1;
                }


                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: BirthDays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: BirthDays/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Birthdate,ImageData")] Person person, IFormFile image)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            // if (ModelState.IsValid)
            {
                try
                {
                    if (image != null)
                    {
                        byte[] p1 = null;
                        using (var fs1 = image.OpenReadStream())
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                        person.ImageData = p1;
                    }
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
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
            return View(person);
        }

        // GET: BirthDays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: BirthDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.Person.FindAsync(id);
            if (person != null)
            {
                _context.Person.Remove(person);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.Id == id);
        }
    }
}
