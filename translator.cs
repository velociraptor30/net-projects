using System;
using System.Collections.Generic;
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

        // Load translations into a dictionary
        var translations = LoadTranslations(file);

        if (translations.TryGetValue(word, out string translation))
        {
            Console.WriteLine($"Translation: {translation}");
            return;
        }

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
            string newTranslation = Console.ReadLine()?.Trim();

            if (!string.IsNullOrEmpty(newTranslation))
            {
                // Add bidirectional entries
                File.AppendAllText(file, $"\n{word},{newTranslation}");
                File.AppendAllText(file, $"\n{newTranslation},{word}");
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

    private Dictionary<string, string> LoadTranslations(string file)
    {
        var translations = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        if (File.Exists(file))
        {
            var lines = File.ReadAllLines(file);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 2)
                {
                    var key = parts[0].Trim();
                    var value = parts[1].Trim();

                    if (!translations.ContainsKey(key))
                    {
                        translations[key] = value;
                    }

                    // Ensure bidirectional translation
                    if (!translations.ContainsKey(value))
                    {
                        translations[value] = key;
                    }
                }
            }
        }

        return translations;
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
