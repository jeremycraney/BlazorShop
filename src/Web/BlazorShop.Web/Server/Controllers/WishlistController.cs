﻿namespace BlazorShop.Web.Server.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure.Extensions;
    using Services.Wishlist;
    using Shared.Products;

    using static Infrastructure.WebConstants;

    [Authorize]
    public class WishlistController : ApiController
    {
        private readonly IWishlistService wishlistService;

        public WishlistController(IWishlistService wishlistsService)
            => this.wishlistService = wishlistsService;

        [HttpGet]
        public async Task<IEnumerable<ProductsListingResponseModel>> Get()
            => await this.wishlistService.GetByUserIdAsync(this.User.GetId());

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] int id)
        {
            await this.wishlistService.AddProductAsync(id, this.User.GetId());

            return this.Ok();
        }

        [HttpDelete(Id)]
        public async Task<ActionResult> Remove(int id)
        {
            var removed = await this.wishlistService.RemoveProductAsync(id, this.User.GetId());
            if (!removed)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }
    }
}
