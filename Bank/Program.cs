using CRUD;
using System;
class Program
{
    static void Main(string[] args)
    {
        AllOperations operations = new AllOperations();
        //operations.DummyData();


        bool flag = true;
        while(flag)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n\t\t\t------------------------------------- Transaction Menu -------------------------------");
            Console.WriteLine("\t\t\t|                                                                                    |");
            Console.WriteLine("\t\t\t|                                                                                    |");
            Console.WriteLine("\t\t\t|                                                                                    |");
            Console.WriteLine("\t\t\t|                                1.Add New Transaction                               |");
            Console.WriteLine("\t\t\t|                                2.View Transaction                                  |");
            Console.WriteLine("\t\t\t|                                3.Update Transaction                                |");
            Console.WriteLine("\t\t\t|                                4.Delete Transaction                                |");
            Console.WriteLine("\t\t\t|                                5.Want to Send File on Email                        |");
            Console.WriteLine("\t\t\t|                                6.Exit                                              |");
            Console.WriteLine("\t\t\t|                                                                                    |");
            Console.WriteLine("\t\t\t|                                                                                    |");
            Console.WriteLine("\t\t\t--------------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nChoose Transaction Menu : ");
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":                    
                    operations.CreateTransaction();
                    break;
                case "2":
                    operations.ViewTransaction();
                    break;
                case "3":
                    operations.UpdateTransaction();
                    break;
                case "4":
                    operations.DeleteTransaction();
                    break;
                case "5":
                    operations.SendMailThread();
                    break;
                case "6":
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Choose a Valid Transaction From Menu");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;

            }          
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}