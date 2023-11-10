using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HW_28._10._23
{
    internal class Program
    {
        public static event Action<string> ResultOutput;
        static void Main(string[] args)
        {
            ResultOutput += result => Console.WriteLine(result);
            Generation();
            Summ();
            Calc();
        }
        
        static void Generation()
        {
            int numberOfPairs = 10;
            string filePath = "pairs.txt";
            Random random = new Random();
            using (StreamWriter writer = new StreamWriter(filePath))
            {
               for (int i = 0; i < numberOfPairs; i++)
                {
                    int firstNumber = random.Next(1, 100); 
                    int secondNumber = random.Next(1, 100);
                    writer.WriteLine($"{firstNumber}, {secondNumber}");
                }
            }
            ResultOutput?.Invoke($"Згенеровано та збережено {numberOfPairs} пар чисел у файлi {filePath}.");
        }
        static void Summ()
        {   string filePath = "pairs.txt";
            string filePath1 = $"summ_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.txt";
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] numbers = line.Split(',');

                    if (numbers.Length == 2 && int.TryParse(numbers[0], out int firstNumber) && int.TryParse(numbers[1], out int secondNumber))
                    {
                        int sum = firstNumber + secondNumber;
                        ResultOutput?.Invoke($"Сума чисел {firstNumber} i {secondNumber} дорiвнює {sum}");
                        using (StreamWriter writer = new StreamWriter(filePath1, true))
                        {
                           writer.WriteLine($"{firstNumber} + {secondNumber} = {sum}");
                        }
                    }
                    else
                    {
                        ResultOutput?.Invoke($"Некоректний формат рядка: {line}");
                    }
                }
            }
            else
            {
                ResultOutput?.Invoke($"Файл {filePath} не існує.");
            }
        }
        static void Calc()
        {
            string filePath = "pairs.txt";
            string filePath1 = $"calc_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.txt";
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] numbers = line.Split(',');

                    if (numbers.Length == 2 && int.TryParse(numbers[0], out int firstNumber) && int.TryParse(numbers[1], out int secondNumber))
                    {
                        int sum = firstNumber * secondNumber;
                        ResultOutput?.Invoke($"Добуток чисел {firstNumber} i {secondNumber} дорiвнює {sum}");
                        using (StreamWriter writer = new StreamWriter(filePath1, true))
                        {
                            writer.WriteLine($"{firstNumber} * {secondNumber} = {sum}");
                        }
                    }
                    else
                    {
                        ResultOutput?.Invoke($"Некоректний формат рядка: {line}");
                    }
                }
            }
            else
            {
                ResultOutput?.Invoke($"Файл {filePath} не існує.");
            }
        }
    }
}

