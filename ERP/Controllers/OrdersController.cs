using ERP.Application.DTOs.Order;
using ERP.Application.Interfaces;
using ERP.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    #region GETs

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<OrderResponseDTO>> GetById(Guid id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);

        if (order is null)
            return NotFound($"Order with ID '{id}' not found.");

        return Ok(order);
    }

    #endregion


    #region CHANGE ORDER STATUS

    [HttpPatch("cancel/{id:guid}")]
    public async Task<IActionResult> CancelOrder(Guid id)
    {
        await _orderService.CancelOrderAsync(id);

        return Ok();
    }

    [HttpPatch("submit/{id:guid}")]
    public async Task<ActionResult<OrderResponseDTO>> SubmitOrder(Guid id)
    {
        var order = await _orderService.SubmitOrderAsync(id);

        return Ok(order);
    }

    [HttpPatch("complete/{id:guid}")]
    public async Task<ActionResult<OrderResponseDTO>> CompleteOrder(Guid id)
    {
        var order = await _orderService.CompleteOrderAsync(id);

        return Ok(order);
    }

    [HttpPatch("invoice/{id:guid}")]
    public async Task<ActionResult<OrderResponseDTO>> InvoiceOrder(Guid id)
    {
        var order = await _orderService.InvoiceOrderAsync(id);

        return Ok(order);
    }
    #endregion


    #region ADD, CREATE, REMOVE, UPDATES

    [HttpPost]
    public async Task<ActionResult<OrderResponseDTO>> CreateOrder([FromBody] CreateOrderDTO orderDTO)
    {
        if (orderDTO is null)
            return BadRequest("Product data must be provided.");

        var order = await _orderService.CreateOrderAsync(orderDTO);

        return CreatedAtAction(
            nameof(GetById),
            new { id = order.Id },
            order
            );
    }
    #endregion
}
