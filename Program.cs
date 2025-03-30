using System;
using System.Media;
using System.IO;

public class CyberGuardian
{
    public static void Main(string[] args)
    {
        Console.Clear();
        PlayVoiceGreeting("welcome.wav");
        DisplayBanner();
        PrintSeparator();
        GreetUser();
        PrintSeparator();
        StartChatLoop();
    }

    static void PlayVoiceGreeting(string audioFilePath)
    {
        if (File.Exists(audioFilePath))
        { 
            try
            {
                using (SoundPlayer player = new SoundPlayer(audioFilePath))
                {
                    player.PlaySync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing audio: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine($"Missing audio file: {audioFilePath}");
        }
    }

    static void PlayResponseSound()
    {
        try { Console.Beep(1000, 200); }
        catch { }
    }

    static void DisplayBanner()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(@"
  ____       _             _       _      
 / ___|  ___| |_ _   _  __| | __ _| |_ ___
 \___ \ / _ \ __| | | |/ _` |/ _` | __/ _ \
  ___) |  __/ |_| |_| | (_| | (_| | ||  __/
 |____/ \___|\__|\__,_|\__,_|\__,_|\__\___|
                                           ");
        Console.ResetColor();
    }

    static void GreetUser()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Welcome to CyberGuardian – Your Cybersecurity Assistant!");
        Console.ResetColor();

        Console.Write("Enter your name: ");
        string userName = Console.ReadLine();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Hello, {userName}! Let's keep you safe online.");
        Console.ResetColor();
    }

    static void DisplayQuestionMenu()
    {
        Console.WriteLine("\nSelect a cybersecurity topic:");
        Console.WriteLine("1. How are you?");
        Console.WriteLine("2. What's your purpose?");
        Console.WriteLine("3. Password safety tips");
        Console.WriteLine("4. Recognizing phishing attacks");
        Console.WriteLine("5. Protecting against malware");
        Console.WriteLine("6. Safe browsing practices");
        Console.WriteLine("7. Enhancing online privacy");
        Console.WriteLine("8. Social media security tips");
        Console.WriteLine("9. Exit");
        Console.Write("Enter your choice (1-9): ");
    }

    static void HandleUserQuery(string userInput)
    {
        switch (userInput.Trim())
        {
            case "1":
                PlayResponseSound();
                Console.WriteLine("I'm great! How can I assist you today?");
                break;
            case "2":
                PlayResponseSound();
                Console.WriteLine("I help you stay safe online with cybersecurity tips!");
                break;
            case "3":
                PlayResponseSound();
                Console.WriteLine("Use strong, unique passwords. A password manager can help!");
                break;
            case "4":
                PlayResponseSound();
                Console.WriteLine("Avoid clicking suspicious links and never share credentials.");
                break;
            case "5":
                PlayResponseSound();
                Console.WriteLine("Keep your system updated and avoid untrusted downloads.");
                break;
            case "6":
                PlayResponseSound();
                Console.WriteLine("Check for HTTPS, avoid unknown links, and use antivirus software.");
                break;
            case "7":
                PlayResponseSound();
                Console.WriteLine("Enable two-factor authentication and limit personal data sharing.");
                break;
            case "8":
                PlayResponseSound();
                Console.WriteLine("Use strong passwords and review your social media privacy settings.");
                break;
            case "9":
                PlayVoiceGreeting("goodbye.wav");
                Console.WriteLine("Goodbye! Stay cyber-safe.");
                Environment.Exit(0);
                break;
            default:
                Console.Beep(300, 500);
                Console.WriteLine("Invalid input. Please enter a number between 1 and 9.");
                break;
        }
    }

    static void StartChatLoop()
    {
        while (true)
        {
            DisplayQuestionMenu();
            string userInput = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(userInput))
            {
                HandleUserQuery(userInput);
            }
            else
            {
                Console.Beep(200, 300);
                Console.WriteLine("Invalid input. Try again.");
            }
        }
    }

    static void PrintSeparator()
    {
        Console.WriteLine(new string('-', 50));
    }
}