﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project0.StoreApplication.Storage.Models;

namespace StoreAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProductsController : ControllerBase
    {
        private readonly StoreApplicationDBContext _context;

        public OrderProductsController(StoreApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/OrderProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderProduct>>> GetOrderProducts()
        {
            return await _context.OrderProducts.ToListAsync();
        }

        // GET: api/OrderProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderProduct>> GetOrderProduct(short id)
        {
            var orderProduct = await _context.OrderProducts.FindAsync(id);

            if (orderProduct == null)
            {
                return NotFound();
            }

            return orderProduct;
        }

        // PUT: api/OrderProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderProduct(short id, OrderProduct orderProduct)
        {
            if (id != orderProduct.OrderProductId)
            {
                return BadRequest();
            }

            _context.Entry(orderProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OrderProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderProduct>> PostOrderProduct(OrderProduct orderProduct)
        {
            _context.OrderProducts.Add(orderProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderProduct", new { id = orderProduct.OrderProductId }, orderProduct);
        }

        // DELETE: api/OrderProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderProduct(short id)
        {
            var orderProduct = await _context.OrderProducts.FindAsync(id);
            if (orderProduct == null)
            {
                return NotFound();
            }

            _context.OrderProducts.Remove(orderProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderProductExists(short id)
        {
            return _context.OrderProducts.Any(e => e.OrderProductId == id);
        }
    }
}
