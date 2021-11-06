using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Models;
using Test.Data;

namespace Test.Controllers
{
    public class UsersController : Controller
    {
        private readonly UsersContext _context;

        public UsersController(UsersContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Users(/*string sortOrder,*/)
        {
            //ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            //ViewData["MarriedSortParm"] = sortOrder == "Married" ? "married_desc" : "Married";
            //ViewData["PhoneSortParm"] = sortOrder == "Phone" ? "phone_desc" : "Phone";
            //ViewData["SalarySortParm"] = sortOrder == "Salary" ? "salary_desc" : "Salary";
            
            var users = from u in _context.Users select u;
            
            //switch (sortOrder)
            //{
            //    case "name_desc":
            //        users = users.OrderByDescending(u => u.Name);
            //        break;
            //    case "Date":
            //        users = users.OrderBy(u => u.DateOfBirth);
            //        break;
            //    case "date_desc":
            //        users = users.OrderByDescending(s => s.DateOfBirth);
            //        break;
            //    case "Married":
            //        users = users.OrderBy(u => u.Married);
            //        break;
            //    case "married_desc":
            //        users = users.OrderByDescending(s => s.Married);
            //        break;
            //    case "Phone":
            //        users = users.OrderBy(u => u.Phone);
            //        break;
            //    case "phone_desc":
            //        users = users.OrderByDescending(s => s.Phone);
            //        break;
            //    case "Salary":
            //        users = users.OrderBy(u => u.Salary);
            //        break;
            //    case "salary_desc":
            //        users = users.OrderByDescending(s => s.Salary);
            //        break;
            //    default:
            //        users = users.OrderBy(s => s.Name);
            //        break;
            //}
            return View(await users.AsNoTracking().ToListAsync());

        }
        public IActionResult Create()
        {           
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,DateOfBirth,Married,Phone,Salary")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                
            }
            return RedirectToAction("Users");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            User userToEdit = (User)_context.Users.ToList().Find(u => u.ID == id);
            return View(userToEdit);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int? id, string name, DateTime dateOfBirth, bool married, string phone, decimal salary)
        {
            if (id == null)
            {
                return NotFound();
            }
            User userToEdit = (User)_context.Users.ToList().Find(u => u.ID == id);

            userToEdit.Name = name;
            userToEdit.DateOfBirth = dateOfBirth;
            userToEdit.Married = married;
            userToEdit.Phone = phone;
            userToEdit.Salary = salary;

            _context.SaveChanges();

            return RedirectToAction("Users");
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            User userToDelete = (User)_context.Users.ToList().Find(u => u.ID == id);
            _context.Users.Remove(userToDelete);
            _context.SaveChanges();
            return RedirectToAction("Users");
        }
    }
}
