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

        public static void InputLong(ref long inputLong)
        {
            bool valid = false;

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
        }

        public static void Factorial()
        {
            long inputLong = 0;
            bool retry = true;

            while (retry)
            {
                bool display = true;

                Header(); Console.Write("- Find the Factorial");
                InputLong(ref inputLong);

                //factorial calculation
                long factorial = 1;
                for (long i = 1; i < inputLong; i++)
                {
                    factorial = factorial * (i + 1);
                }

                //display results
                if (inputLong > 20)
                {
                    Console.WriteLine("\nSorry, the factorial of the number you entered is too large to be displayed.\nPlease enter a number between 1 and 20");
                    display = false;
                }
                if (display)
                {
                    Console.WriteLine("The factorial of {0} is {1}", inputLong, factorial);
                }
                retry = Retry();
            }
        }

        public static void FactorialReverse()
        {
            long inputLong = 0;
            bool retry = true;

            while (retry)
            {
                Header(); Console.Write("- Find the Reverse Factorial");
                InputLong(ref inputLong);
                long find = inputLong;

                //console display variables
                long number;
                long factorial;
                List<long> numberList = new List<long>();

                bool remain = true;
                int remainCount = 0;
                while (remain)
                {
                    long reverse = find;
                    number = 0;
                    for (long i = 1; reverse > number; i++)//find largest number possible of factorial
                    {
                        long trim = reverse % i;
                        reverse = reverse - trim;
                        reverse = reverse / i;
                        number = i;
                    }
                    factorial = 1;
                    for (long i = 1; i < number; i++)//calculate the factorial
                    {
                        factorial = factorial * (i + 1);
                    }
                    long remainder = find - factorial;
                    if (remainder >= 0)//set loop condition to find all factorials within input
                    {
                        remainCount = remainCount + 1;
                    }
                    else
                        remain = false;
                    find = remainder;
                    //store values of all n! within input
                    numberList.Add(number);
                }

                //condense answer
                if (remainCount != 0)
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
                numberList.Clear();
                numberList.TrimExcess();
                retry = Retry();
            }
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
