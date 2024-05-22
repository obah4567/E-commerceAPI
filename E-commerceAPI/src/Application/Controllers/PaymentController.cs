using E_commerceAPI.src.Domain.DTO;
using E_commerceAPI.src.Domain.Models;
using E_commerceAPI.src.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceAPI.src.Application.Controllers;


[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly ILogger<PaymentController> _logger;

    public PaymentController(IPaymentRepository paymentRepository, ILogger<PaymentController> logger)
    {
        _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(_paymentRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
    }

    [HttpGet]
    public async Task<ActionResult<Payment>> GetAll(CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository.GetAllAsync(cancellationToken);
        if (payment == null)
        {
            return NotFound();
        }
        return Ok(payment);
    }

    [HttpGet("{id}", Name = "GetPaymentById")]
    public async Task<ActionResult<Payment>> GetById(int id, CancellationToken cancellationToken)
    {
        var getAll = await _paymentRepository.GetByIdAsync(id, cancellationToken);
        if (getAll == null)
        {
            return NotFound();
        }
        return Ok(getAll);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Payment>> Create([FromBody] PaymentDTO payment, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingPayementId = await _paymentRepository.GetByIdAsync(payment.Id, cancellationToken);
        if (existingPayementId != null)
        {
            return Conflict(new { message = $"Payment {payment.Id} already exists" });
        }

        var createPayment = new Payment()
        {
            Id = payment.Id,
            Date = payment.Date,
            Amount = payment.Amount,
            Method = payment.Method
        };

        await _paymentRepository.Create(createPayment, cancellationToken);
        await _paymentRepository.Save(cancellationToken);

        return CreatedAtRoute("GetPaymentById", new { id = createPayment.Id }, createPayment);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Payment>> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _paymentRepository.Delete(id, cancellationToken);
            await _paymentRepository.Save(cancellationToken);

            return NoContent();
        }
        catch (Exception)
        {
            _logger.LogError($"L'id {id} n\'existe pas");
            return StatusCode(StatusCodes.Status404NotFound, "L'Id n'a été trouvé ou à déjà supprimé");
        }
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Payment>> Update(int id, [FromBody] PaymentDTO payment, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var existingPayment = await _paymentRepository.GetByIdAsync(id, cancellationToken);
            if (existingPayment == null)
            {
                return NotFound($"Payment {payment.Id} not found");
            }

            existingPayment.Date = payment.Date;
            existingPayment.Method = payment.Method;
            existingPayment.Amount = payment.Amount;

            await _paymentRepository.Update(existingPayment, cancellationToken);
            await _paymentRepository.Save(cancellationToken);

            return NoContent();
        }
        catch (Exception)
        {
            _logger.LogError($"L'id={id} / le payment={payment} n\'existe pas");
            return StatusCode(500, "Une erreur est survenue lors de la mise à jour du payment");
        }
    }

}
