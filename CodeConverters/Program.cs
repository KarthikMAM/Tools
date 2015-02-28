using System;
using System.IO;

namespace CodeConverters
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream inputFile = new FileStream("karthik.txt", FileMode.OpenOrCreate, FileAccess.Read);
            FileStream outputFile = new FileStream("output.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamReader reader = new StreamReader(inputFile);
            StreamWriter writer = new StreamWriter(outputFile);
            string data;
            while((data = reader.ReadLine()) != null)
            {
                while(Char.IsDigit(data[0]) || data[0] == ' ' || data[0] == '.')
                {
                    data = data.Remove(0,1);
                }
                writer.WriteLine(data);
            }
            writer.Close();
            reader.Close();
            inputFile.Close();
            outputFile.Close();
        }
    }
}
