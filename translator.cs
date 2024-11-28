using System;
using System.IO;

public class Translator
{
    private string file1 = @"/Users/kenkadav/RiderProjects/translator/Textfile1.txt"; // GEO-ENG
    private string file2 = @"/Users/kenkadav/RiderProjects/translator/Textfile2.txt"; // GEO-RUS

    public void Translate(string file)
    {
        Console.WriteLine("Enter a word to translate:");
        string word = Console.ReadLine()?.Trim();

        if (string.IsNullOrEmpty(word))
        {
            Console.WriteLine("You must enter a word.");
            return;
        }

        bool found = false;

        if (File.Exists(file))
        {
            var lines = File.ReadAllLines(file);

            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 2 && parts[0].Trim().Equals(word, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Translation: {parts[1].Trim()}");
                    found = true;
                    break;
                }
            }
        }
        else
        {
            Console.WriteLine($"File not found: {file}");
            return;
        }

        if (!found)
        {
            Console.WriteLine("Translation not found. Would you like to add it? (yes/no)");
            string response;
            while (true)
            {
                response = Console.ReadLine()?.Trim().ToLower();
                if (response == "yes" || response == "no")
                {
                    break; // Valid response
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
                }
            }

            if (response == "yes")
            {
                Console.WriteLine("Enter the translation:");
                string translation = Console.ReadLine()?.Trim();

                if (!string.IsNullOrEmpty(translation))
                {
                    File.AppendAllText(file, $"\n{word},{translation}");
                    Console.WriteLine("Translation added successfully.");
                }
                else
                {
                    Console.WriteLine("No translation provided. Nothing was added.");
                }
            }
            else
            {
                Console.WriteLine("No changes made.");
            }
        }
    }
}


partial class Program
{
    static void Main(string[] args)
    {
        Translator translator = new Translator();

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Translate between Georgian and English or vice-versa");
            Console.WriteLine("2. Translate between Georgian and Russian or vice-versa");
            Console.WriteLine("3. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    translator.Translate(@"/Users/kenkadav/RiderProjects/translator/Textfile1.txt");
                    break;
                case "2":
                    translator.Translate(@"/Users/kenkadav/RiderProjects/translator/Textfile2.txt");
                    break;
                case "3":
                    return; // Exit the program
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}
