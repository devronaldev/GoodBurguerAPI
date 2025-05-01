using System.Linq.Expressions;
using GoodBurguerAPI.Configurations;
using GoodBurguerAPI.DTOs.Request;
using GoodBurguerAPI.DTOs.Response;
using GoodBurguerAPI.Models;
using GoodBurguerAPI.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoodBurguerAPI.Controllers
{
    /// <summary>
    /// Controller for handling orders. Provides actions for managing orders, items, and order updates.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes the controller with the provided application DB context.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all items from the menu.
        /// </summary>
        /// <returns>List of all menu items</returns>
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<Item>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorDTO),StatusCodes.Status500InternalServerError)]
        [HttpGet("item")]
        public async Task<ActionResult<List<Item>>> GetAllItemsAsync()
        {
            try
            {
                var items = await GetItemsAsync();

                if (!items.Any()) // Any() >>> Count()
                {
                    return NoContent();
                }

                return Ok(items);
            }
            catch (Exception e)
            {
                return StatusCode(500, new ErrorDTO
                {
                    Message = "An error occurred, please try again later.",
                    Detail = e.Message
                });
            }
        }

        /// <summary>
        /// Retrieves all sandwiches from the menu.
        /// </summary>
        /// <returns>List of sandwiches</returns>
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<Item>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorDTO),StatusCodes.Status500InternalServerError)]
        [HttpGet("sandwich")]
        public async Task<ActionResult<List<Item>>> GetSandwichsAsync()
        {
            try
            {
                var items = await GetItemsAsync(filter: x => x.ItemType == EItemType.Sandwich);

                if (!items.Any())
                {
                    return NoContent();
                }

                return Ok(items);
            }
            catch (Exception e)
            {
                return StatusCode(500, new ErrorDTO
                {
                    Message = "An error occurred, please try again later.",
                    Detail = e.Message
                });
            }
        }

        /// <summary>
        /// Retrieves all extras from the menu.
        /// </summary>
        /// <returns>List of extras</returns>
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<Item>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorDTO),StatusCodes.Status500InternalServerError)]
        [HttpGet("extra")]
        public async Task<ActionResult<List<Item>>> GetExtrasAsync()
        {
            try
            {
                var items = await GetItemsAsync(filter:(x => x.ItemType == EItemType.Extra || x.ItemType == EItemType.Drink));

                if (!items.Any())
                {
                    return NoContent();
                }

                return Ok(items);
            }
            catch (Exception e)
            {
                return StatusCode(500, new ErrorDTO
                {
                    Message = "An error occurred, please try again later.",
                    Detail = e.Message
                });
            }
        }

        /// <summary>
        /// Creates a new order and calculates the total price.
        /// </summary>
        /// <param name="dto">The order creation details including optional SandwichId, ExtraId, and DrinkId.</param>
        /// <returns>
        /// Returns a 201 Created response with the calculated order price if successful,
        /// 400 Bad Request if input is invalid or required items are missing,
        /// or 500 Internal Server Error in case of unexpected errors.
        /// </returns>
        /// <response code="201">Returns the price details of the newly created order.</response>
        /// <response code="400">If the request is invalid or no items were provided.</response>
        /// <response code="500">If an unexpected error occurs while creating the order.</response>
        [Produces("application/json")]
        [ProducesResponseType(typeof(OrderPriceDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDTO),StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<OrderPriceDTO>> PostOrderAsync(CreateOrderDTO dto)
        {
            if (dto.SandwichId == null && dto.ExtraId == null && dto.DrinkId == null)
            {
                return BadRequest(new ErrorDTO
                {
                    Message = "At least one item (Sandwich, Extra, or Drink) must be provided."
                });
            }

            try
            {
                var order = await CreateOrderAsync(dto);
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return Created("orderPrice", (OrderPriceDTO)order);
            }
            catch (ArgumentException e)
            {
                return BadRequest(new ErrorDTO
                {
                    Message = e.Message
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ErrorDTO
                {
                    Message = "An error occurred, please try again later.",
                    Detail = e.Message
                });
            }
        }

        /// <summary>
        /// Retrieves all orders from the database.
        /// </summary>
        /// <returns>
        /// A list of all orders.
        /// </returns>
        /// <response code="200">Returns the list of orders.</response>
        /// <response code="204">If there are no orders in the database.</response>
        /// <response code="500">If an unexpected error occurs while retrieving the data.</response>
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<ViewOrderDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorDTO),StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetOrdersAsync()
        {
            try
            {
                var orders = await _context.Orders
                    .Include(x => x.Sandwich)
                    .Include(x => x.Extra)
                    .Include(x => x.Drink)
                    .Select(x => new ViewOrderDTO
                    {
                        OrderId = x.OrderId,
                        Price = x.Price,
                        Sandwich = x.Sandwich,
                        Extra = x.Extra,
                        Drink = x.Drink
                    })
                    .ToListAsync();
                if (!orders.Any())
                {
                    return NoContent();
                }
                
                return Ok(orders);
            }
            catch (Exception e)
            {
                return StatusCode(500, new ErrorDTO
                {
                    Message = "An error occurred, please try again later.",
                    Detail = e.Message
                });
            }
        }

        /// <summary>
        /// Updates an existing order.
        /// </summary>
        /// <param name="dto">The updated order details.</param>
        /// <returns>Status of the update operation.</returns>
        /// <response code="204">Order was successfully updated.</response>
        /// <response code="400">The request is invalid or missing required fields.</response>
        /// <response code="404">Order not found in the database.</response>
        /// <response code="500">An internal server error occurred.</response>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorDTO),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDTO),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDTO),StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<ActionResult> PutOrderAsync(UpdateOrderDTO dto)
        {
            if (dto.SandwichId == null && dto.ExtraId == null && dto.DrinkId == null)
            {
                return BadRequest(new ErrorDTO
                {
                    Message = "At least one item (Sandwich, Extra, or Drink) must be provided."
                });
            }

            try
            {
                var updateOrder = await CreateOrderAsync(dto);
                var order = await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == dto.OrderId);

                if (order == null)
                {
                    return NotFound(new ErrorDTO
                    {
                        Message = "Order not found.",
                        Detail = "The order was not found. Verify if the database is properly configured."
                    });
                }

                order.Sandwich = updateOrder.Sandwich;
                order.Extra = updateOrder.Extra;
                order.Drink = updateOrder.Drink;
                order.Price = updateOrder.Price;

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (ArgumentException e)
            {
                return BadRequest(new ErrorDTO
                {
                    Message = e.Message
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ErrorDTO
                {
                    Message = "An error occurred, please try again later.",
                    Detail = e.Message
                });
            }
        }

        /// <summary>
        /// Deletes an order from the database.
        /// </summary>
        /// <param name="orderId">The ID of the order to be deleted.</param>
        /// <returns>Status of the delete operation.</returns>
        /// <response code="204">The order was successfully deleted.</response>
        /// <response code="400">The provided orderId is invalid.</response>
        /// <response code="404">The order with the specified orderId was not found.</response>
        /// <response code="500">An internal server error occurred.</response>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorDTO),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDTO),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDTO),StatusCodes.Status500InternalServerError)]
        [HttpDelete("{orderId}")]
        public async Task<ActionResult> DeleteOrderAsync(int orderId)
        {
            if (orderId == 0)
            {
                return BadRequest(new ErrorDTO
                {
                    Message = "The orderId is required.",
                    Detail = "The orderId is required. Verify if your input is correctly configured."
                });
            }

            try
            {
                var deleteOrder = await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId);
                if (deleteOrder == null)
                {
                    return NotFound(new ErrorDTO
                    {
                        Message = "Order not found.",
                        Detail = "The order was not found. Verify if the database is properly configured."
                    });
                }

                _context.Orders.Remove(deleteOrder);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, new ErrorDTO
                {
                    Message = "An error occurred, please try again later.",
                    Detail = e.Message
                });
            }
        }

        #region Private Methods

        /// <summary>
        /// Retrieves a list of items with an optional filter.
        /// </summary>
        /// <param name="filter">Optional filter expression to narrow down the query.</param>
        /// <returns>List of items</returns>
        [NonAction]
        private async Task<List<Item>> GetItemsAsync(Expression<Func<Item, bool>>? filter = null)
        {
            IQueryable<Item> query = _context.Items;

            if (filter != null)
                query = query.Where(filter);

            return await query.OrderBy(x => x.ItemId).ToListAsync();
        }

        /// <summary>
        /// Creates a new order based on the provided order details.
        /// </summary>
        /// <param name="dto">The order creation details.</param>
        /// <returns>The newly created order.</returns>
        [NonAction]
        private async Task<Order> CreateOrderAsync(CreateOrderDTO dto)
        {
            var sandwich = dto.SandwichId.HasValue
                ? await _context.Items.FindAsync(dto.SandwichId.Value)
                : null;

            if (sandwich != null && sandwich.ItemType != EItemType.Sandwich)
            {
                throw new ArgumentException("The type of the sandwich is not a valid item type.");
            }

            var extra = dto.ExtraId.HasValue
                ? await _context.Items.FindAsync(dto.ExtraId.Value)
                : null;

            if (extra != null && extra.ItemType != EItemType.Extra)
            {
                throw new ArgumentException("The type of the extra is not a valid item type.");
            }

            var drink = dto.DrinkId.HasValue
                ? await _context.Items.FindAsync(dto.DrinkId.Value)
                : null;

            if (drink != null && drink.ItemType != EItemType.Drink)
            {
                throw new ArgumentException("The type of the drink is not a valid item type.");
            }
            
            // Verify if any two non-null items share the same type
            if ((sandwich != null && extra != null && sandwich.ItemType == extra.ItemType) ||
                (extra != null && drink != null && extra.ItemType == drink.ItemType) ||
                (sandwich != null && drink != null && sandwich.ItemType == drink.ItemType))
            {
                throw new ArgumentException("You have repeated items of the same type");
            }

            var price =
                (sandwich?.ItemPrice ?? 0) +
                (extra?.ItemPrice ?? 0) +
                (drink?.ItemPrice ?? 0);

            decimal discount = 0;
            if (sandwich != null && extra != null && drink != null)
            {
                discount = 0.20m;
            }
            else if (sandwich != null && drink != null)
            {
                discount = 0.15m;
            }
            else if (sandwich != null && extra != null)
            {
                discount = 0.10m;
            }

            price -= price * discount;

            return new Order(price, sandwich, extra, drink);
        }
    }
}

#endregion