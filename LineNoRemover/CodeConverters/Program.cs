using System;
using System.IO;

namespace CodeConverters
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceFile, destinationFile;

            Console.Write("\n\n\tENTER THE SOURCE : ");
            sourceFile = Console.ReadLine();
            Console.Write("\n\n\tENTER THE TARGET (DIFFERENT FROM SOURCE) : ");
            destinationFile = Console.ReadLine();

            try
            {
                FileStream inputFile = new FileStream(sourceFile, FileMode.OpenOrCreate, FileAccess.Read);
                FileStream outputFile = new FileStream(destinationFile, FileMode.OpenOrCreate, FileAccess.Write);
                StreamReader reader = new StreamReader(inputFile);
                StreamWriter writer = new StreamWriter(outputFile);
                string data;
                while ((data = reader.ReadLine()) != null)
                {
                    while (Char.IsDigit(data[0]) || data[0] == ' ' || data[0] == '.')
                    {
                        data = data.Remove(0, 1);
                    }
                    writer.WriteLine(data);
                }
                writer.Close();
                reader.Close();
                inputFile.Close();
                outputFile.Close();

                Console.WriteLine("\n\n\tTHE FILE HAS BEEN SUCCESSFULLY CONVERTED AND SAVED IN {0}", Path.GetFileName(destinationFile));
            }
            catch(IOException e)
            {
                Console.WriteLine("\n\n\t EXCEPTION : " + e.Message);
            }
        }
    }
}
