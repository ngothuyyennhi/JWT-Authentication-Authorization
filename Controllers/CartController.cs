using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using WebApplication2.Model;
using WebApplication2.Services;
using ZstdSharp.Unsafe;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IConfiguration configuration1;
        private readonly ApplicationDbContext context;
        private readonly IAuthService authService;

        public CartController(IConfiguration configuration1, ApplicationDbContext context, IAuthService authService)
        {
            this.configuration1 = configuration1;
            this.context = context;
            this.authService = authService;
        }
        [Authorize]
        [HttpGet("getcartlist")]
        public async Task<IActionResult> GetUserCart()
        {
            string cartId =  authService.loginUser();

            //has relation ship
            var shoppingCart = await context.cart
                .Include(c => c.cart_detail)
                .Where(c => c.Cart_ID == cartId)
                .FirstOrDefaultAsync();
            //doesn't has relationship using query

            //var shoppingCart = await (from cart in context.cart
            //                          join cart_detail in context.cart_detail
            //                          on cart.Cart_ID equals cart_detail.Cart_Id
            //                          where cart.Cart_ID == cartId
            //                          select cart_detail
            //             ).ToListAsync();

            //doesn't has relationship using dbcontext

            //var shoppingCart = await context.cart
            //.Join(context.cart_detail,c => c.Cart_ID,cd => cd.Cart_Id,
            //    (_cart, _cartdetail) => new
            //    {
            //        Cart = _cart,
            //        CartDetail = _cartdetail
            //    }
            //)
            //.Where(joinResult => joinResult.Cart.Cart_ID == cartId)
            //.Select(joinResult => new
            //{
            //    CartId = joinResult.Cart.Cart_ID,
            //    Product = joinResult.CartDetail.Product_Id,
            //    Quantity = joinResult.CartDetail.Quantity
            //})
            //.ToListAsync();




            if (shoppingCart == null)
            {
                return NotFound("Cart doesn't exist"); 
            }

            return Ok(shoppingCart);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCart([FromBody] CartDetail detail)
        {
            string cartId = authService.loginUser();
            Cart exist = await context.cart.FirstOrDefaultAsync(p => p.Cart_ID == cartId);
            if (exist != null)
            {
                CartDetail cartDetail = new CartDetail(cartId, detail.Product_Id, detail.Product_Id, detail.Price);
                context.cart_detail.Add(cartDetail);
                exist.Total += detail.Price * detail.Quantity;
                context.cart.Update(exist);
                context.SaveChanges();
                return Ok(cartDetail);
            }
            else
            {
                Cart newcart = new Cart(cartId);

                context.cart.Add(newcart);

                CartDetail cartDetail = new CartDetail(cartId, detail.Product_Id, detail.Product_Id, detail.Price);
                context.cart_detail.Add(cartDetail);
                context.SaveChanges();
                return Ok(cartDetail);
            }

        }
    }
}
