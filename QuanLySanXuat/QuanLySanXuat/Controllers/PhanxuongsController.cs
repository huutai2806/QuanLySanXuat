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
    public class PhanxuongsController : Controller
    {
        private readonly QuanLySanXuatContext _context;

        public PhanxuongsController(QuanLySanXuatContext context)
        {
            _context = context;
        }

        // GET: Phanxuongs
        public async Task<IActionResult> Index()
        {
              return _context.Phanxuongs != null ? 
                          View(await _context.Phanxuongs.ToListAsync()) :
                          Problem("Entity set 'QuanLySanXuatContext.Phanxuongs'  is null.");
        }
        public async Task<IActionResult> Search(string searchString)
        {
            var phanXuong = from px in _context.Phanxuongs
                            select px;

            if (!string.IsNullOrEmpty(searchString))
            {
                phanXuong = phanXuong.Where(px => px.MaPhanXuong.ToString().Contains(searchString)
                                                || px.TenPhanXuong.Contains(searchString)
                                                || px.DiaChi.Contains(searchString)
                                                || px.DienThoai.Contains(searchString)
                                                || px.HoTenQuanDoc.Contains(searchString));
            }
            return View(await phanXuong.ToListAsync());
        }

        // GET: Phanxuongs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Phanxuongs == null)
            {
                return NotFound();
            }

            var phanxuong = await _context.Phanxuongs
                .FirstOrDefaultAsync(m => m.MaPhanXuong == id);
            if (phanxuong == null)
            {
                return NotFound();
            }

            return View(phanxuong);
        }

        // GET: Phanxuongs/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaPhanXuong,TenPhanXuong,DiaChi,DienThoai,HoTenQuanDoc")] Phanxuong phanxuong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phanxuong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(phanxuong);
        }

        // GET: Phanxuongs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Phanxuongs == null)
            {
                return NotFound();
            }

            var phanxuong = await _context.Phanxuongs.FindAsync(id);
            if (phanxuong == null)
            {
                return NotFound();
            }
            return View(phanxuong);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaPhanXuong,TenPhanXuong,DiaChi,DienThoai,HoTenQuanDoc")] Phanxuong phanxuong)
        {
            if (id != phanxuong.MaPhanXuong)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phanxuong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhanxuongExists(phanxuong.MaPhanXuong))
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
            return View(phanxuong);
        }

        // GET: Phanxuongs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Phanxuongs == null)
            {
                return NotFound();
            }

            var phanxuong = await _context.Phanxuongs
                .FirstOrDefaultAsync(m => m.MaPhanXuong == id);
            if (phanxuong == null)
            {
                return NotFound();
            }

            return View(phanxuong);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Phanxuongs == null)
            {
                return Problem("Entity set 'QuanLySanXuatContext.Phanxuongs'  is null.");
            }
            var phanxuong = await _context.Phanxuongs.FindAsync(id);
            if (phanxuong != null)
            {
                _context.Phanxuongs.Remove(phanxuong);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhanxuongExists(int id)
        {
          return (_context.Phanxuongs?.Any(e => e.MaPhanXuong == id)).GetValueOrDefault();
        }
    }
}
