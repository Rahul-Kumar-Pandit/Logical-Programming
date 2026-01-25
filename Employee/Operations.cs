using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LINQAssignment
{

   

    internal class Operations
    {
        List<Employee> employees = new List<Employee>();

        // validation
        private static readonly Regex NameRegex = new Regex(@"^[\p{L}\s]+$");

        private static readonly Regex CompanyAndDepartment = new Regex(@"^[A - Za - z\D]+${2,}");
        public static bool IsValidName(string name)
        {   
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            // Check if the string matches the regex pattern
            return NameRegex.IsMatch(name);
        }

        // validate department name
        public static bool IsValidCompanyAndDepartment(string dname)
        {
            if (string.IsNullOrWhiteSpace(dname)) return false;

            return CompanyAndDepartment.IsMatch(dname);
        }

        public static bool IsValidTechnology(string technology)
        {

            if (string.IsNullOrWhiteSpace(technology)) return false;

            return true;
        }



        // Show table
        public void ShowEmployees()
        {
            if(employees.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Employees list is Empty");
                return;
            }
            var employee = from emp in employees select emp;
            foreach(var e in employee)
            {
                Console.WriteLine($"ID : {e.ID} , Name : {e.Name} , Departement : {e.Department} , Technology : {e.Technology} , Company Name : {e.CompanyName}");
            }
        }

        // Add Employee
        public void AddEmployee()
        {
            var employee = new Employee();  
            bool flag = true;
            while (flag)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Enter Employee id : ");
                    employee.ID = Convert.ToInt32(Console.ReadLine());
                    flag = false;
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Employee Id should be integer.");
                }
            }
            flag = true;  

            while (flag)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\nEnter Employee Name : ");
                employee.Name = Console.ReadLine();
                if (!IsValidName(employee.Name))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Enter a valid Employee Name");
                }
                else
                {
                    flag = false;
                }
            }

            flag = true;

            while (flag)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\nEnter Department of Employee : ");
                employee.Department = Console.ReadLine();
                if (!IsValidCompanyAndDepartment(employee.Department))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Enter a Valid Department Name");
                }
                else
                {
                    flag = false;
                }
            }

            flag = true;
            while (flag)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\nEnter Technology of Employee : ");
                employee.Technology = Console.ReadLine();

                if (!IsValidTechnology(employee.Technology))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Enter a valid Technology");
                }
                else
                {
                    flag = false;
                }
            }

           
            flag = true;
            while (flag)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\nEnter Company Name where Employee Work : ");
                employee.CompanyName = Console.ReadLine();
                if (!IsValidName(employee.CompanyName))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Enter a valid Company Name");
                }
                else
                {
                    flag = false;
                }
            }

            employees.Add(employee);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Employee Added Successfully!");
        }

        // Update Employee Information
        public void UpdateEmployeeInfo()
        {
            if(employees.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Employee list is Empty");
                return;
            }

            int id=0;
            bool isValidInput = false;

            while (!isValidInput)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Enter Employee id which you want to update : ");
                string userInput = Console.ReadLine();

                if (!int.TryParse(userInput, out id))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You Entered Wrong Employee ID");    
                }
                else
                {
                    isValidInput = true;
                    id = Convert.ToInt32(userInput);
                }
            }          

            Employee employee = employees.Find(e => e.ID == id);

            if( employee != null)
            {
                bool isTrue = true;
                while (isTrue)
                {
                    bool flag = true;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Update Employee Information");
                    Console.WriteLine("1. Update Employee Name");
                    Console.WriteLine("2. Update Employee Department");
                    Console.WriteLine("3. Update Employee Technology");
                    Console.WriteLine("4. Update Employee Company Name");
                    Console.WriteLine("5. Exit.");
                    Console.ForegroundColor = ConsoleColor.White;

                    string UpdateInfo = Console.ReadLine();

                    switch (UpdateInfo)
                    {
                        case "1":
                            flag = true;
                            while (flag)
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("\nEnter New Name of Employee : ");
                                employee.Name = Console.ReadLine();
                                if (!IsValidName(employee.Name))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Enter a valid Employee Name");
                                }
                                else
                                {
                                    flag = false;
                                }
                            }
                            break;

                        case "2":
                            flag = true;
                            while (flag)
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("\nEnter New Department of Employee : ");
                                employee.Department = Console.ReadLine();
                                if (!IsValidCompanyAndDepartment(employee.Department))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Enter a Valid Department Name");
                                }
                                else
                                {
                                    flag = false;
                                }
                            }
                            break;
                        case "3":
                            flag = true;
                            while (flag)
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("\nEnter New Technology of Employee : ");
                                employee.Technology = Console.ReadLine();

                                if (!IsValidTechnology(employee.Technology))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Enter a valid Technology");
                                }
                                else
                                {
                                    flag = false;
                                }
                            }
                            break;
                        case "4":
                            flag = true;
                            while (flag)
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("\nEnter New Company Name of Employee : ");
                                employee.CompanyName = Console.ReadLine();
                                if (!IsValidName(employee.CompanyName))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Enter a valid Company Name");
                                }
                                else
                                {
                                    flag = false;
                                }
                            }
                            break;
                        case "5":
                            isTrue = false;
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("You entered wrong number");
                            break;

                    }
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Employee Record Updated Successfully!");
            }
            else
            {
                Console.WriteLine("Employee Not found!");
            }
        }

        //Delete Employee Record
        public void DeleteEmployee()
        {
            if(employees.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Employee list is Empty");
                return;
            }
            
            int id = 0;
            bool isValidInput = false;

            while (!isValidInput)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Enter Employee id which you want to delete : ");
                string userInput = Console.ReadLine();

                if (!int.TryParse(userInput, out id))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You Entered Wrong Employee ID");
                }
                else
                {
                    isValidInput = true;
                    id = Convert.ToInt32(userInput);
                }
            }

            Employee employee = employees.Find(e => e.ID == id);

            if( employee != null )
            {
                employees.Remove(employee);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Employee Deleted Successfully!");
            }
            else
            {
                Console.WriteLine("Employee Not found!");
            }
        }

        // Show Company 
        public void ShowCompany()
        {
            if(employees.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Employee list is Empty");
                return;
            }
            

            int id = 0;
            bool isValidInput = false;
            while (!isValidInput)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Enter Employee id : ");
                string userInput = Console.ReadLine();

                if (!int.TryParse(userInput, out id))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You Entered Wrong Employee ID");
                }
                else
                {
                    isValidInput = true;
                    id = Convert.ToInt32(userInput);
                }
            }

            Employee employee = employees.Find(e => e.ID == id);

            if (employee != null)
            {
                Console.WriteLine("Company Name : "+ employee.CompanyName);
            }
            else
            {
                Console.WriteLine("Employee Not found!");
            }
        }

        public void ShowEmployeeByCompany()
        {
            if (employees.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Employee list is Empty");
                return;
            }
            Console.WriteLine("Enter company name (e.g., Apple, Google)");
            string company = Console.ReadLine();
            var totalEmployee = from emp in employees where emp.CompanyName.Equals(company,StringComparison.OrdinalIgnoreCase) select emp;

            foreach(var e in totalEmployee)
            {
                Console.WriteLine($"Employee Name at {company} is : "+e.Name + " ");
            }
            Console.WriteLine();
        }
    }
}
