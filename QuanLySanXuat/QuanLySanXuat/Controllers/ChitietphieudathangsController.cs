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
    public class ChitietphieudathangsController : Controller
    {
        private readonly QuanLySanXuatContext _context;

        public ChitietphieudathangsController(QuanLySanXuatContext context)
        {
            _context = context;
        }

        // GET: Chitietphieudathangs
        public async Task<IActionResult> Index(string searchString)
        {
            var chiTietPhieuDatHangs = from ct in _context.Chitietphieudathangs
                                       select ct;
            if (!String.IsNullOrEmpty(searchString))
            {
                chiTietPhieuDatHangs = chiTietPhieuDatHangs.Where(ct => ct.SoPhieu.ToString().Contains(searchString)
                                                                      || ct.MaThuoc.ToString().Contains(searchString));
            }
            return View(await chiTietPhieuDatHangs.ToListAsync());
        }

        // GET: Chitietphieudathangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Chitietphieudathangs == null)
            {
                return NotFound();
            }

            var chitietphieudathang = await _context.Chitietphieudathangs
                .Include(c => c.MaThuocNavigation)
                .Include(c => c.SoPhieuNavigation)
                .FirstOrDefaultAsync(m => m.SoPhieu == id);
            if (chitietphieudathang == null)
            {
                return NotFound();
            }

            return View(chitietphieudathang);
        }

        // GET: Chitietphieudathangs/Create
        public IActionResult Create()
        {
            ViewData["MaThuoc"] = new SelectList(_context.Thuocs, "MaThuoc", "MaThuoc");
            ViewData["SoPhieu"] = new SelectList(_context.Phieudathangs, "SoPhieu", "SoPhieu");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SoPhieu,MaThuoc,SoLuongDat")] Chitietphieudathang chitietphieudathang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chitietphieudathang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaThuoc"] = new SelectList(_context.Thuocs, "MaThuoc", "MaThuoc", chitietphieudathang.MaThuoc);
            ViewData["SoPhieu"] = new SelectList(_context.Phieudathangs, "SoPhieu", "SoPhieu", chitietphieudathang.SoPhieu);
            return View(chitietphieudathang);
        }

        // GET: Chitietphieudathangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Chitietphieudathangs == null)
            {
                return NotFound();
            }

            var chitietphieudathang = await _context.Chitietphieudathangs.FindAsync(id);
            if (chitietphieudathang == null)
            {
                return NotFound();
            }
            ViewData["MaThuoc"] = new SelectList(_context.Thuocs, "MaThuoc", "MaThuoc", chitietphieudathang.MaThuoc);
            ViewData["SoPhieu"] = new SelectList(_context.Phieudathangs, "SoPhieu", "SoPhieu", chitietphieudathang.SoPhieu);
            return View(chitietphieudathang);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SoPhieu,MaThuoc,SoLuongDat")] Chitietphieudathang chitietphieudathang)
        {
            if (id != chitietphieudathang.SoPhieu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chitietphieudathang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChitietphieudathangExists(chitietphieudathang.SoPhieu))
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
            ViewData["MaThuoc"] = new SelectList(_context.Thuocs, "MaThuoc", "MaThuoc", chitietphieudathang.MaThuoc);
            ViewData["SoPhieu"] = new SelectList(_context.Phieudathangs, "SoPhieu", "SoPhieu", chitietphieudathang.SoPhieu);
            return View(chitietphieudathang);
        }

        // GET: Chitietphieudathangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Chitietphieudathangs == null)
            {
                return NotFound();
            }

            var chitietphieudathang = await _context.Chitietphieudathangs
                .Include(c => c.MaThuocNavigation)
                .Include(c => c.SoPhieuNavigation)
                .FirstOrDefaultAsync(m => m.SoPhieu == id);
            if (chitietphieudathang == null)
            {
                return NotFound();
            }

            return View(chitietphieudathang);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Chitietphieudathangs == null)
            {
                return Problem("Entity set 'QuanLySanXuatContext.Chitietphieudathangs'  is null.");
            }
            var chitietphieudathang = await _context.Chitietphieudathangs.FindAsync(id);
            if (chitietphieudathang != null)
            {
                _context.Chitietphieudathangs.Remove(chitietphieudathang);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChitietphieudathangExists(int id)
        {
          return (_context.Chitietphieudathangs?.Any(e => e.SoPhieu == id)).GetValueOrDefault();
        }
    }
}
