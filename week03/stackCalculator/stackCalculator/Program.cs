using Calc;
using System;

namespace Solution
{
    static class Program
    {
        static void Main()
        {
            Console.WriteLine("-----Stack calculator-----");
            Console.WriteLine("\nEnter an expression in postfix form: ");
            try
            {
                string? inputString = Console.ReadLine();
                if (inputString is null)
                {
                    throw new ArgumentNullException();
                }
                Console.WriteLine("\nResult: {0}", Calculator.Compute(inputString));
            }
            catch (Exception)
            {
                Console.WriteLine("\nAn error occured");
            }
        }
    }
}
