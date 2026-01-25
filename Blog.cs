using System;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

class Blog
{
    static string fileName = "";
    private static readonly Regex TitleRegex = new Regex(@"^.{2,20}$");
    private static readonly Regex emailRegex = new Regex(
        @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$",
        RegexOptions.IgnoreCase | RegexOptions.Compiled
    );

    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;
        return emailRegex.IsMatch(email);
    }
    public static bool isValidTile(string title)
    {
        if (string.IsNullOrWhiteSpace(title)) return false;

        return TitleRegex.IsMatch(title);
    }

    public static bool isValidDescription(string des)
    {
        if (string.IsNullOrWhiteSpace(des)) return false;
        if (des.Length < 3) return false;
        return true;
    }

    public static bool IsValidEailBody(string emailBody)
    {
        if (string.IsNullOrWhiteSpace(emailBody)) return false;
        return true;
    }

    public static void SendMail()
    {
        bool flag = true;
        string userEmail = "";
        while (flag)
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Enter Your email Address : ");
            userEmail = Console.ReadLine();
            if (IsValidEmail(userEmail))
            {
                flag = false;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Enter a valid email address");
            }
        }
        string myEmail = "rahulp.revoceptsolutions@gmail.com";
        MailAddress to = new MailAddress(userEmail);
        MailAddress from = new MailAddress(myEmail);

        MailMessage email = new MailMessage(from, to);
        Console.Write("Enter the Subject of email : ");
        string emailSubject = Console.ReadLine();
        email.Subject = emailSubject;

        flag = true;
        string emailBody = "";
        while (flag)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Enter the body of email");
            emailBody = Console.ReadLine();

            if (IsValidEailBody(emailBody))
            {
                flag = false;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Email Body should not null");
            }
        }
        email.Body = emailBody;
        Attachment attachment = new Attachment($"{fileName}");

        
        email.Attachments.Add(attachment);

        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com";
        smtp.Port = 25;
        smtp.Credentials = new NetworkCredential(myEmail, "ocdl jwyg eldb psec");
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.EnableSsl = true;

        try
        {
            smtp.Send(email);
            Console.WriteLine("Mail Sent.");
        }
        catch (SmtpException ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    public static void createFile(string title, string description)
    {
        //DateOnly currDate = new DateOnly();
        string currDate = DateTime.Now.ToString("dd-mm-yy");
        Random rand = new Random();
        int randomNumber = rand.Next(10, 100);

        FileStream filestrm;
        fileName = $"../../../{currDate}{randomNumber}.txt";
        try
        {
            filestrm = new FileStream(fileName, FileMode.CreateNew);
        }
        catch (IOException exc)
        {
            Console.WriteLine("File Creation Error " + exc.Message);
            return;
        }

        using (StreamWriter strwriter = new StreamWriter(filestrm))
        {
            strwriter.Write("Title : " + title + "\r\n");
            strwriter.Write("Description : " + description + "\r\n");
        };
    }

    public static void createBlog()
    {        
        string title = "";
        string description = "";
        // Title of the blog
        bool flag = true;
        while (flag)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Enter the title of Blog : ");

            title = Console.ReadLine();
            if (isValidTile(title))
            {
                //Console.WriteLine(title);
                flag = false;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Title should not null or min length 2 and max length 20");
            }

        }
        // description of the blog
        flag = true;
        while (flag)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nEnter the description of the Blog : ");
            description = Console.ReadLine();

            if (isValidDescription(description))
            {
                //Console.WriteLine(description);
                flag = false;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Description should be not null and more than 2 length");
            }
        }

        Console.WriteLine("1. Save Blog");
        Console.WriteLine("2. Cancel Blog");
        string wantToSave = Console.ReadLine();
        switch (wantToSave)
        {
            case "1":
                //strwriter.Write(DateAndUserId + "\r\n");
                createFile(title, description);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Saved Successfully");
                break;
            case "2":
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Canceled Blog");
                flag = true;
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You entered wrong number");
                break;
        }
        if (flag) return;

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Do you want to share file on email? ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("1. Send Email");
        Console.WriteLine("2. Exit.");
        string wantToSendEmail = Console.ReadLine();
        
        switch (wantToSendEmail)
        {
            case "1":
                SendMail();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Send File on your email Successfully!");
                break;
            case "2":
                return;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You entered wrong number");
                break;
        }
    }
    static void Main(string[] args)
    {
        Console.WriteLine("Blog Writting System.");

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1. Do you want to create new Blog");
            Console.WriteLine("2. Exit.");
            string newBlog = Console.ReadLine();

            switch (newBlog)
            {
                case "1":
                    createBlog();
                    break;
                case "2":
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You entered wrong number");
                    break;
                    
            }
        }
        
    }
}