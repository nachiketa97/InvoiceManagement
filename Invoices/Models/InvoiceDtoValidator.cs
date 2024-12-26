using FluentValidation;

namespace Invoices.Models
{
    public class InvoiceDtoValidator : AbstractValidator<InvoiceDto>
    {
        public InvoiceDtoValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.InvoiceDate).NotEmpty();
            RuleFor(x => x.DueDate).NotEmpty();
            RuleFor(x => x.InvoiceItems).NotEmpty().Must(items => items.Any()).WithMessage("Invoice must have at least one item.");

            RuleForEach(x => x.InvoiceItems).ChildRules(item =>
            {
                item.RuleFor(i => i.Description).NotEmpty();
                item.RuleFor(i => i.Quantity).GreaterThan(0);
                item.RuleFor(i => i.UnitPrice).GreaterThan(0);
            });
        }
    }
}