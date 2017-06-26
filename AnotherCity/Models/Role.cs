using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AnotherCity.Models
{
    public class Role : IdentityRole<int>
    {
        public Role() : base()
        {
        }
        public Role(string name): base(name)
        {
        }
    }
}
