﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TP4.Models;

namespace TP4.Controllers
{
    public class CustomersController : Controller
    {
        private readonly AppDbContext _context;

        public CustomersController(AppDbContext context)
        {
            _context = context;
        }

        //  get customers
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Customers.Include(c => c.Membership);
            return View(await appDbContext.ToListAsync());
        }

        // get customer details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Membership)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        public IActionResult Create()
        {
            ViewData["MembershipTypeId"] = new SelectList(_context.MembershipTypes, "Id", "Id");
            return View();
        }

        // create customer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,MembershipTypeId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MembershipTypeId"] = new SelectList(_context.MembershipTypes, "Id", "Id", customer.MembershipTypeId);
            return View(customer);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["MembershipTypeId"] = new SelectList(_context.MembershipTypes, "Id", "Id", customer.MembershipTypeId);
            return View(customer);
        }

        // edit customer

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,MembershipTypeId")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            ViewData["MembershipTypeId"] = new SelectList(_context.MembershipTypes, "Id", "Id", customer.MembershipTypeId);
            return View(customer);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Membership)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        //delete customer
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Customers == null)
            {
                return Problem("Entity set 'AppDbContext.Customers'  is null.");
            }
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
          return (_context.Customers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
