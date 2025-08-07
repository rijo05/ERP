using ERP.Application.DTOs.Order;
using ERP.Application.DTOs.OrderItems;
using ERP.Application.Interfaces;
using ERP.Domain.Entities;
using ERP.Domain.Enums;
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

    [HttpGet("{id:Guid}/produts")]
    public async Task<ActionResult<List<OrderItemResponseDTO>>> GetOrderItems(Guid id)
    {
        var orderItems = await _orderService.GetOrderItemsAsync(id);

        return Ok(orderItems);
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
            return BadRequest("Order data must be provided.");

        var order = await _orderService.CreateOrderAsync(orderDTO);

        return CreatedAtAction(
            nameof(GetById),
            new { id = order.Id },
            order
            );
    }

    [HttpPost]
    public async Task<ActionResult<OrderResponseDTO>> AddItem([FromBody] UpdateOrderItemDTO orderDTO)
    {
        if (orderDTO is null)
            return BadRequest("Item data must be provided.");

        var order = await _orderService.AddItemAsync(orderDTO);

        return Ok(order);
    }

    [HttpPatch]
    public async Task<ActionResult<OrderResponseDTO>> UpdateQuantity(UpdateOrderItemDTO orderDTO)
    {
        if (orderDTO is null)
            return BadRequest("Item data must be provided.");

        var order = _orderService.UpdateQuantityAsync(orderDTO);

        return Ok(order);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveItem(UpdateOrderItemDTO orderDTO)
    {
        if (orderDTO is null)
            return BadRequest("Item data must be provided.");

        await _orderService.RemoveItemAsync(orderDTO);

        return NoContent();
    }


    #endregion
}
