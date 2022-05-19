using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace GapSongGenerator
{
    class Program
    {
        public static class KnownFolder
        {
            public static readonly Guid Downloads = new Guid("374DE290-123F-4565-9164-39C4925E467B");
        }

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags,
            IntPtr hToken, out string pszPath);
        
        static void Main(string[] args)
        {
            string path;
            
            if (args.Length > 0)
            {
                path = args[0];
            }
            else
            {
                Console.WriteLine("what is the file path?\npaste it down below");
                path = Console.ReadLine();
            }
            
            string downloadsFolderPath;
            SHGetKnownFolderPath(KnownFolder.Downloads, 0, IntPtr.Zero, out downloadsFolderPath);
            
            String line;


            StreamReader sr = new StreamReader(path);
            StreamWriter srout = new StreamWriter(downloadsFolderPath + @"\output.txt");

            line = sr.ReadLine();

            Random rmd = new Random();

            int switchIt = 1;

            while (line != null)
            {

                if (switchIt == 1)
                {
                    char[] delimiterChars = { ' ', '-', ',' };
                    string[] words = line.Split(delimiterChars);

                    int r = rmd.Next(0, words.Count());

                    string changedword = words.GetValue(r).ToString();


                    char[] charArray = changedword.ToCharArray();

                    string changedwordafter = "";

                    foreach (char c in changedword)
                    {
                        string letter = c.ToString();
                        
                        letter = Regex.Replace(letter, "[^\"]", "."); //Replace all with "." EXCEPT: " 

                        changedwordafter = new string(charArray);
                    }

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