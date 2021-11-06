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
        public async Task<IActionResult> Users()
        {
            var users = from u in _context.Users select u;
            
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
