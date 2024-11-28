using System.Text.RegularExpressions;

namespace OperatorOverride
{
    partial class Program
    {
        private static bool IsLetter(string input)
        {
            return MyRegex().IsMatch(input);
        }
        
        static void Main(string[] args)
        {
            Console.Write("Enter your word, you won't see it in console, when finished hit enter: ");

            List<char> word = new List<char>();
            while (true)
            {
                try
                {   
                    
                    var key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine(); // Move to the next line after pressing Enter
                        break;
                    }

                    // Check if the key entered is a letter
                    if (char.IsLetter(key.KeyChar))
                    {
                        word.Add(key.KeyChar); // Append valid letter
                    }
                    else
                    {
                        // Throw exception for invalid character
                        throw new InvalidOperationException("Only letters are allowed.");
                    }
                }
                catch (Exception ex)
                {
                    Console.Beep(); // Optional feedback
                    Console.WriteLine($"\nError: {ex.Message}");
                }
            }
            
            int allowedMistakes = 6;
            List<char> guessedLetters = new List<char>();
            char[] displayWord = new char[word.Count];
            // Initialize displayWord with underscores
            for (int i = 0; i < word.Count; i++)
            {   
                Console.Write("_");
                displayWord[i] = '_';
            }
            
            Console.WriteLine("\n\r");

            

            
            
            while (allowedMistakes > 0)
            {
                char guess;

                try
                {   
                    Console.Write("Enter your letter: ");
                    guess = Console.ReadKey().KeyChar;

                    // Check if the input is a letter
                    if (!char.IsLetter(guess))
                    {
                        throw new Exception("Only letters are allowed.");
                    }

                    Console.WriteLine();

                    // If the guess has already been made
                    if (guessedLetters.Contains(guess))
                    {
                        Console.WriteLine("You've already guessed that letter.");  //nawilobrivi sitkva unda daubechdos
                        continue;
                    }
                    guessedLetters.Add(guess);

                    // Check if the guess is in the word
                    if (word.Contains(guess))
                    {
                        Console.WriteLine("Good guess!");
                        for (int i = 0; i < word.Count; i++)
                        {   
                            if (word[i] == guess)
                            {
                                displayWord[i] = guess; // Reveal the correct letters
                                
                            }
                        }
                        Console.WriteLine();
                        foreach (var letter in displayWord)
                        {
                            Console.Write(letter);
                        }
                        Console.WriteLine();
                        // Check if the word is fully guessed
                        if (!string.Join("", displayWord).Contains('_'))
                        {
                            Console.WriteLine("Congratulations! You've guessed the word: " + string.Join("", displayWord));
                            return;
                        }
                    }
                    else
                    {
                        allowedMistakes--;
                        Console.WriteLine($"Wrong guess! You have {allowedMistakes} mistake(s) left.");
                        if (allowedMistakes == 0)
                        {
                            Console.WriteLine("Game Over! You've used all your guesses.");
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError: {ex.Message}");
                }
            }
            
            
            
            
        }
        

        [GeneratedRegex("^[a-zA-Z]+$")]
        private static partial Regex MyRegex();
            
    }
}


// შევქმანთ ფიგურის(Shape) აბსტრაქტული კლასი, აბსტრაქტული მეთოდებით(CalculateArea) და ამ კლასის
// სამი შვილობილი კლასი, სამკუთხედი, ოთხკუთხედი, წრე.(გამოვიყენოთ მემკვიდრეობითობა და პოლიმორფიზმი)


