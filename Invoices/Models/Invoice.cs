using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Invoices.Models
{
    public enum InvoiceStatus
    {
        Draft,
        Sent,
        Paid,
        Overdue
    }

    public class Invoice
    {
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public InvoiceStatus Status { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public DateTime? ModifiedOn { get; set; }

        public Customer Customer { get; set; }
    }

    public class InvoiceItem
    {
        public int Id { get; set; }

        [Required]
        public int InvoiceId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public Invoice Invoice { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        // ... other customer properties
    }
}