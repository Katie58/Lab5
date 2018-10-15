using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                MainMenu();
                exit = ReturnToMain();
            }
            Exit();
        }

        public static void Header()
        {
            Console.Clear();
            Console.Write("Welcome, to the Factorial Calculator! ");
        }

        public static void MainMenu()
        {
            Header();
            Console.WriteLine("\n\nPlease choose an option below by entering the corresponding number: \n");

            List<KeyValuePair<string, Action>> menu = new List<KeyValuePair<string, Action>>();//to add additional menu items, simply add to list and create a new method
            menu.Add(new KeyValuePair<string, Action>("Find the Factorial", () => Factorial()));
            menu.Add(new KeyValuePair<string, Action>("Find the Reverse Factorial", () => FactorialReverse()));
            menu.Add(new KeyValuePair<string, Action>("View Max Factorials of Computer Integer Types", () => FactorialMaxType()));

            int index = 0;
            foreach (KeyValuePair<string, Action> item in menu)//displays menu
            {
                index = index + 1;
                Console.WriteLine("{0}. {1}", index, item.Key);
            }
            Console.WriteLine(Environment.NewLine);

            if (int.TryParse(Console.ReadLine(), out int entry) && entry > 0 && entry <= menu.Count)
            {
                menu[entry - 1].Value.Invoke();
            }
            else
            {
                Invalid();
            }
            menu.Clear();
            menu.TrimExcess();
        }

        public static long InputLong()
        {
            bool valid = false;
            long inputLong = 0;

            while (!valid)
            {
                Console.Write("\n\nPlease enter an integer to be calculate:  ");
                string input = Console.ReadLine();
                if (long.TryParse(input, out inputLong) && inputLong > 0)
                {
                    valid = true;
                }
                else if (input.All(Char.IsDigit))
                {
                    Console.WriteLine("\nSorry, the number you entered is too large to be displayed");
                    Invalid();
                    continue;
                }
                else
                {
                    Invalid();
                    continue;
                }
            }
            return inputLong;
        }

        public static void Factorial()
        {
            bool retry = true;

            while (retry)
            {
                Header(); Console.Write("- Find the Factorial");
                long number = InputLong();//get n!
                long factorial = FactorialCalc(number);//get factorial
                FactorialDisplay(number, factorial);//display results

                retry = Retry();
            }
        }

        public static long FactorialCalc(long number)//calculate factorial
        {
            long factorial = 1;
            for (long i = 1; i < number; i++)
            {
                factorial = factorial * (i + 1);
            }
            return factorial;
        }

        public static void FactorialDisplay(long number, long factorial)
        {
            bool display = true;

            if (InputLong() > 20)
            {
                Console.WriteLine("\nSorry, the factorial of the number you entered is too large to be displayed.\nPlease enter a number between 1 and 20");
                display = false;
            }
            if (display)
            {
                Console.WriteLine("{0}! = {1}", number, factorial);
            }
        }

        /*//////////////////////////////////////////////////// recursion method
        public static void FactorialRecursion()
        {
            long number = InputLong();
            long factorial = RecursionCalc(number);
            FactorialDisplay(number, factorial);
        }

        public static long RecursionCalc(long number)
        {
            long factorial = 1;
            for (long i = 1; i < number; i++)
            {
                factorial = factorial * (i + 1);
            }
            return factorial;
        }*////////////////////////////////////////////////////

        public static void FactorialReverse()
        {
            bool retry = true;

            while (retry)
            {
                Header(); Console.Write("- Find the Reverse Factorial");//header
                long inputLong = InputLong();//get user input
                FactorialReverseDisplay(inputLong);//calculate & display

                retry = Retry();
            }
        }

        public static List<long> FactorialReverseCalc(long inputLong)//calculate reverse factorial
        {
            long find = inputLong;
            List<long> numbers = new List<long>();
            bool remain = true;
            while (remain)
            {
                long reverse = find;
                long number = 0;
                for (long i = 1; reverse > number; i++)//find largest number possible of factorial
                {
                    long trim = reverse % i;
                    reverse = reverse - trim;
                    reverse = reverse / i;
                    number = i;
                }
                numbers.Add(number);//store values of all n! within input
                long remainder = find - FactorialCalc(number);
                find = remainder;
                if (remainder <= 0)//set loop condition to find all factorials within input
                {
                    remain = false;
                }
            }
            return numbers;
        }

        public static void FactorialReverseDisplay(long inputLong)
        {
            List<long> numbers = FactorialReverseCalc(inputLong);//calculate
            if (numbers.Count > 1)//create multiplier (key) & remove duplicate values to condense result
            {
                List<KeyValuePair<int, long>> condense = new List<KeyValuePair<int, long>>();
                for (int i = 0; i < numbers.Count; i++)//
                {
                    int key = 1;
                    long value = numbers[i];
                    while (i < numbers.Count - 1 && numbers[i] == numbers[i + 1])
                    {
                        key = key + 1;
                        numbers.Remove(numbers[i + 1]);
                    }
                    condense.Add(new KeyValuePair<int, long>(key, numbers[i]));
                }
                Console.Write("{0} =", inputLong);//display results
                bool first = true;
                foreach (KeyValuePair<int, long> kvp in condense)
                {
                    if (kvp.Value > 0)
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
                    }
                    first = false;
                }
                condense.Clear();
                condense.TrimExcess();
            }
            numbers.Clear();
            numbers.TrimExcess();
        }

        public static void FactorialMaxType()
        {
            Header(); Console.WriteLine(" - View Max Factorials of Computer Integer Types\n");
            List<KeyValuePair<string, long>> max = new List<KeyValuePair<string, long>>();
            max.Add(new KeyValuePair<string, long>("sbyte MaxValue = ", sbyte.MaxValue));
            max.Add(new KeyValuePair<string, long>("byte MaxValue = ", byte.MaxValue));
            max.Add(new KeyValuePair<string, long>("short MaxValue = ", short.MaxValue));
            max.Add(new KeyValuePair<string, long>("ushort MaxValue = ", ushort.MaxValue));
            max.Add(new KeyValuePair<string, long>("int MaxValue = ", int.MaxValue));
            max.Add(new KeyValuePair<string, long>("uint MaxValue = ", uint.MaxValue));
            max.Add(new KeyValuePair<string, long>("long MaxValue = ", long.MaxValue));

            foreach (KeyValuePair<string, long> kvp in max)
            {
                Console.WriteLine(kvp.Key + kvp.Value);
                FactorialReverseDisplay(kvp.Value);
                Console.WriteLine(Environment.NewLine);
            }
            Console.WriteLine("ulong MaxValue = N/A\n");
            Console.WriteLine("BigInteger MaxValue = infinite");
        }

        public static void Invalid()
        {
            Console.WriteLine("\nInvalid Entry, Please Try Again... ");
        }

        public static bool Retry()
        {
            bool retry = true;
            Console.Write("\n\nEnter a new number? (y/n)  ");
            while (retry)
            {
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.Y)
                {
                    break;
                }
                else if (key == ConsoleKey.N)
                {
                    retry = false;
                }
                else
                {
                    Invalid();
                    continue;
                }
            }
            return retry;
        }

        public static bool ReturnToMain()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Write("\nReturn to Main Menu? (y/n)  ");
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.Y)
                {
                    break;
                }
                else if (key == ConsoleKey.N)
                {
                    exit = true;
                }
                else
                {
                    Invalid();
                }
            }
            return exit;
        }

        public static void Exit()
        {
            Console.WriteLine("\nGoodbye! Press the ESCAPE key to exit");
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                continue;
            }
        }
    }
}
