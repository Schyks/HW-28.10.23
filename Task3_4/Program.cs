using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task3_4
{
    internal class Program
    {
            static List<int> randomNumbers = new List<int>();
            static List<int> primeNumbers = new List<int>();
            static List<int> primeNumbers_7 = new List<int>();

            static Mutex mutex = new Mutex(); 
   
            static void Main()
            {
                Thread thread1 = new Thread(GenerateRandomNumbers);
                Thread thread2 = new Thread(ProcessRandomNumbers);
                Thread thread3 = new Thread(ProcessPrimeNumbers);
                Thread thread4 = new Thread(GenerateReport);
                thread1.Start();
                thread2.Start();
                thread3.Start();
                thread4.Start();
            Console.WriteLine("Потоки вiдпрацювали!");
        }

            static void GenerateRandomNumbers()
            {
                mutex.WaitOne();
                string filePath = "randomNumbers.txt";
                Random rand = new Random();
                for (int i = 0; i < 100; i++)
                {
                    int randomNumber = rand.Next(1, 100);
                    randomNumbers.Add(randomNumber);
                }
            randomNumbers=randomNumbers.Distinct().ToList(); 
            randomNumbers.Sort();
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (int number in randomNumbers)
                {
                    writer.WriteLine(number);
                }
            }
            mutex.ReleaseMutex();
            }

            static void ProcessRandomNumbers()
            {
                mutex.WaitOne();
            string filePath = "primeNumbers.txt";
            string[] lines = File.ReadAllLines("randomNumbers.txt");
                randomNumbers = lines.Select(int.Parse).ToList();
                primeNumbers = randomNumbers.Where(IsPrime).ToList();
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (int number in primeNumbers)
                {
                    writer.WriteLine(number);
                }
            }
            mutex.ReleaseMutex();
            }

            static void ProcessPrimeNumbers()
            {
            mutex.WaitOne();
            string filePath = "primeNumbers_7.txt";
            string[] lines = File.ReadAllLines("primeNumbers.txt");
                primeNumbers = lines.Select(int.Parse).ToList();
                primeNumbers_7 = primeNumbers.Where(num => num % 10 == 7).ToList();
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (int number in primeNumbers_7)
                {
                    writer.WriteLine(number);
                }
            }
            mutex.ReleaseMutex();
            }
        static void GenerateReport()
        {
            mutex.WaitOne();
            using (StreamWriter writer = new StreamWriter("report.txt"))
            {
                WriteFileReport(writer, "randomNumbers.txt");
                WriteFileReport(writer, "primeNumbers.txt");
                WriteFileReport(writer, "primeNumbers_7.txt");
            }
            mutex.ReleaseMutex();
        }
        static void WriteFileReport(StreamWriter writer, string fileName)
        {
            writer.WriteLine($"Iнформацiя про файл: {fileName}");
            writer.WriteLine($"- Кiлькiсть елементiв: {File.ReadLines(fileName).Count()}");
            writer.WriteLine($"- Розмiр файлу (bytes): {new FileInfo(fileName).Length}");
            writer.WriteLine($"- Вмiст файлу:");
            writer.WriteLine(File.ReadAllText(fileName));
            writer.WriteLine();
        }
        static bool IsPrime(int num)
            {
                if (num < 2)
                    return false;

                for (int i = 2; i <= Math.Sqrt(num); i++)
                {
                    if (num % i == 0)
                        return false;
                }
                return true;
            }
        }
    }

