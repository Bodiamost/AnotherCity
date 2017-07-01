using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AnotherCity.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class Account : IdentityUser<int>
    {
        public string PasswordSalt { get; set; }

        public virtual User User { get; set; }
    }
}
