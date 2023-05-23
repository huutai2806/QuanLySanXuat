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
    public class PhieudathangsController : Controller
    {
        private readonly QuanLySanXuatContext _context;

        public PhieudathangsController(QuanLySanXuatContext context)
        {
            _context = context;
        }

        // GET: Phieudathangs
        public async Task<IActionResult> Index(string searchString)
        {
            var phieuDatHangs = from p in _context.Phieudathangs
                                select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                phieuDatHangs = phieuDatHangs.Where(p => p.SoPhieu.ToString().Contains(searchString)
                    || p.NgayLapPhieu.ToString().Contains(searchString)
                    || p.MaKhachHang.ToString().Contains(searchString));
            }

            return View(await phieuDatHangs.ToListAsync());
        }
        // GET: Phieudathangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Phieudathangs == null)
            {
                return NotFound();
            }

            var phieudathang = await _context.Phieudathangs
                .Include(p => p.MaKhachHangNavigation)
                .FirstOrDefaultAsync(m => m.SoPhieu == id);
            if (phieudathang == null)
            {
                return NotFound();
            }

            return View(phieudathang);
        }

        // GET: Phieudathangs/Create
        public IActionResult Create()
        {
            ViewData["MaKhachHang"] = new SelectList(_context.Khachhangs, "MaKhachHang", "MaKhachHang");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SoPhieu,NgayLapPhieu,MaKhachHang")] Phieudathang phieudathang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phieudathang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaKhachHang"] = new SelectList(_context.Khachhangs, "MaKhachHang", "MaKhachHang", phieudathang.MaKhachHang);
            return View(phieudathang);
        }

        // GET: Phieudathangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Phieudathangs == null)
            {
                return NotFound();
            }

            var phieudathang = await _context.Phieudathangs.FindAsync(id);
            if (phieudathang == null)
            {
                return NotFound();
            }
            ViewData["MaKhachHang"] = new SelectList(_context.Khachhangs, "MaKhachHang", "MaKhachHang", phieudathang.MaKhachHang);
            return View(phieudathang);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SoPhieu,NgayLapPhieu,MaKhachHang")] Phieudathang phieudathang)
        {
            if (id != phieudathang.SoPhieu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phieudathang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhieudathangExists(phieudathang.SoPhieu))
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
            ViewData["MaKhachHang"] = new SelectList(_context.Khachhangs, "MaKhachHang", "MaKhachHang", phieudathang.MaKhachHang);
            return View(phieudathang);
        }

        // GET: Phieudathangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Phieudathangs == null)
            {
                return NotFound();
            }

            var phieudathang = await _context.Phieudathangs
                .Include(p => p.MaKhachHangNavigation)
                .FirstOrDefaultAsync(m => m.SoPhieu == id);
            if (phieudathang == null)
            {
                return NotFound();
            }

            return View(phieudathang);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Phieudathangs == null)
            {
                return Problem("Entity set 'QuanLySanXuatContext.Phieudathangs'  is null.");
            }
            var phieudathang = await _context.Phieudathangs.FindAsync(id);
            if (phieudathang != null)
            {
                _context.Phieudathangs.Remove(phieudathang);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhieudathangExists(int id)
        {
          return (_context.Phieudathangs?.Any(e => e.SoPhieu == id)).GetValueOrDefault();
        }
    }
}
