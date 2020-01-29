using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalBank_Assignment.entities
{
    public class Customer
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string ProductName { get; set; }
        public double PayoutAmount { get; set; }

        public double AnnualPremium { get; set; }

        public double CreditCharge { get { return Math.Round(( (5 * AnnualPremium) / 100),2); } }

        public double TotalPremium { get { return AnnualPremium + CreditCharge; } }

        public double AvgMonthlyPremium { get { return Math.Round((TotalPremium / 12), 2); } }
        public double InitialMonthlyPremium { get; set; }
        public double OtherMonthlyPremium { get; set; }
    }
}
