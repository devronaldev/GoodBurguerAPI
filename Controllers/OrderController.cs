using System.Linq.Expressions;
using GoodBurguerAPI.Configurations;
using GoodBurguerAPI.DTOs.Request;
using GoodBurguerAPI.Models;
using GoodBurguerAPI.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoodBurguerAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class OrderController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    [HttpGet("items")]
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
            return StatusCode(500, new
            {
                Message = "An error occured, please try again later.",
                Detail = e.Message
            });
        }
    }

    [HttpGet("sandwichs")]
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
            return StatusCode(500, new
            {
                Message = "An error occured, please try again later.",
                Detail = e.Message
            });
        }
    }

    [HttpGet("extras")]
    public async Task<ActionResult<List<Item>>> GetExtrasAsync()
    {
        try
        {
            var items = await GetItemsAsync(filter: x => x.ItemType == EItemType.Extra);
            
            if (!items.Any())
            {
                return NoContent();
            }

            return Ok(items);
        }
        catch (Exception e)
        {
            return StatusCode(500, new
            {
                Message = "An error occured, please try again later.",
                Detail = e.Message
            });
        }
    }

    [HttpPost("order")]
    public async Task<ActionResult<Order>> PostOrderAsync(CreateOrderDTO dto)
    {
        if (dto.SandwichId == null && dto.ExtraId == null && dto.DrinkId == null)
        {
            return BadRequest(new
            {
                Message = "At least one item (Sandwich, Extra, or Drink) must be provided."
            });
        }
        
        try
        {
            var order = await CreateOrderAsync(dto);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return Ok(order);
        }
        catch (ArgumentException e)
        {
            return BadRequest(new
            {
                Message = e.Message
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new
            {
                Message = "An error occured, please try again later.",
                Detail = e.Message
            });
        }
    }
    
    [NonAction]
    private async Task<List<Item>> GetItemsAsync(Expression<Func<Item, bool>>? filter = null)
    {
        IQueryable<Item> query = _context.Items;
        
        if (filter != null)
            query = query.Where(filter);
        
        return await query.OrderBy(x=>x.ItemId).ToListAsync();
    }
    
    [NonAction]
    private async Task<Order> CreateOrderAsync(CreateOrderDTO dto)
    {
        var sandwich = dto.SandwichId.HasValue 
            ? await _context.Items.FindAsync(dto.SandwichId.Value) 
            : null;

        var extra = dto.ExtraId.HasValue 
            ? await _context.Items.FindAsync(dto.ExtraId.Value) 
            : null;
    
        var drink = dto.DrinkId.HasValue
            ? await _context.Items.FindAsync(dto.DrinkId.Value)
            : null;

        // Verify if the types of items are different.
        if ((sandwich?.ItemType == extra?.ItemType) || 
            (extra?.ItemType == drink?.ItemType) ||
            (sandwich?.ItemType == drink?.ItemType))
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