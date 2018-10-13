using System;
using System.Linq;

namespace Lab5
{
    class Program//starts getting funky at 21
    {
        static void Main(string[] args)
        {
            long recurse = 0;
            bool request = true;
            bool retry = true;

            while (retry)
            {
                Console.Clear();
                MathCalc(ref recurse, ref retry, ref request);
                retry = Retry(ref retry, ref request);
            }
            Exit();
        }

        public static void MathCalc(ref long recurse, ref bool retry, ref bool request)
        {
            long input = 0;

            request = true;
            Console.WriteLine("Welcome, to the Factorial Calculator!\nPlease enter an integer that's greater than 0 but less than 10: ");
            string userInput = Console.ReadLine();
            if (long.TryParse(userInput, out input) && input > 0)
            {
                recurse = input;
            }
            else if (userInput.All(Char.IsDigit))
            {
                Console.WriteLine("\nSorry, the number you entered is too large to be displayed");
                Invalid(ref retry, ref request);
            }
            else
                Invalid(ref retry, ref request);
            if (input > 10)
            {
                Console.WriteLine("Tisk, Tisk, Tisk, you entered a number greater than 10! Lucky for you, I removed those constraints :)");
            }
            //using recursive
            long factorialRecurse = MathRecurse(recurse);
            //using loop
            long factorialLoopIncrease = 1;
            for (long i = 1; i < input; i++)
            {
                factorialLoopIncrease = factorialLoopIncrease * (i + 1);
            }
            if (factorialLoopIncrease > long.MaxValue || factorialRecurse > long.MaxValue)
            {
                Console.WriteLine("Sorry, the factorial of the number you entered is too large to be displayed");
                Invalid(ref retry, ref request);
            }
            Console.WriteLine("Using the recursive method, the factorial of {0} is {1}", input, factorialRecurse);
            Console.WriteLine("Using the loop method, the factorial of {0} is {1}", input, factorialLoopIncrease);

            //reverse factorial
            Console.WriteLine("Enter a large number to reverse the calculation: ");
            long find = long.Parse(Console.ReadLine());
            long factorialLoopDecrease = find;
            long number = 0;
            for (int i = 1; factorialLoopDecrease > number; i++)
            {
                long trim = factorialLoopDecrease % i;
                factorialLoopDecrease = factorialLoopDecrease - trim;
                factorialLoopDecrease = factorialLoopDecrease / i;
                number = i;
            }
            long loop = 1;
            for (long i = 1; i < number; i++)
            {
                loop = loop * (i + 1);
            }
            Console.WriteLine("Within the number entered {0}, there is the factorial of {1} which equals {2} with a remainder of {3}",find, number, loop, find - loop);
        }

        public static long MathRecurse(long recurse)
        {
            if (recurse > 1)
            {
                recurse = recurse * MathRecurse(recurse - 1);
            }
            else
                recurse = 1;
            return recurse;
        }

        public static void Invalid(ref bool retry, ref bool request)
        {
            Console.WriteLine("ERROR - Invalid Input...");
            Retry(ref retry, ref request);
        }

        public static Boolean Retry(ref bool retry, ref bool request)
        {
            if (request)
            {
                request = false;
                Console.WriteLine("Continue? (y/n)\n");
                char answer = Console.ReadKey().KeyChar;
                if (answer == 'y' || answer == 'Y')
                {
                    retry = true;
                }
                else if (answer == 'n' || answer == 'N')
                {
                    retry = false;
                }
                else
                    Invalid(ref retry, ref request);
            }
            return retry;
        }

        public static void Exit()
        {
            Console.WriteLine("\nGoodbye! Press the ESCAPE key to exit");
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                Console.ReadKey();
                continue;
            }
        }
    }
}
