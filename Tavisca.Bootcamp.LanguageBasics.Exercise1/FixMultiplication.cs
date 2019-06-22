using System;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    class FixMultiplication
    {
        static void Main(string[] args)
        {
            Test("42*47=1?74", 9);
            Test("4?*47=1974", 2);
            Test("42*?7=1974", 4);
            Test("42*?47=1974", -1);
            Test("2*12?=247", -1);
            Console.ReadKey(true);
        }

        private static void Test(string args, int expected)
        {
            var result = FindDigit(args).Equals(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"{args} : {result}");
        }

        public static int FindDigit(string equation)
        {
            // Add your code here.
            String saveUnknownInteger = "", valueInString = "";
            int saveKnownInteger1 = 0, saveKnownInteger2 = 0, value = 0;
            Boolean x = true;
            String[] splitOnMultiplicationSign = equation.Split('*');
            if (splitOnMultiplicationSign[0].Contains("?"))
            {
                saveUnknownInteger = splitOnMultiplicationSign[0];
                saveKnownInteger1 = Int32.Parse(splitOnMultiplicationSign[1].Split('=')[0]);
                saveKnownInteger2 = Int32.Parse(splitOnMultiplicationSign[1].Split('=')[1]);
                value = (saveKnownInteger2 / saveKnownInteger1);
                valueInString = value.ToString();
                if (Math.Ceiling((decimal)((decimal)saveKnownInteger2 / (decimal)saveKnownInteger1)) != value)
                {
                    x = false;
                }
            }
            else if (splitOnMultiplicationSign[1].Contains("?"))
            {
                saveKnownInteger1 = Int32.Parse(splitOnMultiplicationSign[0]);
                String[] splitOnEqualsSign = splitOnMultiplicationSign[1].Split('=');
                if (splitOnEqualsSign[0].Contains("?"))
                {
                    saveUnknownInteger = splitOnEqualsSign[0];
                    saveKnownInteger2 = Int32.Parse(splitOnEqualsSign[1]);
                    value = (saveKnownInteger2 / saveKnownInteger1);
                    valueInString = value.ToString();
                    if (Math.Ceiling((decimal)((decimal)saveKnownInteger2 / (decimal)saveKnownInteger1)) != value)
                    {
                        x = false;
                    }
                }
                else
                {
                    saveKnownInteger2 = Int32.Parse(splitOnEqualsSign[0]);
                    saveUnknownInteger = splitOnEqualsSign[1];
                    value = (saveKnownInteger2 * saveKnownInteger1);
                    valueInString = value.ToString();
                    if (saveKnownInteger2 * saveKnownInteger1 != value)
                    {
                        x = false;
                    }
                }
            }
            if (saveUnknownInteger.Length == valueInString.Length && x == true)
            {
                int y = 0;
                for (int i = 0; i < saveUnknownInteger.Length; i++)
                {
                    if (saveUnknownInteger[i] == '?')
                    {
                        y = Int32.Parse("" + valueInString[i]);
                    }
                }
                return y;
            }
            else
            {
                return -1;
            }
            throw new NotImplementedException();
        }
    }
}
