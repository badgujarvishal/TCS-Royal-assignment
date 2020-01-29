using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using RoyalBank_Assignment.entities;
using System.Configuration;
using System.Reflection;

namespace RoyalBank_Assignment.Business_Layer
{
    /// <summary>
    /// Implmentation of CSV reader to read the data and mapped it to object
    /// </summary>
    class CSVReader : ICustomerDataDAL
    {

       public  List<Customer> ReadCustomerData()
        {
            List<Customer> customers = new List<Customer>();

            //string csvpath = ConfigurationManager.AppSettings["DataPath"];
          //  string path = ConfigurationManager.AppSettings["DataPath"];
            string workingDirectory = Environment.CurrentDirectory;
            // or: Directory.GetCurrentDirectory() gives the same result

            // This will get the current PROJECT directory
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            string path = projectDirectory + ConfigurationManager.AppSettings["DataPath"];
            DataTable dt = readCSVFile(path);
            customers = dt.AsEnumerable()
                               .Select(g => new Customer
                               {
                                   ID = Convert.ToInt32(g.Field<string>("id")),
                                   Title = g.Field<string>("Title"),
                                   FirstName = g.Field<string>("firstname"),
                                   Surname = g.Field<string>("surname"),
                                   ProductName = g.Field<string>("productname"),
                                   PayoutAmount = Convert.ToDouble(g.Field<string>("payoutamount")),
                                   AnnualPremium = Convert.ToDouble(g.Field<string>("annualpremium"))

                               }).ToList();
            return customers;
        }

        public DataTable readCSVFile(string path)
        {
            string CSVFilePathName = path;
            string[] Lines = File.ReadAllLines(CSVFilePathName);
            string[] Fields;
            Fields = Lines[0].Split(new char[] { ',' });
            int Cols = Fields.GetLength(0);
            DataTable dt = new DataTable();
            //1st row must be column names; force lower case to ensure matching later on.
            for (int i = 0; i < Cols; i++)
                dt.Columns.Add(Fields[i].ToLower(), typeof(string));
            DataRow Row;
            for (int i = 1; i < Lines.GetLength(0); i++)
            {
                Fields = Lines[i].Split(new char[] { ',' });
                Row = dt.NewRow();
                for (int f = 0; f < Cols; f++)
                    Row[f] = Fields[f];
                dt.Rows.Add(Row);
            }
            return dt;
        }
    }
}
