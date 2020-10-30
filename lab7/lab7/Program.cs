using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            string example = ReadFile(); //"3 + 5 * 2 / 2 * 3 / -1 + 2";

            example = DeleteSpace(example);

            string[] str = Reworking(example);

            int quantityOfActions = CountingAction(str);

            int divisionMultiplication = checkDivisionMultiplication(str, quantityOfActions);
            int additionSubtraction = checkAdditionSubtraction(str, quantityOfActions);

            if (divisionMultiplication + additionSubtraction == quantityOfActions)
            {
                Refusal();
            }

            string result = SelectingAnAction(str, divisionMultiplication);

            Write(result, example);
        }

        static string ReadFile()
        {
            return File.ReadAllText("input.txt");
        }

        static string DeleteSpace(string example)
        {
            return Regex.Replace(example, @"\s+", " ");
        }

        static string[] Reworking(string example)
        {
            return example.Split(new char[] { ' ' });
        }

        static int checkDivisionMultiplication(string[] str, int quantityOfActions)
        {
            int correctActions = 0;

            for (int i = 1; i < quantityOfActions * 2; i += 2)
            {
                if (str[i] == "/" || str[i] == "*")
                    correctActions++;
            }
            return correctActions;
        }
        static int checkAdditionSubtraction(string[] str,int quantityOfActions)
        {
            
            int correctActions = 0;

            for (int i = 1; i < quantityOfActions * 2; i += 2)
            {
                if (str[i] == "+" || str[i] == "-")
                    correctActions++;
            }
            return correctActions;
        }

        static string SelectingAnAction(string[] str,int divisionMultiplication)
        {
            double result = 0;
            double twoNum = 0;
            double oneNum = 0;
            string newExample = "";

            int quantityOfActions = CountingAction(str);

            for (int i = 0; i < divisionMultiplication * 2; i += 2)
            {
                int Actions = CountingAction(str);

                newExample = СalculationMultiplicationAndDivision(str, oneNum, twoNum, result, Actions);
                str = Reworking(newExample);

            }

            quantityOfActions = CountingAction(str);
            return СalculationSubtractionAndAddition(str, oneNum, twoNum, result, quantityOfActions);


        }

        static string СalculationMultiplicationAndDivision(string[] str, double oneNum, double twoNum, double result, int quantityOfActions)
        {
            string newExample = ""; 
            int b = 0;

            for (int i = 0; i < quantityOfActions * 2; i += 2)
            {
                oneNum = Convert.ToInt32(str[i]);
                twoNum = Convert.ToInt32(str[i + 2]);
                if (b == 0)
                {
                    switch (str[i + 1])
                    {
                        case "/":
                            if (twoNum == 0)
                                Refusal();

                            result = oneNum / twoNum;

                            if (i == 0)
                                newExample += $"{result}";
                            else
                                newExample += $" {str[i - 1]} {oneNum / twoNum}";
                            b++;
                            break;

                        case "*":
                            result = oneNum * twoNum;

                            if (i == 0)
                                newExample += $"{result}";
                            else
                                newExample += $" {str[i - 1]} {oneNum * twoNum}";
                            b++;
                            break;

                        default:
                            if (i == 0)
                                newExample += $"{oneNum}";
                            else
                                newExample += $" {str[i - 1]} {str[i]}";
                            break;
                           
                    }
                    result = Math.Round(result, 2);
                }
                else
                    for (int j = i + 1; j < quantityOfActions * 2; j += 2)
                    {
                            newExample += $" {str[j]} {str[j + 1]}";
                        i += 2;
                    }
            }
            return newExample;
        }

        static string СalculationSubtractionAndAddition(string[] str, double oneNum, double twoNum, double result, int quantityOfActions)
        {
            for (int i = 0; i < quantityOfActions * 2; i += 2)
            {
                if (i >= 2)
                    oneNum = result;
                else
                    oneNum = Convert.ToInt32(str[i]);

                twoNum = Convert.ToInt32(str[i + 2]);

                switch (str[i + 1])
                {
                    case "+":
                        result = oneNum + twoNum;
                        break;

                    case "-":
                        result = oneNum - twoNum;
                        break;
                }
                result = Math.Round(result, 2);
            }
            return Convert.ToString(Math.Round(result, 2));
        }

        static int CountingAction(string[] str)
        {
            int quantityOfNumbers = 0;

            for (int i = 0; i < str.Length; i++)
            {
                if (i % 2 == 0)
                    quantityOfNumbers++;
            }
            return str.Length - quantityOfNumbers;
        }

        static void Write(string result, string example)
        {
            Console.WriteLine($"{ example} = { result}");
            File.WriteAllText("output.txt", $"{ example} = { result}");
        }

        static void Refusal()
        {
            Console.WriteLine("Недопустимая операция");
            return;
        }
    }
}