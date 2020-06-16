using System;
using System.IO;
using System.Collections.Generic;

using course.Entities;

namespace course
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<LogRecord> set = new HashSet<LogRecord>();

            Console.Write("Enter file full path: ");
            string path = Console.ReadLine();

            try 
            {
                using (StreamReader streamReader = File.OpenText(path)) 
                {
                    while(!streamReader.EndOfStream)
                    {
                        string[] line = streamReader.ReadLine().Split(' ');
                        string name = line[0];
                        DateTime insert = DateTime.Parse(line[1]);
                        set.Add(new LogRecord(name, insert));
                    }
                    Console.Write("Total Users: " + set.Count);
                }
            }
            catch (IOException e)
            {
                Console.Write(e.Message);
            }
        }
    }
}
