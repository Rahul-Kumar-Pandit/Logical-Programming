using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD
{
    public enum TransactionType
    {
        Income,
        Expense
    }

    public enum CategoryType    
    {
        Food,
        Utilities,
        Rent,
        Entertainment,
        Savings,
        Others
    }
    public class FinanceRecord
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public CategoryType Category { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public string PaymentMethod { get; set; }
        public string Email {  get; set; }

        public FinanceRecord()
        {

        }
        public FinanceRecord(Guid id, decimal amount, TransactionType transactionType, CategoryType category, string description, DateTime transactionDate, string paymentMethod, string email)
        {
            Id = id;
            Amount = amount;
            TransactionType = transactionType;
            Category = category;
            Description = description;
            TransactionDate = transactionDate;
            PaymentMethod = paymentMethod;
            Email = email;
        }
    }

}
