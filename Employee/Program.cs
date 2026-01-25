using LINQAssignment;
using System;

class Program
{
    static void Main()
    {
        Operations emp = new();

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("---------- Operation on Employee ------------- ");
            Console.WriteLine("1. To display total employees");
            Console.WriteLine("2. To Add new Employee");
            Console.WriteLine("3. To Update Employee Information");
            Console.WriteLine("4. To Delete Employee Record");
            Console.WriteLine("5. Show Compnay Name By Employee ID");
            Console.WriteLine("6. Show Employee By Company Name ");
            Console.WriteLine("7. Exit.");
            Console.ForegroundColor = ConsoleColor.White;

            //int choice = Convert.ToInt32(Console.ReadLine());
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    emp.ShowEmployees(); break;
                case "2":
                    emp.AddEmployee();
                    break;
                case "3":
                    emp.UpdateEmployeeInfo(); break;
                case "4":
                    emp.DeleteEmployee();
                    break;
                case "5":
                    emp.ShowCompany();
                    break;
                case "6":
                    emp.ShowEmployeeByCompany();
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("You entered wrong number ");
                    break;

            }
        }
    }
}