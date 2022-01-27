using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using enno2.Data;
using enno2.Models;
using System.IO;

namespace enno2.Areas.admin.Controllers
{
    [Area("admin")]
    public class TeamsController : Controller
    {
        private readonly AppDbContext _context;

        public TeamsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: admin/Teams
        public async Task<IActionResult> Index()
        {
            return View(await _context.teams.ToListAsync());
        }

        // GET: admin/Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.teams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: admin/Teams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Team team)
        {
            if (ModelState.IsValid)
            {
                if(team.ImageFile != null && team.ImageFile.ContentType == "image/jpeg" || team.ImageFile.ContentType == "image/jpg" || team.ImageFile.ContentType == "image/png")
                {
                    if(team.ImageFile.Length <= 2000000)
                    {
                        string filename = Guid.NewGuid() + "-" + team.ImageFile.FileName;
                        string filepath = Path.Combine("wwwroot/uploads", filename);

                        using(var stream= new FileStream(filepath, FileMode.Create))
                        {
                            team.ImageFile.CopyTo(stream);
                        }

                        team.Image = filename;

                        _context.Add(team);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }

                
            }
            return RedirectToAction("Index");
        }

        // GET: admin/Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        // POST: admin/Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Team team)
        {
            if (id != team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string filename = Guid.NewGuid() + "-" + team.ImageFile.FileName;
                    string filepath = Path.Combine("wwwroot/uploads", filename);


                    using(var stream = new FileStream(filepath, FileMode.Create))
                    {
                        team.ImageFile.CopyTo(stream);
                    }

                    team.Image = filename;


                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.Id))
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
            return View(team);
        }

        // GET: admin/Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.teams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: admin/Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _context.teams.FindAsync(id);
            _context.teams.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
            return _context.teams.Any(e => e.Id == id);
        }
    }
}
