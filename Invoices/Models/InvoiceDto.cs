using System.ComponentModel.DataAnnotations;

namespace Invoices.Models
{
    public class InvoiceDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public ICollection<InvoiceItemDto> InvoiceItems { get; set; } = new List<InvoiceItemDto>();
    }

    public class InvoiceItemDto
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }
    }
}