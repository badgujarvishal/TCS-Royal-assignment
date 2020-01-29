using RoyalBank_Assignment.entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;

namespace RoyalBank_Assignment.Business_Layer
{
    /// <summary>
    /// letter generator class which will get data from data provider and after calculating the amounts saving into the text file as sample letter.
    /// </summary>
    public class LetterGenerator
    {
        public void GenerateRenewalFiles()
        {
            try
            {
                CustomerBL customer = new CustomerBL(new CSVReader()); // constructor injection which will help us to change the dataprovider in future
                List<Customer>  customerlist = customer.ReadCustomerData();
                foreach(Customer cust in customerlist)
                {
                    GenerateRenewalfile(cust);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured while generating report.");
                Console.WriteLine("Error message : " + ex.Message);
                throw ex;
            }
        }

        private void GenerateRenewalfile(Customer customer)
        {
            try
            {
                
                string fileName = customer.ID + "_" + customer.FirstName + ".txt";

                string workingDirectory = Environment.CurrentDirectory;
                // This will get the current PROJECT directory

                string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
                string OutputFolder = projectDirectory +  ConfigurationManager.AppSettings["OutputFolder"];
                if (!File.Exists(OutputFolder + fileName))
                {
                   if(!System.IO.Directory.Exists(OutputFolder))
                    {
                        System.IO.Directory.CreateDirectory(OutputFolder);
                    }
                    CalculateMonthlypayment(customer);  // funcation call to calculate the monthly payment amount
                    string lettertext = GenerateLetter(customer); // method to generate the letter for the customer details
                    File.Create(OutputFolder + fileName).Close(); 
                    using (StreamWriter sw = File.AppendText(OutputFolder + fileName))
                    {
                        sw.Write(lettertext); // Write text to .txt file
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured while generating report for customer : " + customer.FirstName);
                Console.WriteLine("Error message : " + ex.Message);
                throw ex;
            }

        }

        //function to generate the letter from sample letter
        private string GenerateLetter(Customer customer)
        {

            // setting culture info to get the dd'/'MM'/'yyyy format
            CultureInfo ci = new CultureInfo(CultureInfo.CurrentCulture.Name);

            ci.DateTimeFormat.ShortDatePattern = "dd'/'MM'/'yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
            var dt = DateTime.Now.Date;

            string currentdate = dt.ToString("d");
            string workingDirectory = Environment.CurrentDirectory;
            // This will get the current PROJECT directory

            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            string letterpath = projectDirectory + ConfigurationManager.AppSettings["SampleLetter"];
            try
            {
                StringBuilder contents = new StringBuilder(
                    File.ReadAllText(letterpath, Encoding.UTF7));  // UTF7 to read the symbol £ 



                contents.Replace("<<date>>", currentdate);
                contents.Replace("<<customerFullName>>", customer.Title + " " + customer.FirstName + " " + customer.Surname);
                contents.Replace("<<customerlastname>>", customer.Title + " " + customer.Surname);
                contents.Replace("<<productname>>", customer.ProductName);
                contents.Replace("<<PayoutAmount>>", customer.PayoutAmount.ToString());
                contents.Replace("<<AnnualPremium>>", customer.AnnualPremium.ToString());
                contents.Replace("<<CreditCharge>>", customer.CreditCharge.ToString());
                contents.Replace("<<AnnualPremiumplusCreditCharge>>", customer.AnnualPremium.ToString());
                contents.Replace("<<InitialMonthlyPaymentAmount>>", customer.InitialMonthlyPremium.ToString());
                contents.Replace("<<OtherMonthlyPaymentsAmount>>", customer.OtherMonthlyPremium.ToString());
               
                return contents.ToString();

            }
            catch (Exception)
            {

                throw;
            }
        }

        //function to get the initial monthly premium and other monthly premium
        private void CalculateMonthlypayment(Customer customer)
        {
            if(customer.TotalPremium == customer.AvgMonthlyPremium * 12)
            {
                customer.InitialMonthlyPremium = customer.AvgMonthlyPremium;
                customer.OtherMonthlyPremium = customer.AvgMonthlyPremium;
            }
            else
            {
                customer.InitialMonthlyPremium = Math.Round((customer.TotalPremium - (customer.AvgMonthlyPremium * 11)),2);
                customer.OtherMonthlyPremium = Math.Round(customer.AvgMonthlyPremium,2);
            }
        }
    }
}
