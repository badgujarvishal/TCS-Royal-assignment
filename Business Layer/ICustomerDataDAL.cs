using RoyalBank_Assignment.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalBank_Assignment.Business_Layer
{

    /// <summary>
    /// Interface to which will help to implement the different data providers 
    /// </summary>
    public interface ICustomerDataDAL
    {
        List<Customer> ReadCustomerData();
    }
}
