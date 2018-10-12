using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    class Program//starts getting funky at 21
    {
        static void Main(string[] args)
        {
            string userName = null;
            long recurse = 0;
            bool request = true;
            bool retry = true;

            Greeting(ref userName);
            while (retry)
            {
                Console.Clear();
                MathCalc(ref recurse, userName, ref retry, ref request);
                retry = Retry(ref retry, userName, ref request);
            }
            Exit(userName);
        }

        public static void Greeting(ref string userName)
        {
            Console.WriteLine("Please enter your name: ");
            userName = Console.ReadLine();
        }

        public static void MathCalc(ref long recurse, string userName, ref bool retry, ref bool request)
        {
            long input = 0;

            request = true;
            Console.WriteLine("Welcome {0}, to the Factorial Calculator!\nPlease enter an integer that's greater than 0 but less than 10: ", userName);
            string userInput = Console.ReadLine();
            if (long.TryParse(userInput, out input) && input > 0)
            {
                recurse = input;
            }
            else if (userInput.All(Char.IsDigit))
            {
                Console.WriteLine("\nSorry {0}, the number you entered is too large to be displayed", userName);
                Invalid(ref retry, userName, ref request);
            }
            else
                Invalid(ref retry, userName, ref request);
            if (input > 10)
            {
                Console.WriteLine("Tisk, Tisk, {0}, you entered a number greater than 10! Lucky for you, I removed those constraints :)");
            }
            //using recursive
            long factorialRecurse = MathRecurse(recurse);
            //using increasing loop
            long factorialLoopIncrease = 1;
            for (long i = 1; i < input; i++)
            {
                factorialLoopIncrease = factorialLoopIncrease * (i + 1);
            }
            if (factorialLoopIncrease > long.MaxValue || factorialRecurse > long.MaxValue)
            {
                Console.WriteLine("Sorry {0}, the factorial of the number you entered is too large to be displayed", userName);
                Invalid(ref retry, userName, ref request);
            }
            Console.WriteLine(userName + ", using the recursive method, the factorial of {0} is {1}", input, factorialRecurse);
            Console.WriteLine(userName + ", using the i++ loop method, the factorial of {0} is {1}", input, factorialLoopIncrease);

            //using decreasing loop
            Console.WriteLine("Enter a large number to reverse the calculation: ");
            //long find = long.Parse(Console.ReadLine());
            //long factorialLoopDecrease = find;
            long factorialLoopDecrease = factorialLoopIncrease;
            long number = 0;
            for (int i = 1; factorialLoopDecrease % i == 0; i++)
            {
                Console.WriteLine("i = {0}, j = {1}", factorialLoopDecrease, i);
                factorialLoopDecrease = factorialLoopDecrease / i;
                number = i;
                /*if (i - 1 > 0)
                {
                    long trim = factorialLoopDecrease % (i + 1);
                    factorialLoopDecrease = factorialLoopDecrease - trim;
                    Console.WriteLine("i = {0}, j = {1}", factorialLoopDecrease, i);
                }
                else
                    break;*/

            }
            long loop = 1;
            for (long i = 1; i < number; i++)
            {
                loop = loop * (i + 1);
            }
            Console.WriteLine(userName + ", using the i-- loop method, the factorial of {0} is {1}", number, loop);

            /*
            //max factorial int
            int maxFactorialINT = int.MaxValue;
            int maxNumberINT = 0;
            for (int i = maxFactorialINT, j = 0; i > 0; i--, j++)
            {
                maxFactorialINT = maxFactorialINT * (i - 1);
                int trim = maxFactorialINT % (i - 1);
                maxFactorialINT = maxFactorialINT - trim;
                maxNumberINT = j;
            }
            int maxLoopINT = 1;
            for (int i = 1; i < maxNumberINT; i++)
            {
                maxLoopINT = maxLoopINT * (maxLoopINT - 1);
            }
            Console.WriteLine(userName + ", using the i-- loop method, the factorial of {0} is {1}, which is the largest <int> capable of being calculated.", maxNumberINT, maxLoopINT);
            //max factorial uint
            uint maxFactorialUINT = uint.MaxValue;
            uint maxNumberUINT = 0;
            for (uint i = maxFactorialUINT, j = 0; i > 0; i--, j++)
            {
                maxFactorialUINT = maxFactorialUINT * (i - 1);
                uint trim = maxFactorialUINT % (i - 1);
                maxFactorialUINT = maxFactorialUINT - trim;
                maxNumberUINT = j;
            }
            uint maxLoopUINT = 1;
            for (uint i = 1; i < maxNumberUINT; i++)
            {
                maxLoopUINT = maxLoopUINT * (maxLoopUINT - 1);
            }
            Console.WriteLine(userName + ", using the i-- loop method, the factorial of {0} is {1}, which is the largest <uint> capable of being calculated.", maxNumberUINT, maxLoopUINT);
            //max factorial long
            long maxFactorialLong = long.MaxValue;
            long maxNumberLong = 0;
            for (long i = maxFactorialLong, j = 0; i > 0; i--, j++)
            {
                maxFactorialLong = maxFactorialLong * (i - 1);
                long trim = maxFactorialLong % (i - 1);
                maxFactorialLong = maxFactorialLong - trim;
                maxNumberLong = j;
            }
            long maxLoopLong = 1;
            for (long i = 1; i < maxNumberLong; i++)
            {
                maxLoopLong = maxLoopLong * (maxLoopLong - 1);
            }
            Console.WriteLine(userName + ", using the i-- loop method, the factorial of {0} is {1}, which is the largest <long> capable of being calculated.", maxNumberLong, maxLoopLong);
            */
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

        public static void Invalid(ref bool retry, string userName, ref bool request)
        {
            Console.WriteLine("ERROR - Invalid Input...");
            Retry(ref retry, userName, ref request);
        }

        public static Boolean Retry(ref bool retry, string userName, ref bool request)
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
                    Invalid(ref retry, userName, ref request);
            }
            return retry;
        }

        public static void Exit(string userName)
        {
            Console.WriteLine("\nGoodbye {0}! Press the ESCAPE key to exit", userName);
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                Console.ReadKey();
                continue;
            }
        }
    }
}
