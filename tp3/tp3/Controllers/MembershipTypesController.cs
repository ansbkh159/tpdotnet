using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TP3net.Models;

namespace TP3net.Controllers
{
    public class MembershipTypesController : Controller
    {
        private readonly AppDbContext _context;

        public MembershipTypesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return _context.MembershipTypes != null ? 
                          View(await _context.MembershipTypes.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.MembershipTypes'  is null.");
        }

        // get membership details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MembershipTypes == null)
            {
                return NotFound();
            }

            var membershipType = await _context.MembershipTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membershipType == null)
            {
                return NotFound();
            }

            return View(membershipType);
        }

        public IActionResult Create()
        {
            return View();
        }

        // create membership

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MembershipType membershipType)
        {
            if (ModelState.IsValid)
            {
                _context.MembershipTypes.Add(membershipType);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(membershipType);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MembershipTypes == null)
            {
                return NotFound();
            }

            var membershipType = await _context.MembershipTypes.FindAsync(id);
            if (membershipType == null)
            {
                return NotFound();
            }
            return View(membershipType);
        }

        // edit membership
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SignupFee,DurationInMonths,DiscountRate")] MembershipType membershipType)
        {
            if (id != membershipType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membershipType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembershipTypeExists(membershipType.Id))
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
            return View(membershipType);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MembershipTypes == null)
            {
                return NotFound();
            }

            var membershipType = await _context.MembershipTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membershipType == null)
            {
                return NotFound();
            }

            return View(membershipType);
        }

        // delete membership
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MembershipTypes == null)
            {
                return Problem("Entity set 'AppDbContext.MembershipTypes'  is null.");
            }
            var membershipType = await _context.MembershipTypes.FindAsync(id);
            if (membershipType != null)
            {
                _context.MembershipTypes.Remove(membershipType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembershipTypeExists(int id)
        {
          return (_context.MembershipTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
