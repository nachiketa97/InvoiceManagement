using Microsoft.AspNetCore.Mvc;
using Invoices.Models;
using Invoices.Repositories;
using AutoMapper;
using FluentValidation;

namespace Invoices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<InvoiceDto> _validator;

        public InvoicesController(IInvoiceRepository invoiceRepository, IMapper mapper, IValidator<InvoiceDto> validator)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            return Ok(await _invoiceRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(int id)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return Ok(invoice);
        }

        [HttpPost]
        public async Task<ActionResult<Invoice>> CreateInvoice(InvoiceDto invoiceDto)
        {
            var validationResult = await _validator.ValidateAsync(invoiceDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var invoice = _mapper.Map<Invoice>(invoiceDto);
            invoice.TotalAmount = invoice.InvoiceItems.Sum(item => item.Amount);
            await _invoiceRepository.CreateAsync(invoice);

            return CreatedAtAction(nameof(GetInvoice), new { id = invoice.Id }, invoice);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(int id, InvoiceDto invoiceDto)
        {
            var validationResult = await _validator.ValidateAsync(invoiceDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var invoice = await _invoiceRepository.GetByIdAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            invoice.CustomerId = invoiceDto.CustomerId;
            invoice.InvoiceDate = invoiceDto.InvoiceDate;
            invoice.DueDate = invoiceDto.DueDate;
            invoice.InvoiceItems = invoiceDto.InvoiceItems.Select(itemDto =>
                new InvoiceItem
                {
                    Description = itemDto.Description,
                    Quantity = itemDto.Quantity,
                    UnitPrice = itemDto.UnitPrice,
                    Amount = itemDto.Quantity * itemDto.UnitPrice
                }).ToList();
            invoice.TotalAmount = invoice.InvoiceItems.Sum(item => item.Amount);

            await _invoiceRepository.UpdateAsync(invoice);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            await _invoiceRepository.DeleteAsync(id);
            return NoContent();
        }

       
    }
}