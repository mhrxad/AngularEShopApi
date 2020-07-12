using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularEshop.Core.DTOs.Producs;
using AngularEshop.Core.Services.Interfaces;
using AngularEshop.Core.Utilities.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularEshop.WebApi.Controllers
{

    public class ProductsController : SiteBaseController
    {
        #region constructor

        private IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        #endregion

        #region products

        [HttpGet("filter-products")]
        public async Task<IActionResult> GetProducts([FromQuery]FilterProductsDTO filter)
        {
            var products = await productService.FilterProducts(filter);

            return JsonResponseStatus.Success(products);
        }

        #endregion

        #region get products categories

        [HttpGet("product-active-categories")]
        public async Task<IActionResult> GetProductsCategories()
        {
            return JsonResponseStatus.Success(await productService.GetAllActiveProductCategories());
        }

        #endregion

        #region get single product

        [HttpGet("single-product/{id}")]
        public async Task<IActionResult> GetProduct(long id)
        {
            var product = await productService.GetProductById(id);
            var productGalleries = await productService.GetProductActiveGalleries(id);

            if (product != null)
                return JsonResponseStatus.Success(new { product = product, galleries = productGalleries });

            return JsonResponseStatus.NotFound();
        }

        #endregion

    }
}