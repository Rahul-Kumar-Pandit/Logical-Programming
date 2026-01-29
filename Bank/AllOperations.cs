using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace CRUD
{
    public class AllOperations
    {   
        public List<FinanceRecord> transactionLists = new List<FinanceRecord>
        {
            new FinanceRecord(Guid.NewGuid(), 7584, TransactionType.Expense, CategoryType.Entertainment, "we enjoyed at swing pool", DateTime.Now, "Bank Transfe", "rahulpandit@gmail.com")
            
        };
        
        public static void ErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void SuccessMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
       
        public static bool IsValidEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        public void CreateTransaction()
        {
            FinanceRecord records = new FinanceRecord();
            records.Id = Guid.NewGuid();
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("\nTransaction Type\n");
                Console.WriteLine("1. Income");
                Console.WriteLine("2. Expense\n");
                Console.Write("Choose Your Choice : ");
                string addChooseUser = Console.ReadLine();
                switch (addChooseUser)
                {
                    case "1":
                        records.TransactionType = TransactionType.Income;
                        flag = false;
                        break;
                    case "2":
                        records.TransactionType = TransactionType.Expense;
                        flag = false;
                        break;
                    default:
                        ErrorMessage("Choose a Valid Transaction Type");
                        break;
                }
            }

            flag = true;
            while (flag)
            {
                Console.Write("\nEnter Transaction Amount : ");
                string input = Console.ReadLine();

                if (Decimal.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal value))
                {
                    if (value < 0)
                    {
                        ErrorMessage("Amount Should be Positive");
                    }
                    else
                    {
                        records.Amount = value;
                        flag = false;
                    }
                }
                else
                {
                    ErrorMessage("Enter a valid Amount");
                }
            }

            flag = true;
            while (flag)
            {
                Console.WriteLine("\nTransaction Category\n");
                Console.WriteLine("1. Food");
                Console.WriteLine("2. Utilities");
                Console.WriteLine("3. Rent");
                Console.WriteLine("4. Entertainment");
                Console.WriteLine("5. Savings");
                Console.WriteLine("6. Others\n");
                Console.Write("Choose Your Choice : ");
                string categoryInput = Console.ReadLine();

                switch (categoryInput)
                {
                    case "1":
                        records.Category = CategoryType.Food;
                        flag = false;
                        break;
                    case "2":
                        records.Category = CategoryType.Utilities;
                        flag = false;
                        break;
                    case "3":
                        records.Category = CategoryType.Rent;
                        flag = false;
                        break;
                    case "4":
                        records.Category = CategoryType.Entertainment;
                        flag = false;
                        break;
                    case "5":
                        records.Category = CategoryType.Savings;
                        flag = false;
                        break;
                    case "6":
                        records.Category = CategoryType.Others;
                        flag = false;
                        break;
                    default:
                        ErrorMessage("Choose a Valid Transaction Category");
                        break;
                }
            }

            flag = true;
            while (flag)
            {
                Console.Write("\nEnter the Description of Transaction : ");
                string descInput = Console.ReadLine();

                if(string.IsNullOrWhiteSpace(descInput))
                {
                    ErrorMessage("Description should not null");
                }
                else if(descInput.Length < 3)
                {
                    ErrorMessage("Description should be minimum of 3 length");
                }
                else
                {
                    records.Description = descInput;
                    flag = false;
                }
            }


            records.TransactionDate = DateTime.Now;

            flag = true;
            while (flag)
            {
                Console.WriteLine("\nPayment Method for Transaction\n");
                Console.WriteLine("1. Cash");
                Console.WriteLine("2. Credit Card");
                Console.WriteLine("3. Bank Transfer\n");
                Console.Write("Choose Your Choice : ");
                string paymentInput = Console.ReadLine();
                switch (paymentInput)
                {
                    case "1":
                        records.PaymentMethod = "Cash";
                        flag = false; break;
                    case "2":
                        records.PaymentMethod = "Credit Card";
                        flag = false; break;
                    case "3":
                        records.PaymentMethod = "Bank Transfer";
                        flag = false; break;
                    default:
                        ErrorMessage("Choose a valid Payment Method for Transaction");
                        break;
                }
            }

            flag = true;
            while (flag)
            {
                Console.Write("\nEnter Your Email : ");
                string emailInput = Console.ReadLine();
                if (IsValidEmail(emailInput))
                {
                    records.Email = emailInput;
                    flag = false;
                }
                else
                {
                    ErrorMessage("Enter a Valid Email");
                }
            }

            transactionLists.Add(records);
            SuccessMessage("Transaction Added Successfully!");
            ThreadMethod("ADD",records);     
        }

        public void ViewTransaction()
        {
            if(transactionLists.Count == 0)
            {
                ErrorMessage("Transaction List is Empty");
                return;
            }

            bool flag = true;
            while (flag)
            {
                Console.WriteLine("\nWhich Transaction You want to See\n");
                Console.WriteLine("1. View All Transactions");
                Console.WriteLine("2. View Transaction Based on Date Range");
                Console.WriteLine("3. View Transaction By Category");
                Console.WriteLine("4. View Transaction By Payment Method");
                Console.WriteLine("5. Back to Main Menu\n");
                Console.Write("Choose Your choice : ");
                string viewInput = Console.ReadLine();
                switch (viewInput)
                {
                    case "1":
                        ViewAllTransaction();
                        break;
                    case "2":
                        ViewDateRange();
                        break;
                    case "3":
                        ViewByCategory();
                        break;
                    case "4":
                        ViewByPaymentMethod();
                        break;
                    case "5":
                        flag = false;
                        break;
                    default:
                        ErrorMessage("Choose a valid choice");
                        break;

                }
            }      
            
        }

        public void ViewAllTransaction()
        {
            DisplayTransaction(transactionLists);
        }

        public void ViewDateRange()
        {
            Console.Write("\nEnter Starting date : ('DD-MM-YYYY') : ");
            string stDate = Console.ReadLine();
            DateTime startDate = DateTime.Parse(stDate);

            Console.Write("\nEnter Ending date : ('DD-MM-YYYY') : ");
            string endDate = Console.ReadLine();
            DateTime endDateExclusive = DateTime.Parse(endDate);

            var filteredTransaction = (from record in transactionLists
                               where record.TransactionDate >= startDate && record.TransactionDate <= endDateExclusive
                               select record).ToList();

            if(filteredTransaction.Count == 0)
            {
                ErrorMessage("In this range there is no transaction");
                return;
            }
            DisplayTransaction(filteredTransaction);
        }

        public void ViewByCategory()
        {
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("\nView Transation Based On Which Category\n");
                Console.WriteLine("1. Food");
                Console.WriteLine("2. Utilities");
                Console.WriteLine("3. Rent");
                Console.WriteLine("4. Entertainment");
                Console.WriteLine("5. Savings");
                Console.WriteLine("6. Others");
                Console.WriteLine("7. Exit\n");
                Console.Write("Choose Your Choice : ");
                string categoryInput = Console.ReadLine();
                switch (categoryInput)
                {
                    case "1":
                        ShowCategoryTransaction(CategoryType.Food);
                        break;
                    case "2":
                        ShowCategoryTransaction(CategoryType.Utilities);
                        break;
                    case "3":
                        ShowCategoryTransaction(CategoryType.Rent);
                        break;
                    case "4":
                        ShowCategoryTransaction(CategoryType.Entertainment);
                        break;
                    case "5":
                        ShowCategoryTransaction(CategoryType.Savings);
                        break;
                    case "6":
                        ShowCategoryTransaction(CategoryType.Others);
                        break;
                    case "7":
                        flag = false;
                        break;
                    default:
                        ErrorMessage("Choose a Valid Choice");
                        break;
                }
            }
        }

        public void ShowCategoryTransaction(CategoryType category)
        {

            var filterCategory = transactionLists.Where(ct => ct.Category == category).ToList();
            if (filterCategory.Count == 0)
            {
                ErrorMessage($"{category} Category is not present");
                return;
            }
            DisplayTransaction(filterCategory);
        }

        public void ViewByPaymentMethod()
        {
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("\nWhich Transaction You want to see\n");
                Console.WriteLine("1. Cash");
                Console.WriteLine("2. Credit Card");
                Console.WriteLine("3. Bank Transfer");
                Console.WriteLine("4. Exit\n");
                Console.Write("Choose Your Choice : ");
                string paymentMethodChoice = Console.ReadLine();
                switch (paymentMethodChoice)
                {
                    case "1":
                        ShowPaymentMethodTransaction("Cash");
                        break;
                    case "2":
                        ShowPaymentMethodTransaction("Credit Card");
                        break;
                    case "3":
                        ShowPaymentMethodTransaction("Bank Transfer");
                        break;
                    case "4":
                        flag = false;
                        break;
                    default:
                        ErrorMessage("Choose a Valid Choice");
                        break;
                }
            }
        }

        public void ShowPaymentMethodTransaction(string paymentMethod)
        {
            var filteredPaymentMethod = (from transaction in transactionLists
                                                    where transaction.PaymentMethod == paymentMethod
                                                    select transaction).ToList();

            if(filteredPaymentMethod.Count == 0)
            {
                ErrorMessage($"There is no transaction in {paymentMethod} Payment Method");
                return;
            }

            DisplayTransaction(filteredPaymentMethod);
           
        }


        public static void DisplayTransaction(List<FinanceRecord> transactions) {

            /*
                AS A TABLE 

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.WriteLine($"{"ID",-10} | {"Amount",-10} | {"Transaction Type", -10} | {"Category Type", -10} | {"Description", -7} | {"Transaction Data", -10} | {"Payment Method", -12} | {"Email", -1.0}\n");   
            
            foreach (var transaction in transactions)
            {
                Console.WriteLine($"{transaction.Id, -30} | {transaction.Amount, -12} | {transaction.TransactionType , -10} | {transaction.Category, -10} | {transaction.Description, -30} | {transaction.TransactionDate , -20} | {transaction.PaymentMethod, -12} | {transaction.Email,-30}\n");
                
            }
            Console.ForegroundColor = ConsoleColor.White;

            */

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            
            foreach (var transaction in transactions)
            {
                Console.WriteLine($"ID : {transaction.Id} , Amount : {transaction.Amount} , Transaction Type : {transaction.TransactionType}");
                Console.WriteLine($"Category Type : {transaction.Category} , Description : {transaction.Description} , Transaction Date : {transaction.TransactionDate}");
                Console.WriteLine($"Payment Method : {transaction.PaymentMethod} , Email : {transaction.Email}\n\n");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void UpdateTransaction()
        {
            if( transactionLists.Count == 0)
            {
                ErrorMessage("Transaction List is Empty");
                return;
            }
            
            bool flag = true;
            string userInputId;
            Guid validGuid = Guid.Empty;
            while (flag)
            {
                Console.Write("\nEnter Transaction Id : ");
                userInputId = Console.ReadLine();
                
                if (Guid.TryParse(userInputId, out validGuid))
                {
                    validGuid = Guid.Parse(userInputId);
                    flag = false;
                }
                else
                {
                    ErrorMessage("Enter a Valid Transation Id");
                }
            }
            var record = transactionLists.Find(r => r.Id ==  validGuid);

            if (record != null)
            {
                flag = true;
                while (flag)
                {
                    Console.WriteLine("\nDo You want to Update Amount?\n");
                    Console.WriteLine("1. Yes");
                    Console.WriteLine("2. Skip\n");
                    Console.Write("Choose Your Choice : ");
                    string amountUpdateChoice = Console.ReadLine();
                    switch (amountUpdateChoice)
                    {
                        case "1":
                            Console.Write("\nEnter New Transaction Amount : ");
                            string input = Console.ReadLine();

                            if (Decimal.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal value))
                            {
                                if (value < 0)
                                {
                                    ErrorMessage("Amount Should be Positive");
                                }
                                else
                                {
                                    record.Amount = value;
                                    SuccessMessage("Amount Updated Successfully!");
                                    flag = false;
                                }
                            }
                            else
                            {
                                ErrorMessage("Enter a valid Amount");
                            }
                            break;
                        case "2":
                            flag = false;
                            break;
                        default:
                            ErrorMessage("Choose a Valid Choice");
                            break;
                    }

                }

                flag = true;
                while (flag)
                {
                    Console.WriteLine("\nDo You want to Update Description?\n");
                    Console.WriteLine("1. Yes");
                    Console.WriteLine("2. Skip\n");
                    Console.Write("Choose Your Choice : ");
                    string descUpdateInput = Console.ReadLine();
                    switch (descUpdateInput)
                    {
                        case "1":
                            Console.Write("\nEnter the New Description of Transaction : ");
                            string descInput = Console.ReadLine();

                            if (string.IsNullOrWhiteSpace(descInput))
                            {
                                ErrorMessage("Description should not null");
                            }
                            else if (descInput.Length < 3)
                            {
                                ErrorMessage("Description should be minimum of 3 length");
                            }
                            else
                            {
                                record.Description = descInput;
                                SuccessMessage("Description Updated Successfully!");
                                flag = false;
                            }
                            break;
                        case "2":
                            flag = false; break;
                        default:
                            ErrorMessage("Choose a Valid Choice");
                            break;
                    }
                }

                flag = true;
                while (flag)
                {
                    Console.WriteLine("\nDo You Want to Update Category\n");
                    Console.WriteLine("1. Yes");
                    Console.WriteLine("2. Skip\n");
                    Console.Write("Choose Your Choice : ");
                    string categoryUpdateInput = Console.ReadLine();

                    switch (categoryUpdateInput)
                    {
                        case "1":
                            Console.WriteLine("\nNew Transaction Category\n");
                            Console.WriteLine("1. Food");
                            Console.WriteLine("2. Utilities");
                            Console.WriteLine("3. Rent");
                            Console.WriteLine("4. Entertainment");
                            Console.WriteLine("5. Savings");
                            Console.WriteLine("6. Others\n");
                            Console.Write("Choose Your Choice : ");
                            string categoryInput = Console.ReadLine();

                            switch (categoryInput)
                            {
                                case "1":
                                    record.Category = CategoryType.Food;
                                    SuccessMessage("Category Updated Successfully!");
                                    flag = false;
                                    break;
                                case "2":
                                    record.Category = CategoryType.Utilities;
                                    SuccessMessage("Category Updated Successfully!");
                                    flag = false;
                                    break;
                                case "3":
                                    record.Category = CategoryType.Rent;
                                    SuccessMessage("Category Updated Successfully!");
                                    flag = false;
                                    break;
                                case "4":
                                    record.Category = CategoryType.Entertainment;
                                    SuccessMessage("Category Updated Successfully!");
                                    flag = false;
                                    break;
                                case "5":
                                    record.Category = CategoryType.Savings;
                                    SuccessMessage("Category Updated Successfully!");
                                    flag = false;
                                    break;
                                case "6":
                                    record.Category = CategoryType.Others;
                                    SuccessMessage("Category Updated Successfully!");
                                    flag = false;
                                    break;
                                default:
                                    ErrorMessage("Choose a Valid Transaction Category");
                                    break;
                            }
                            break;

                        case "2":
                            flag = false;
                            break;
                        default:
                            ErrorMessage("Choose a Valid Choice");
                            break;
                    }
                }
                //CreateTextFile("UPDATE", record);
                ThreadMethod("UPDATE", record);
            }
            else
            {
                ErrorMessage("Transaction Id not found");
            }
            
        }
    
        public void DeleteTransaction()
        {
            if (transactionLists.Count == 0)
            {
                ErrorMessage("Transaction List is Empty");
                return;
            }

            bool flag = true;
            string userInputId;
            Guid validGuid = Guid.Empty;
            while (flag)
            {
                Console.Write("\nEnter Transaction Id : ");
                userInputId = Console.ReadLine();

                if (Guid.TryParse(userInputId, out validGuid))
                {
                    validGuid = Guid.Parse(userInputId);
                    flag = false;
                }
                else
                {
                    ErrorMessage("Enter a Valid Transation Id");
                }
            }
            var record = transactionLists.Find(r => r.Id == validGuid);
            
            if(record != null)
            {
                //CreateTextFile("DELETE", record);
                ThreadMethod("DELETE", record);
                transactionLists.Remove(record);
                SuccessMessage("Transaction Deleted Successfully!");
            }
            else
            {
                ErrorMessage("Transaction Id not found!");
            }
        }

        public static void CreateTextFile(Object obj)
        {
            Array argArray = new object[2];
            argArray = (Array)obj;

            string operation = (string)argArray.GetValue(0);
            FinanceRecord record = (FinanceRecord)argArray.GetValue(1);
            Thread.Sleep(1000 * 15);
            string filePath = @"../../../Transaction.txt";
            if (record != null)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(filePath, true))
                    {
                        writer.WriteLine($"{operation} \tDATA : [ ID : {record.Id} , Amount : {record.Amount} , Transaction Type : {record.TransactionType} , Category : {record.Category} , Description : {record.Description} , Transaction Date : {record.TransactionDate} , Payment Method : {record.PaymentMethod} ] , Operation Time : {DateTime.Now}\n");
                    }
                    SuccessMessage("\n--------------------- Transaction stored to a text file successfully! ------------------------------\n");
                }
                catch (IOException e)
                {
                    Console.WriteLine($"An error occurred: {e.Message}");
                }
            }
            else
            {
                ErrorMessage("Transaction does not found");
            }   
        }
        
        
        public static void MailMethod(Object obj)
        {
            
            Array arr = new object[1];
            arr = (Array)obj;
            string userEmail = (string)arr.GetValue(0);

            MailAddress to = new MailAddress(userEmail);
            MailAddress from = new MailAddress("rahulp.revoceptsolutions@gmail.com");

            MailMessage email = new MailMessage(from, to);
            Attachment attachment = new Attachment(@"../../../Transaction.txt");
            email.Attachments.Add(attachment);


            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 25;
            smtp.Credentials = new NetworkCredential("rahulp.revoceptsolutions@gmail.com", "snbx hqzh vjzj aegs");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;

            try
            {
                Thread.Sleep(1000 * 60);
                smtp.Send(email);
                SuccessMessage("Mail Sent Successfully!");
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }     
       
        public static void ThreadMethod(string operation , FinanceRecord records)
        {
            object args = new object[2] { operation, records };
            Thread b1 = new Thread(new ParameterizedThreadStart(CreateTextFile));
            b1.Start(args);
        }
    

        public void SendMailThread()
        {
            bool flag = true;
            string userEmail = "";
            while (flag)
            {
                Console.Write("\nEnter Your Email : ");
                string emailInput = Console.ReadLine();
                if (IsValidEmail(emailInput))
                {
                    userEmail = emailInput;
                    flag = false;
                }
                else
                {
                    ErrorMessage("Enter a Valid Email");
                }
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Mail Will be send after 1 minute");
            Console.ForegroundColor = ConsoleColor.White;
            object obj = new object[1] { userEmail };
            Thread thread = new Thread(new ParameterizedThreadStart(MailMethod));
            thread.Start(obj);
        }
    }
}
