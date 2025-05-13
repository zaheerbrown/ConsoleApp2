using System;
using System.Media;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class CyberGuardian
{
    private static Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>()
    {
        {"password", new List<string> {
            "Make sure to use strong, unique passwords for each account. Avoid using personal details in your passwords.",
            "A strong password typically includes a mix of uppercase and lowercase letters, numbers, and symbols.",
            "Consider using a password manager to securely store and generate complex passwords."
        }},
        {"scam", new List<string> {
            "Be cautious of unsolicited emails or messages asking for personal information. Scammers often impersonate trusted organizations.",
            "Never share sensitive information like bank details or login credentials via email or unverified websites.",
            "If something seems too good to be true, it probably is a scam."
        }},
        {"privacy", new List<string> {
            "Review your privacy settings on social media and other online platforms to control who sees your information.",
            "Be mindful of the data you share online and consider using privacy-enhancing tools.",
            "Enable two-factor authentication to add an extra layer of security to your accounts."
        }},
        {"phishing tip", new List<string> {
            "Be cautious of emails with urgent requests or suspicious attachments.",
            "Verify the sender's identity before clicking on any links or providing information.",
            "Look for inconsistencies in email addresses, grammar, and spelling."
        }},
        {"how are you", new List<string> { "I'm great! How can I assist you today?", "I'm doing well, thank you for asking!" }},
        {"what's your purpose", new List<string> { "I help you stay safe online with cybersecurity tips!", "My purpose is to provide you with information and guidance on cybersecurity." }},
        {"malware", new List<string> { "Keep your system updated and avoid untrusted downloads.", "Install and regularly update your antivirus software to protect against malware." }},
        {"safe browsing", new List<string> { "Check for HTTPS, avoid unknown links, and use antivirus software.", "Be wary of websites that don't have a secure connection (HTTPS)." }},
        {"online privacy", new List<string> { "Enable two-factor authentication and limit personal data sharing.", "Understand the privacy policies of the online services you use." }},
        {"social media security", new List<string> { "Use strong passwords and review your social media privacy settings.", "Be careful about the personal information you share on social media." }},
        {"exit", new List<string> { "Goodbye! Stay cyber-safe." }}
    };

    private static Dictionary<string, string> userMemory = new Dictionary<string, string>();
    private static Random random = new Random();

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
  ____        _           _       _
 / ___| ___| |_ _   _  __| | __ _| |_ ___
 \___ \/ _ \ __| | | |/ _` |/ _` | __/ _ \
  ___) |  __/ |_| |_| | (_| | (_| | ||  __/
 |____/ \___|\__|\__,_|\__,_|\__,_|\__\___|
                                           ");
        Console.ResetColor();
    }

    static string userName;
    static string favoriteCyberTopic;

    static void GreetUser()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Welcome to CyberGuardian – Your Cybersecurity Assistant!");
        Console.ResetColor();

        Console.Write("Enter your name: ");
        userName = Console.ReadLine();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Hello, {userName}! Let's keep you safe online.");
        Console.ResetColor();

        Console.Write("What is a cybersecurity topic you are particularly interested in? ");
        favoriteCyberTopic = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(favoriteCyberTopic))
        {
            userMemory["favoriteCyberTopic"] = favoriteCyberTopic.ToLower();
            Console.WriteLine($"Great! I'll remember that you're interested in {favoriteCyberTopic}.");
        }
    }

    static void HandleUserQuery(string userInput)
    {
        string lowerInput = userInput.ToLower();
        bool responseFound = false;

        foreach (var keyword in keywordResponses.Keys)
        {
            if (lowerInput.Contains(keyword))
            {
                PlayResponseSound();
                Console.ForegroundColor = ConsoleColor.Green; // Set color for chatbot response
                string response = GetRandomResponse(keyword);
                Console.WriteLine(response);
                Console.ResetColor(); // Reset to default console color

                if (keyword == "exit")
                {
                    PlayVoiceGreeting("goodbye.wav");
                    Environment.Exit(0);
                }

                responseFound = true;
                break; // Added break to stop after finding the first relevant keyword
            }
        }

        if (!responseFound)
        {
            // Sentiment Detection (very basic example)
            if (lowerInput.Contains("worried") || lowerInput.Contains("concerned") || lowerInput.Contains("anxious"))
            {
                Console.Beep(1000, 200);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("It's completely understandable to feel that way. Cybersecurity can seem overwhelming, but I'm here to help you understand and stay safe.");
                Console.ResetColor();
                responseFound = true;
            }
            else if (lowerInput.Contains("curious") || lowerInput.Contains("tell me more"))
            {
                Console.Beep(1000, 200);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("That's great that you're curious! What specific area of cybersecurity are you interested in?");
                Console.ResetColor();
                responseFound = true;
            }
            else if (lowerInput.Contains("frustrated") || lowerInput.Contains("confused"))
            {
                Console.Beep(1000, 200);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("I understand this can be frustrating. Please tell me what's confusing you, and I'll try my best to explain it clearly.");
                Console.ResetColor();
                responseFound = true;
            }
            else if (lowerInput.Contains("thank you"))
            {
                Console.Beep(1000, 200);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You're welcome! Is there anything else I can help you with today?");
                Console.ResetColor();
                responseFound = true;
            }
        }

        if (!responseFound)
        {
            // Check for memory recall
            if (lowerInput.Contains("you remember") && userMemory.ContainsKey("favoriteCyberTopic"))
            {
                Console.Beep(1000, 200);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Yes, I remember you are interested in {userMemory["favoriteCyberTopic"]}. Would you like some more information on that?");
                Console.ResetColor();
                responseFound = true;
            }
        }

        if (!responseFound)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Beep(300, 500);
            Console.WriteLine("I'm not sure I understand. Can you try rephrasing your question or ask about password, scam, or privacy?");
            Console.ResetColor();
        }
    }

    static string GetRandomResponse(string keyword)
    {
        if (keywordResponses.ContainsKey(keyword))
        {
            int index = random.Next(keywordResponses[keyword].Count);
            return keywordResponses[keyword][index];
        }
        return "Sorry, I don't have a specific response for that right now.";
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
