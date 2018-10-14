using System;
using System.Linq;

namespace Lab5
{
    class Program//starts getting funky at 21
    {
        static void Main(string[] args)
        {
            long recurse = 0;
            bool retry = true;
            bool exit = false;

            while (retry)
            {
                MainMenu(ref recurse, ref retry, ref exit);
                Exit(ref retry, ref exit);
            }
        }

        public static void Header()
        {
            Console.Clear();
            Console.WriteLine("Welcome, to the Factorial Calculator!\n");
        }

        public static void MainMenu(ref long recurse, ref bool retry, ref bool exit)
        {
            Header();
            Console.WriteLine("Please choose an option below by entering the corresponding number:\n");
            Console.WriteLine("1. Find the factorial of a number");
            Console.WriteLine("2. Find the reverse factorial of a number");
            ConsoleKey menuKey = ConsoleKey.Enter;
            while (menuKey != ConsoleKey.Escape)
            {
                menuKey = Console.ReadKey().Key;
                if (menuKey == ConsoleKey.Escape)
                {
                    exit = true;
                    break;
                }
                else if (menuKey == ConsoleKey.D1 || menuKey == ConsoleKey.NumPad1)
                {
                    MathCalcFactorial(ref recurse, ref retry, ref exit);
                    break;
                }
                else if (menuKey == ConsoleKey.D2 || menuKey == ConsoleKey.NumPad2)
                {
                    MathCalcReverseFactorial(ref retry, ref exit);
                    break;
                }
                else
                {
                    Invalid();
                    continue;
                }
            }
            if (menuKey == ConsoleKey.Escape)
            {
                exit = true;
            }
        }

        public static void MathCalcFactorial(ref long recurse, ref bool retry, ref bool exit)
        {
            while (retry)
            {
                Header();

                long input = 0;

                Console.WriteLine("Please enter an integer that's greater than 0 but less than 10: ");
                string userInput = Console.ReadLine();

                if (long.TryParse(userInput, out input) && input > 0)
                {
                    recurse = input;
                }
                else if (userInput.All(Char.IsDigit))
                {
                    Console.WriteLine("Sorry, the number you entered is too large to be displayed");
                    Invalid();
                }
                else
                    Invalid();

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
                    Invalid();
                }
                Console.WriteLine("Using the recursive method, the factorial of {0} is {1}", input, factorialRecurse);
                Console.WriteLine("Using the loop method, the factorial of {0} is {1}", input, factorialLoopIncrease);
                retry = Retry(ref retry, ref exit);
            }
        }

        static void MathCalcReverseFactorial(ref bool retry, ref bool exit)
        {
            while (retry)
            {
                Header();
                Console.WriteLine("Enter a large number to reverse the calculation: ");
                string inputString = Console.ReadLine();
                long findInput = 0;
                long find = 0;
                if (long.TryParse(inputString, out findInput))//store valid input & duplicate
                {
                    find = findInput;
                }
                else if (inputString.All(Char.IsDigit))
                {
                    Console.WriteLine("Sorry, the number you entered is too large to be displayed");
                    Invalid();
                    continue;
                }
                else
                {
                    Invalid();
                    continue;
                }

                //console display variables
                long reverseFactorial;
                long number;
                List<long> numberList = new List<long>();
                List<long> factorialList = new List<long>();
                List<long> remainderList = new List<long>();

                bool remain = true;
                while (remain)
                {
                    long reverse = find;
                    number = 0;
                    for (int i = 1; reverse > number; i++)//find largest number possible of factorial
                    {
                        long trim = reverse % i;
                        reverse = reverse - trim;
                        reverse = reverse / i;
                        number = i;
                    }
                    reverseFactorial = 1;
                    for (long i = 1; i < number; i++)//calculate the factorial
                    {
                        reverseFactorial = reverseFactorial * (i + 1);
                    }
                    long remainder = find - reverseFactorial;
                    if (remainder <= 0)//set loop condition to find all factorials within input
                    {
                        remain = false;
                    }
                    find = remainder;
                    //store values (n!, factorial, remainder)
                    numberList.Add(number);
                    factorialList.Add(reverseFactorial);
                    remainderList.Add(remainder);
                }
                Console.WriteLine("Within the number entered {0}, there is the factorial of {1}! which equals {2} with a remainder of {3}", findInput, numberList[0], factorialList[0], remainderList[0]);
                //condense answer
                if (remainderList[0] != 0)
                {
                    List<KeyValuePair<int, long>> condense = new List<KeyValuePair<int, long>>();
                    for (int i = 0; i < numberList.Count; i++)//create multiplier (key) & remove duplicate values
                    {
                        int key = 1;
                        long value = numberList[i];
                        while (i < numberList.Count - 1 && numberList[i] == numberList[i + 1])
                        {
                            key = key + 1;
                            numberList.Remove(numberList[i + 1]);
                        }
                        condense.Add(new KeyValuePair<int, long>(key, numberList[i]));
                    }
                    Console.Write("\nTherefore, your number {0} =", findInput);//display results
                    bool first = true;
                    foreach (KeyValuePair<int, long> kvp in condense)
                    {
                        if (!first)
                        {
                            Console.Write(" +");
                        }
                        if (kvp.Key == 1)
                        {
                            Console.Write(" {0}!", kvp.Value);
                        }
                        else
                            Console.Write(" {0}({1}!)", kvp.Key, kvp.Value);

                        first = false;
                    }
                }
                retry = Retry(ref retry, ref exit);
            }
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

        public static void Invalid()
        {
            Console.WriteLine("\nInvalid Entry, Please Try Again or Press Escape to Exit... ");
        }

        public static bool Retry(ref bool retry, ref bool exit)
        {
            if (!exit)
            {
                Console.WriteLine("\nEnter a new number? (y/n)\n");
                ConsoleKey answer = Console.ReadKey().Key;
                if (answer == ConsoleKey.Y)
                {
                    retry = true;
                }
                else if (answer == ConsoleKey.N)
                {
                    retry = false;
                }
                else
                {
                    Invalid();
                    Retry(ref retry, ref exit);
                }
            }
            return retry;
        }

        public static void Exit(ref bool retry, ref bool exit)
        {
            Console.WriteLine("\nReturn to Main Menu? (y/n)\n");
            ConsoleKey answer = Console.ReadKey().Key;
            if (answer == ConsoleKey.Y)
            {
                retry = true;
            }
            else if (answer == ConsoleKey.N)
            {
                retry = false;
                Console.WriteLine("\nGoodbye! Press the ESCAPE key to exit");
                while (Console.ReadKey().Key != ConsoleKey.Escape)
                {
                    Console.ReadKey();
                    continue;
                }
            }
            else
            {
                Invalid();
                Retry(ref retry, ref exit);
            }
        }
    }
}
