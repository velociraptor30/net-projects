using System;
using System.Collections.Generic;
using System.IO;


namespace atmMachine
{

    public class CardHolder
    {
        // Properties
        public string Name { get; set; }

        public int CardNumber { get; set; }
        public double Balance { get; set; }
        public int Pin { get; set; }

        // Constructor
        public CardHolder(string name, int cardNumber, double balance, int pin)
        {
            Name = name;
            CardNumber = cardNumber;
            Balance = balance;
            Pin = pin;
        }

        // Path to the file
        private static readonly string FilePath = "/Users/kenkadav/RiderProjects/atmMachine/CardHolders.txt";



        // Method to display card holder information
        public void DisplayInfo()
        {
            var cardHolders = ReadCardHoldersFromFile();
            var cardHolder = cardHolders.Find(ch => ch.Name == Name && ch.CardNumber == CardNumber);
            if (cardHolder != null)
            {
                Console.WriteLine($"Name: {cardHolder.Name}");
                Console.WriteLine($"Balance: {cardHolder.Balance:C}");
            }
            else
            {
                Console.WriteLine("Card holder not found.");
            }

        }

        // Method to deposit money
        public void Deposit(double amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                UpdateBalanceInFile();
                Console.WriteLine($"{amount:C} deposited successfully.");
            }
            else
            {
                Console.WriteLine("Deposit amount must be positive.");
            }
        }

        // Method to withdraw money
        public void Withdraw(double amount)
        {
            if (amount > 0 && amount <= Balance)
            {
                Balance -= amount;
                UpdateBalanceInFile();
                Console.WriteLine($"{amount:C} withdrawn successfully.");
            }
            else
            {
                Console.WriteLine("Invalid withdrawal amount.");
            }
        }
        
        // Helper method to read card holders from the file
        public static List<CardHolder> ReadCardHoldersFromFile()
        {
            var cardHolders = new List<CardHolder>();
            if (File.Exists(FilePath))
            {
                var lines = File.ReadAllLines(FilePath);
                foreach (var line in lines)
                {
                    var data = line.Split(',');
                    if (data.Length == 4 &&
                        int.TryParse(data[1], out int cardNumber) &&
                        double.TryParse(data[2], out double balance) &&
                        int.TryParse(data[3], out int pin))
                    {
                        cardHolders.Add(new CardHolder(data[0], cardNumber, balance, pin));
                    }
                }
            }

            return cardHolders;
        }
        
        public void UpdatePin(int newPin)
        {
            Pin = newPin;
            var cardHolders = ReadCardHoldersFromFile();
            for (int i = 0; i < cardHolders.Count; i++)
            {
                if (cardHolders[i].Name == Name && cardHolders[i].CardNumber == CardNumber)
                {
                    cardHolders[i].Pin = Pin; // Update the PIN
                }
            }
            WriteCardHoldersToFile(cardHolders);
            Console.WriteLine("PIN updated successfully.");
        }
        
        // Helper method to update balance in the file
        private void UpdateBalanceInFile()
        {
            var cardHolders = ReadCardHoldersFromFile();
            for (int i = 0; i < cardHolders.Count; i++)
            {
                if (cardHolders[i].Name == Name && cardHolders[i].CardNumber == CardNumber)
                {
                    cardHolders[i].Balance = Balance;
                }
            }

            WriteCardHoldersToFile(cardHolders);
        }

        private static void WriteCardHoldersToFile(List<CardHolder> cardHolders)
        {
            var lines = new List<string>();
            foreach (var holder in cardHolders)
            {
                lines.Add($"{holder.Name},{holder.CardNumber},{holder.Balance},{holder.Pin}");
            }

            File.WriteAllLines(FilePath, lines);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // void printOptions()
            // {   
            //     //DEFAULT - LOGIN FIRST
            //     Console.WriteLine("Please choose from following options: ");
            //     Console.WriteLine("1. Deposit ");
            //     Console.WriteLine("2. Withdraw ");
            //     Console.WriteLine("3. Show Balance ");
            //     Console.WriteLine("4. Change PIN ");
            //     Console.WriteLine("5. Transfer Money "); // TO DO - Inheritance 
            //     Console.WriteLine("6. Register ");
            //     Console.WriteLine("7. Exit ");
            // }

            Console.WriteLine("Welcome to the ATM!");
            Console.Write("Enter your name: ");
            var name = Console.ReadLine();
            Console.Write("Enter your PIN: ");
            var pin = int.Parse(Console.ReadLine());

            var cardHolders = CardHolder.ReadCardHoldersFromFile();
            
            var currentUser = cardHolders.Find(ch => ch.Name == name && ch.Pin == pin);

            if (currentUser == null)
            {
                Console.WriteLine("Invalid credentials. Exiting.");
                return;
            }

            Console.WriteLine($"Welcome, {currentUser.Name}!");

            // ATM functionality
            bool running = true;
            while (running)
            {
                Console.WriteLine("Please choose from following options: ");
                Console.WriteLine("1. Display Info ");
                Console.WriteLine("2. Deposit ");
                Console.WriteLine("3. Withdraw ");
                Console.WriteLine("4. Change PIN "); 
                Console.WriteLine("5. Exit ");  
                Console.Write("Choose an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        currentUser.DisplayInfo();
                        break;
                    case "2":
                        Console.Write("Enter amount to deposit: ");
                        double deposit = double.Parse(Console.ReadLine());
                        currentUser.Deposit(deposit);
                        break;
                    case "3":
                        Console.Write("Enter amount to withdraw: ");
                        double withdraw = double.Parse(Console.ReadLine());
                        currentUser.Withdraw(withdraw);
                        break;
                    case "4":
                        Console.Write("Enter your current PIN: ");
                        if (int.TryParse(Console.ReadLine(), out int currentPin) && currentPin == currentUser.Pin)
                        {
                            Console.Write("Enter your new PIN: ");
                            if (int.TryParse(Console.ReadLine(), out int newPin))
                            {
                                currentUser.UpdatePin(newPin);
                            }
                            else
                            {
                                Console.WriteLine("Invalid new PIN.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Incorrect current PIN.");
                        }
                        break;
                    case "5":
                        Console.WriteLine("Thank you for using the ATM. Goodbye!");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }

            }
        }
    }
}
