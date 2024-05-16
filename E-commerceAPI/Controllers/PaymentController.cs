using E_commerceAPI.DTO;
using E_commerceAPI.Models;
using E_commerceAPI.Repository;
using E_commerceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceAPI.Controllers;


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

    [HttpGet("{id}")]
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
    public async Task<ActionResult<Payment>> Create([FromRoute] PaymentDTO payment, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid == true)
        {
            var createPayment = new Payment()
            {
                Id = payment.Id,
                Date = payment.Date,
                Amount = payment.Amount,
                Method = payment.Method
            };

            await _paymentRepository.Create(createPayment, cancellationToken);
            await _paymentRepository.Save();

            return CreatedAtRoute(nameof(Create), new { id = payment.Id }, createPayment);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Payment>> Delete(int id, CancellationToken cancellationToken)
    {
        await _paymentRepository.Delete(id, cancellationToken);
        await _paymentRepository.Save();

        return NoContent();
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Product>> Update(int id, [FromRoute] PaymentDTO paymentDTO, CancellationToken cancellationToken)
    {
        var update = await _paymentRepository.GetByIdAsync(id, cancellationToken);
        if (update == null)
        {
            return NotFound();
        }

        if (paymentDTO == null)
        {
            return BadRequest("Le payment n'est peut pas être null");
        }

        try
        {
            var createPayment = new Payment()
            {
                Id = paymentDTO.Id,
                Date = paymentDTO.Date,
                Amount = paymentDTO.Amount,
                Method = paymentDTO.Method
            };

            await _paymentRepository.Update(id, createPayment, cancellationToken);
            await _paymentRepository.Save();

            return CreatedAtRoute(nameof(Create), new { id = paymentDTO.Id }, createPayment);
            //return CreatedAtRoute(nameof(Create), new { id = product.Id }, createProduct);
        }
        catch (Exception)
        {
            _logger.LogError($"L'id={id} / le payment={paymentDTO} n\'existe pas");
            return StatusCode(500, "Une erreur est survenue lors de la mise à jour du payment");
        }
    }

}
