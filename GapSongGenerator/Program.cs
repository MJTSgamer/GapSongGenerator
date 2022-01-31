using System;
using System.IO;
using System.Linq;
using System.Text;

namespace GapSongGenerator
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("what is the file path?\npaste it down below");
            String line;

            string path = Console.ReadLine();

            StreamReader sr = new StreamReader(path);
            StreamWriter srout = new StreamWriter(path.Replace(".txt", "") + "- output.txt");

            line = sr.ReadLine();

            Random rmd = new Random();

            int switchIt = 1;

            while (line != null)
            {

                if (switchIt == 1)
                {
                    string[] words = line.Split(" ");

                    int r = rmd.Next(0, words.Count());

                    string changedword = words.GetValue(r).ToString();


                    string changedwordafter = new String(changedword.Select(r => r == ' ' ? ' ' : '.').ToArray());

                    words.SetValue(changedwordafter, r);

                    string result = words.Aggregate((acc, next) => acc + " " + next);
                    Console.WriteLine(result);

                    srout.WriteLine(result);

                    changedword = String.Empty;

                    switchIt = 2;
                }
                else
                {
                    Console.WriteLine(line);
                    srout.WriteLine(line);
                    switchIt = 1;
                }

                line = sr.ReadLine();
            }

            srout.Close();
            sr.Close();
            Console.ReadLine();

        }
    }
}