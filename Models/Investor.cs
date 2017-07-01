using System;
using System.Collections.Generic;

namespace AnotherCity.Models
{
    public partial class Investor : User
    {
        public int? OpportunityId { get; set; }

        public virtual InvestOpportunity Opportunity { get; set; }
    }
}
