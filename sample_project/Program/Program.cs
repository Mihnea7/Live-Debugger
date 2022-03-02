using System;
using System.IO;

namespace University
{
    public class Program
    {
        public static void Main(String[] args) {
            String path = "University.Tests/bin/Debug/net6.0/input.txt";
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("3");
                Console.WriteLine("Created input.txt file!");
            }
        }
    }
}
