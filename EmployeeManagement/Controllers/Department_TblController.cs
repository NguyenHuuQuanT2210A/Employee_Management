using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Data;
using EmployeeManagement.Models;

namespace EmployeeManagement.Controllers
{
    public class Department_TblController : Controller
    {
        private readonly EmployeeManagementContext _context;

        public Department_TblController(EmployeeManagementContext context)
        {
            _context = context;
        }

        // GET: Department_Tbl
        public async Task<IActionResult> Index()
        {
            return View(await _context.Department_Tbl.ToListAsync());
        }

        // GET: Department_Tbl/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department_Tbl = await _context.Department_Tbl
                .Include(x => x.Employees)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department_Tbl == null)
            {
                return NotFound();
            }

            return View(department_Tbl);
        }

        // GET: Department_Tbl/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Department_Tbl/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DepartmentCode,Name,location,NumberOfPersonals")] Department_Tbl department_Tbl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department_Tbl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department_Tbl);
        }

        // GET: Department_Tbl/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department_Tbl = await _context.Department_Tbl
                .Include(x => x.Employees)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department_Tbl == null)
            {
                return NotFound();
            }
            return View(department_Tbl);
        }

        // POST: Department_Tbl/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DepartmentCode,Name,location,NumberOfPersonals")] Department_Tbl department_Tbl)
        {
            if (id != department_Tbl.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department_Tbl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Department_TblExists(department_Tbl.Id))
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
            return View(department_Tbl);
        }

        // GET: Department_Tbl/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department_Tbl = await _context.Department_Tbl
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department_Tbl == null)
            {
                return NotFound();
            }

            return View(department_Tbl);
        }

        // POST: Department_Tbl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department_Tbl = await _context.Department_Tbl.FindAsync(id);
            if (department_Tbl != null)
            {
                _context.Department_Tbl.Remove(department_Tbl);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Department_TblExists(int id)
        {
            return _context.Department_Tbl.Any(e => e.Id == id);
        }
    }
}
