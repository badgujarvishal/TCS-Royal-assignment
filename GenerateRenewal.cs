using RoyalBank_Assignment.Business_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalBank_Assignment
{
    class GenerateRenewal
    {
        static void Main(string[] args)
        {
            LetterGenerator letter = new LetterGenerator();
            Console.WriteLine("Generating the letters....");
            try
            {
                letter.GenerateRenewalFiles();
                Console.WriteLine("Generating letter completed.");
            }
            catch(Exception)
            {
                Console.WriteLine("Generating letter failed.");
            }
            finally
            {
                Console.ReadLine();
            }

        }
    }
}
