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
            var MissingOperand = "";  //storing Missing operand
            var CalculatedMissingOperandInString = ""; //storing Missing operand in string format
            var checkForConditions=-1;  
            int FirstOperand = 0, SecondOperand = 0, CalculatedMissingOperand = 0;
            String[] splitOnMultiplicationSign = equation.Split('*');
            //when 2?*22=484 first operand is missing
            if (splitOnMultiplicationSign[0].Contains("?"))
            {
                MissingOperand = splitOnMultiplicationSign[0];
                String[] splitOnEqualsSign = splitOnMultiplicationSign[1].Split('=');
                FirstOperand = Int32.Parse(splitOnEqualsSign[0]);
                SecondOperand = Int32.Parse(splitOnEqualsSign[1]);
                CalculatedMissingOperand = (SecondOperand / FirstOperand);
                CalculatedMissingOperandInString = CalculatedMissingOperand.ToString();
                checkForConditions = VerifyIfMissingOperandIsCorrect(FirstOperand, CalculatedMissingOperand, SecondOperand, '/');
            }
            else if (splitOnMultiplicationSign[1].Contains("?"))
            {
                FirstOperand = Int32.Parse(splitOnMultiplicationSign[0]);
                String[] splitOnEqualsSign = splitOnMultiplicationSign[1].Split('=');
                //when 22*2?=484 second operand is missing
                if (splitOnEqualsSign[0].Contains("?"))
                {
                    MissingOperand = splitOnEqualsSign[0];
                    SecondOperand = Int32.Parse(splitOnEqualsSign[1]);
                    CalculatedMissingOperand = (SecondOperand / FirstOperand);
                    CalculatedMissingOperandInString = CalculatedMissingOperand.ToString();
                    checkForConditions = VerifyIfMissingOperandIsCorrect(FirstOperand, CalculatedMissingOperand, SecondOperand, '/');
                }
                //when 22*2?=48? third operand is missing
                else
                {
                    SecondOperand = Int32.Parse(splitOnEqualsSign[0]);
                    MissingOperand = splitOnEqualsSign[1];
                    CalculatedMissingOperand = (FirstOperand * SecondOperand);
                    CalculatedMissingOperandInString = CalculatedMissingOperand.ToString();
                    checkForConditions = VerifyIfMissingOperandIsCorrect(FirstOperand, SecondOperand, CalculatedMissingOperand, '*');
                }
            }
            if (MissingOperand.Length == CalculatedMissingOperandInString.Length && checkForConditions == 1)
            {
                return FetchingMissingIntegerValue(MissingOperand, CalculatedMissingOperandInString);
            }
            else
            {
                return -1;
            }
        }

        //function to fetch the missing value from given missing operand and calculated operand

        private static int FetchingMissingIntegerValue(String MissingOperand, String CalculatedMissingOperand)
        {
            // given equation 22*2?=484
            //MissingOperand=2?
            //CalculatedMissingOperand=22
            int MissingIntegerValue = 0;
            for (int i = 0; i < MissingOperand.Length; i++)
            {
                if (MissingOperand[i] == '?')
                {
                    MissingIntegerValue = Int32.Parse("" + CalculatedMissingOperand[i]);
                }
            }
            return MissingIntegerValue;
        }

        //function to check if the calculated value of missing operand is correct or not

        private static int VerifyIfMissingOperandIsCorrect(decimal FirstOperand, decimal SecondOperand,int ThirdOperand, char Operator)
        {
            try
            {
                //if equation is 22*2?=484
                //then check 484/22 == 22
                if (Operator == '/')
                {
                    if (Math.Ceiling(ThirdOperand / FirstOperand) != SecondOperand)
                        return 0;
                    else return 1;
                }
                //if equation is 22*22=48?
                //then check 22*22 == 484
                else if (Operator == '*')
                {
                    if (SecondOperand * FirstOperand != ThirdOperand)
                        return 0;
                    else return 1;
                }
                else return -1;
            }
            catch(DivideByZeroException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Cannot divide by zero");
                return -1;
            }
        }
    }
}
