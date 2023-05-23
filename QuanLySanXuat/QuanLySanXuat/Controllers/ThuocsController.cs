using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLySanXuat.Models;

namespace QuanLySanXuat.Controllers
{
    public class ThuocsController : Controller
    {
        private readonly QuanLySanXuatContext _context;

        public ThuocsController(QuanLySanXuatContext context)
        {
            _context = context;
        }

        // GET: THUOCs
        public async Task<IActionResult> Index(string searchString)
        {
            var thuoc = from m in _context.Thuocs
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                thuoc = thuoc.Where(s => s.TenThuoc.Contains(searchString));
            }

            return View(await thuoc.ToListAsync());
        }

        // GET: Thuocs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Thuocs == null)
            {
                return NotFound();
            }

            var thuoc = await _context.Thuocs
                .FirstOrDefaultAsync(m => m.MaThuoc == id);
            if (thuoc == null)
            {
                return NotFound();
            }

            return View(thuoc);
        }

        // GET: Thuocs/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaThuoc,TenThuoc,QuyCach")] Thuoc thuoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thuoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(thuoc);
        }

        // GET: Thuocs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Thuocs == null)
            {
                return NotFound();
            }

            var thuoc = await _context.Thuocs.FindAsync(id);
            if (thuoc == null)
            {
                return NotFound();
            }
            return View(thuoc);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaThuoc,TenThuoc,QuyCach")] Thuoc thuoc)
        {
            if (id != thuoc.MaThuoc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thuoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThuocExists(thuoc.MaThuoc))
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
            return View(thuoc);
        }

        // GET: Thuocs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Thuocs == null)
            {
                return NotFound();
            }

            var thuoc = await _context.Thuocs
                .FirstOrDefaultAsync(m => m.MaThuoc == id);
            if (thuoc == null)
            {
                return NotFound();
            }

            return View(thuoc);
        }

        // POST: Thuocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Thuocs == null)
            {
                return Problem("Entity set 'QuanLySanXuatContext.Thuocs'  is null.");
            }
            var thuoc = await _context.Thuocs.FindAsync(id);
            if (thuoc != null)
            {
                _context.Thuocs.Remove(thuoc);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThuocExists(int id)
        {
          return (_context.Thuocs?.Any(e => e.MaThuoc == id)).GetValueOrDefault();
        }
    }
}
