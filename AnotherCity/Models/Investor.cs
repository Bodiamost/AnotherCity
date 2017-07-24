using System;
using System.Collections.Generic;

namespace AnotherCity.Models
{
    public partial class Investor : User
    {
        public int? InvestOpportunityId { get; set; }

        public virtual InvestOpportunity InvestOpportunity { get; set; }
    }
}
