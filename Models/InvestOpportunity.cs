using System;
using System.Collections.Generic;

namespace AnotherCity.Models
{
    public partial class InvestOpportunity
    {
        public InvestOpportunity()
        {
            Investors = new HashSet<Investor>();
        }

        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public int? Amount { get; set; }
        public string Description { get; set; }
        public string BudgetDocument { get; set; }
        public string LinkToInvest { get; set; }

        public virtual ICollection<Investor> Investors { get; set; }
        public virtual Project Project { get; set; }
    }
}
