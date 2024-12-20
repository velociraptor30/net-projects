using System.Text.RegularExpressions;
using System;

namespace OperatorOverride
{

    public class Book  // Can this be 
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int YearOfPublishing { get; set; }

        public Book(string title, string author, int yearOfPublishing)
        {
            Title = title;
            Author = author;
            YearOfPublishing = yearOfPublishing;
        }
        public Book()
        {
        }
        
    }

    public class BookManager:Book
    {
        private List<Book> bookCollection;
        
        string file = @"/Users/kenkadav/RiderProjects/books/Textfile.txt";

        public BookManager() : base("", "", 0) // Calls base class constructor with dummy values
        {
            bookCollection = new List<Book>();
        }

        public void GetBooks()
        {   
            //var books = new List<Book>();
            if (File.Exists(file)) { 
                // Store each line in array of strings 
                string[] lines = File.ReadAllLines(file); 
  
                foreach(string ln in lines) 
                    Console.WriteLine(ln); 
                
            } else { Console.WriteLine("File not found"); } 
        }
        
        public void AddBook()
        {   
            Console.WriteLine("Input title of the book:");
            var title = Console.ReadLine();
            Console.WriteLine("Input author of the book:");
            var author = Console.ReadLine();
            Console.WriteLine("Input publishing year of the book:");
            int year;
            while (true) // Keep prompting until the user enters valid digits
            {
                Console.WriteLine("Input publishing year of the book (digits only):");
                var yearInput = Console.ReadLine();

                if (int.TryParse(yearInput, out year) && year > 0 && year <= DateTime.Now.Year) // Ensure it's a positive integer
                {
                    break; // Valid input; exit the loop
                }
                else
                {
                    Console.WriteLine("Invalid year. Please enter a valid number (digits only).");
                }
            }

            string book = $"\n{title},{author},{year}"; 
            File.AppendAllText(file, book); 
            
            Console.WriteLine(); 
            Console.WriteLine($"Book '{title}' by {author} added successfully.");
            
            //Console.WriteLine(File.ReadAllText(file));
        }

        public void SearchBooks()
        {
            Console.WriteLine("Enter search term (part of title, author, or year):");
            var searchTerm = Console.ReadLine()?.ToLower(); // Convert to lowercase for case-insensitive search

            if (File.Exists(file))
            {
                // Read all lines from the file
                var lines = File.ReadAllLines(file);
                var found = false;

                foreach (string line in lines)
                {
                    // Check if the line contains the search term
                    if (line.ToLower().Contains(searchTerm))
                    {
                        Console.WriteLine(line);
                        found = true;
                    }
                }

                if (!found)
                {
                    Console.WriteLine("No matching books found.");
                }
            }
            else
            {
                Console.WriteLine("File not found. No books to search.");
            }
        }
        
        
    }
    partial class Program
    {
        
        
        static void Main(string[] args)
        {
            
            BookManager bookManager = new BookManager();

            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Display all books");
                Console.WriteLine("2. Add a new book");
                Console.WriteLine("3. Search for a book");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        bookManager.GetBooks();
                        break;
                    case "2":
                        bookManager.AddBook();
                        break;
                    case "3":
                        bookManager.SearchBooks();
                        break;
                    case "4":
                        return; // Exit the program
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
            
            
        }
        

            
    }
}

