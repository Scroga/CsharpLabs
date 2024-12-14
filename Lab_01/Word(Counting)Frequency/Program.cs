using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace Word_Counting_Frequency
{
    internal class Program
    {
        static void PrintSortedDictionary(Dictionary<string, int> dict)
        {
            var keys = new List<string>(dict.Keys);
            keys.Sort();

            foreach(var key in keys) 
            { 
                Console.WriteLine($"{key}: {dict[key]}");
            }
        }

        static void PrintWordFrequency(string path) 
        {
            Dictionary<string, int> myDict = new Dictionary<string, int>();
            try
            {
                foreach (string line in File.ReadLines(path))
                {
                    string[] words = line.Split(new char[] { ' ', '\n', '\t', }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string word in words)
                    {
                        if (myDict.ContainsKey(word))
                        {
                            myDict[word]++;
                        }
                        else
                        {
                            myDict.Add(word, 1);
                        }
                    }
                }
                PrintSortedDictionary(myDict);
            }
            catch (Exception)
            {
                Console.WriteLine("File Error");
            }
        }

        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                PrintWordFrequency(args[0]);
            }
            else 
            {
                Console.WriteLine("Argument Error");
            }
        }
    }
}
