using System;
using System.Media;
using System.IO;
using System.Collections.Generic;

public class CyberGuardian
{
    public static void Main(string[] args)
    {
        Console.Clear();
        PlayVoiceGreeting("C:\\Users\\RC_Student_lab\\source\\repos\\progpt1\\progaudio.wav");
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

    static void HandleUserQuery(string userInput)
    {
        // List of keywords and corresponding responses
        var responses = new List<(string keyword, string response)>
        {
            ("how are you", "I'm great! How can I assist you today?"),
            ("what's your purpose", "I help you stay safe online with cybersecurity tips!"),
            ("password safety", "Use strong, unique passwords. A password manager can help!"),
            ("phishing", "Avoid clicking suspicious links and never share credentials."),
            ("malware", "Keep your system updated and avoid untrusted downloads."),
            ("safe browsing", "Check for HTTPS, avoid unknown links, and use antivirus software."),
            ("online privacy", "Enable two-factor authentication and limit personal data sharing."),
            ("social media security", "Use strong passwords and review your social media privacy settings."),
            ("exit", "Goodbye! Stay cyber-safe.")
        };

        bool responseFound = false;

        // Loop through each keyword/response pair and check if the input contains the keyword
        foreach (var (keyword, response) in responses)
        {
            if (userInput.ToLower().Contains(keyword))
            {
                PlayResponseSound();
                Console.WriteLine(response);

                // Special case for exit to quit the program
                if (keyword == "exit")
                {
                    PlayVoiceGreeting("goodbye.wav");
                    Environment.Exit(0);
                }

                responseFound = true;
                break;
            }
        }

        if (!responseFound)
        {
            Console.Beep(300, 500);
            Console.WriteLine("Invalid input. Please enter a valid query.");
        }
    }

    static void StartChatLoop()
    {
        while (true)
        {
            Console.Write("\nAsk me something about cybersecurity (or type 'exit' to quit): ");
            string userInput = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(userInput))
            {
                HandleUserQuery(userInput.Trim());
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
