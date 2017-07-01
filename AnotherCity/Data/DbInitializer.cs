using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnotherCity.Models;

namespace AnotherCity.Data
{
    public class DbInitializer
    {
        public static void Initialize(AnotherCityDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Positions.Any())
            {
                return;   // DB has been seeded
            }
            else
            {
                Positions position = new Positions { Title = "Developer" };
                context.Positions.Add(position);
                context.SaveChanges();
            }


        }


    }
}
