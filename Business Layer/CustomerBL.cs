using RoyalBank_Assignment.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalBank_Assignment.Business_Layer
{
    class CustomerBL
    {
        public ICustomerDataDAL customerDAL;
        public CustomerBL(ICustomerDataDAL customerDAL)
        {
            this.customerDAL = customerDAL;
        }
        public List<Customer> ReadCustomerData()
        {
            return customerDAL.ReadCustomerData();
        }
    }
}
