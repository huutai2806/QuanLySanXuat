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
    public class PhancongsController : Controller
    {
        private readonly QuanLySanXuatContext _context;

        public PhancongsController(QuanLySanXuatContext context)
        {
            _context = context;
        }

        // GET: Phancongs
        public async Task<IActionResult> Index(string searchString)
        {
            var phanCong = from pc in _context.Phancongs
                           select pc;

            if (!String.IsNullOrEmpty(searchString))
            {
                phanCong = phanCong.Where(pc => pc.SoPhieu.ToString().Contains(searchString)
                    || pc.MaPhanXuong.ToString().Contains(searchString)
                    || pc.MaThuoc.ToString().Contains(searchString));
            }
            return View(await phanCong.ToListAsync());
        }


        // GET: Phancongs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Phancongs == null)
            {
                return NotFound();
            }

            var phancong = await _context.Phancongs
                .Include(p => p.MaPhanXuongNavigation)
                .Include(p => p.MaThuocNavigation)
                .Include(p => p.SoPhieuNavigation)
                .FirstOrDefaultAsync(m => m.SoPhanCong == id);
            if (phancong == null)
            {
                return NotFound();
            }

            return View(phancong);
        }

        // GET: Phancongs/Create
        public IActionResult Create()
        {
            ViewData["MaPhanXuong"] = new SelectList(_context.Phanxuongs, "MaPhanXuong", "MaPhanXuong");
            ViewData["MaThuoc"] = new SelectList(_context.Thuocs, "MaThuoc", "MaThuoc");
            ViewData["SoPhieu"] = new SelectList(_context.Phieudathangs, "SoPhieu", "SoPhieu");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SoPhanCong,MaPhanXuong,SoPhieu,MaThuoc,SoLuong")] Phancong phancong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phancong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaPhanXuong"] = new SelectList(_context.Phanxuongs, "MaPhanXuong", "MaPhanXuong", phancong.MaPhanXuong);
            ViewData["MaThuoc"] = new SelectList(_context.Thuocs, "MaThuoc", "MaThuoc", phancong.MaThuoc);
            ViewData["SoPhieu"] = new SelectList(_context.Phieudathangs, "SoPhieu", "SoPhieu", phancong.SoPhieu);
            return View(phancong);
        }

        // GET: Phancongs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Phancongs == null)
            {
                return NotFound();
            }

            var phancong = await _context.Phancongs.FindAsync(id);
            if (phancong == null)
            {
                return NotFound();
            }
            ViewData["MaPhanXuong"] = new SelectList(_context.Phanxuongs, "MaPhanXuong", "MaPhanXuong", phancong.MaPhanXuong);
            ViewData["MaThuoc"] = new SelectList(_context.Thuocs, "MaThuoc", "MaThuoc", phancong.MaThuoc);
            ViewData["SoPhieu"] = new SelectList(_context.Phieudathangs, "SoPhieu", "SoPhieu", phancong.SoPhieu);
            return View(phancong);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SoPhanCong,MaPhanXuong,SoPhieu,MaThuoc,SoLuong")] Phancong phancong)
        {
            if (id != phancong.SoPhanCong)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phancong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhancongExists(phancong.SoPhanCong))
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
            ViewData["MaPhanXuong"] = new SelectList(_context.Phanxuongs, "MaPhanXuong", "MaPhanXuong", phancong.MaPhanXuong);
            ViewData["MaThuoc"] = new SelectList(_context.Thuocs, "MaThuoc", "MaThuoc", phancong.MaThuoc);
            ViewData["SoPhieu"] = new SelectList(_context.Phieudathangs, "SoPhieu", "SoPhieu", phancong.SoPhieu);
            return View(phancong);
        }

        // GET: Phancongs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Phancongs == null)
            {
                return NotFound();
            }

            var phancong = await _context.Phancongs
                .Include(p => p.MaPhanXuongNavigation)
                .Include(p => p.MaThuocNavigation)
                .Include(p => p.SoPhieuNavigation)
                .FirstOrDefaultAsync(m => m.SoPhanCong == id);
            if (phancong == null)
            {
                return NotFound();
            }

            return View(phancong);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Phancongs == null)
            {
                return Problem("Entity set 'QuanLySanXuatContext.Phancongs'  is null.");
            }
            var phancong = await _context.Phancongs.FindAsync(id);
            if (phancong != null)
            {
                _context.Phancongs.Remove(phancong);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhancongExists(int id)
        {
          return (_context.Phancongs?.Any(e => e.SoPhanCong == id)).GetValueOrDefault();
        }
    }
}
