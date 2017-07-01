using System;
using System.Collections.Generic;

namespace AnotherCity.Models
{
    public partial class Image
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public double? Size { get; set; }

        public virtual Project Project { get; set; }
    }
}
