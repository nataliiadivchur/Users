using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Models;

namespace Test.Data
{
    public class DbInitializer
    {
        public static void Initialize(UsersContext context)
        {
            context.Database.EnsureCreated();

            // Look for any users
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
            new User { Name = "Pavlo", DateOfBirth = DateTime.Parse("2000-09-01"), Married = false, Phone ="220 222 11 22", Salary = 1500},
            new User { Name = "Mariia", DateOfBirth = DateTime.Parse("1995-07-02"), Married = true, Phone ="220 222 11 88", Salary = 1200},
            new User { Name = "Alla", DateOfBirth = DateTime.Parse("1999-11-08"), Married = false, Phone ="220 222 11 77", Salary = 1350},
            };
            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();

        }
    }
}

