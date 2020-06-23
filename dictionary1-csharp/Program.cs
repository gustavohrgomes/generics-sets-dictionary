using System;
using System.Collections.Generic;
using System.IO;

namespace course
{
    class Program
    {
        static void Main(string[] args)
        {
           

            Console.Write("Enter file full path: ");
            string path = Console.ReadLine();

            try
            {
                Dictionary<string, int> dictionary = new Dictionary<string, int>();

                using (StreamReader votesReader = File.OpenText(path))
                {
                    while (!votesReader.EndOfStream)
                    {
                        string[] votingRecord = votesReader.ReadLine().Split(',');
                        string candidate = votingRecord[0];
                        int votes = int.Parse(votingRecord[1]);

                        if (dictionary.ContainsKey(candidate))
                        {
                            dictionary[candidate] += votes;
                        }
                        else
                        {
                            dictionary[candidate] = votes;
                        }
                    }

                    foreach (KeyValuePair<string, int> item in dictionary)
                    {
                        Console.WriteLine($"{item.Key},{item.Value}");
                    }
                }
            }
            catch (IOException err)
            {
                Console.WriteLine(err.Message);
            }
            catch (ArgumentException err)
            {
                Console.WriteLine(err.Message);
            }
        }
    }
}
