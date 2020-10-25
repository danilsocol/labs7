using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace lab7
{
    class Program
    {
        static void Main(string[] args)
        {
           string example = ReadFile();

            example = DeleteSpace(example);

           string[] str = Reworking(example);

            if (check(str))
            {
                Refusal();
            }


           string result =  SelectingAnAction(str);

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

        static bool check(string[] str)
        {
            return str[1] != "+" && str[1] != "-" && str[1] != "/" && str[1] != "*";
        }

        static string SelectingAnAction(string[] str)
        {
            int oneNum = Convert.ToInt32(str[0]);
            int twoNum = Convert.ToInt32(str[2]);
            double result;

            switch (str[1]) 
            {

                case "+":
                    result = (double) oneNum + twoNum;
                    break;

                case "-":
                    result = (double) oneNum - twoNum;
                    break;

                case "/":
                    if (twoNum == 0)
                        Refusal();
                    result = (double) oneNum / twoNum;
                    break;

                case "*":
                    result = (double) oneNum * twoNum;
                    break;

                default:
                    return "?";
                    
            }
            return Convert.ToString(Math.Round(result,2));

        }

        static void Write(string result,string example)
        {
            Console.WriteLine($"{ example} = { result}");
            File.WriteAllText("output.txt",$"{ example} = { result}");
        }

        static void Refusal()
        {
            Console.WriteLine("Недопустимая операция");
            return;
        }
    }
}
