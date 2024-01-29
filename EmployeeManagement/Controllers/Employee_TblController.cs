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
    public class Employee_TblController : Controller
    {
        private readonly EmployeeManagementContext _context;

        public Employee_TblController(EmployeeManagementContext context)
        {
            _context = context;
        }

        // GET: Employee_Tbl
        public async Task<IActionResult> Index()
        {
            var employees = from s in _context.Employee_Tbl
                           .Include(c => c.Department)
                           select s;
            return View(await employees.ToListAsync());
        }

        // GET: Employee_Tbl/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee_Tbl = await _context.Employee_Tbl
                .Include(x => x.Department)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee_Tbl == null)
            {
                return NotFound();
            }

            return View(employee_Tbl);
        }

        // GET: Employee_Tbl/Create
        public IActionResult Create()
        {
            PopulateDepartmentsDropDownList();
            return View();
        }

        // POST: Employee_Tbl/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeCode,Name,Rank,DepartmentID")] Employee_Tbl employee_Tbl)
        {
            var department = await _context.Department_Tbl
                .Include(e => e.Employees)
                .FirstOrDefaultAsync(d => d.Id == employee_Tbl.DepartmentID);
            if (department != null)
            {
                if (department.Employees.Count < department.NumberOfPersonals)
                {


                    try
                    {
                        _context.Add(employee_Tbl);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateException)
                    {
                        // Xử lý lỗi (ví dụ: log lỗi)
                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists " +
                            "see your system administrator.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Department is full. Cannot add more employees.");
                }
            }
            PopulateDepartmentsDropDownList(employee_Tbl.DepartmentID);
            return View(employee_Tbl);
        }

        // GET: Employee_Tbl/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee_Tbl = await _context.Employee_Tbl.FindAsync(id);
            if (employee_Tbl == null)
            {
                return NotFound();
            }

            PopulateDepartmentsDropDownList(employee_Tbl.DepartmentID);
            return View(employee_Tbl);
        }

        // POST: Employee_Tbl/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var employeeToUpdate = await _context.Employee_Tbl
                .Include(p => p.Department)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (await TryUpdateModelAsync<Employee_Tbl>(employeeToUpdate,
                "",
                p => p.Name, p => p.EmployeeCode, p => p.Rank, p => p.DepartmentID))
            {
                try
                {
                    _context.Update(employeeToUpdate);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            PopulateDepartmentsDropDownList(employeeToUpdate.DepartmentID);
            return View(employeeToUpdate);
        }

        private void PopulateDepartmentsDropDownList(object selectedDepartment= null)
        {
            var departmentsQuery = from d in _context.Department_Tbl
                                  select d;
            ViewBag.DepartmentID = new SelectList(departmentsQuery.AsNoTracking(), "Id", "Name", selectedDepartment);
        }
        

        // GET: Employee_Tbl/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee_Tbl = await _context.Employee_Tbl
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee_Tbl == null)
            {
                return NotFound();
            }

            return View(employee_Tbl);
        }

        // POST: Employee_Tbl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee_Tbl = await _context.Employee_Tbl.FindAsync(id);
            if (employee_Tbl != null)
            {
                _context.Employee_Tbl.Remove(employee_Tbl);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Employee_TblExists(int id)
        {
            return _context.Employee_Tbl.Any(e => e.Id == id);
        }
    }
}
