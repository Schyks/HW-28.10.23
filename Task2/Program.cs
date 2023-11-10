using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task2
{
    internal class Program
    {
        static  object locker = new object();

        static void Main()
        {
            string filePath = @"C:\Users\Schyks\source\repos\HW-28.10.23\Task2\bin\Debug\text.txt";
            Thread readerThread = new Thread(() =>
            {
                int sentenceCount = CountSentences(filePath);
                Console.WriteLine($"Кiлькiсть речень в файлi: {sentenceCount}");
            });

            Thread modifierThread = new Thread(() =>
            {
                ModifyFile(filePath);
                Console.WriteLine("Файл модифiковано!");
            });
            readerThread.Start();
            modifierThread.Start();
        }

        static int CountSentences(string filePath)
        {
            lock (locker)
            {
                string content = File.ReadAllText(filePath);
                int sentenceCount = CountSent(content);
                return sentenceCount;
            }
        }

        static void ModifyFile(string filePath)
        {
            lock (locker)
            {
                string content = File.ReadAllText(filePath);
                content = content.Replace(",", "/");
                File.WriteAllText(filePath, content);
            }
        }

        static int CountSent(string content)
        {
            int count = 0;
            foreach (char c in content)
            {
                if (c == '.' || c == '?' || c == '!')
                {
                    count++;
                }
            }
            return count;
        }
    }
}
