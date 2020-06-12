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
        public async Task<IActionResult> GetProducts([FromQuery]FilterProdcutsDTO filter)
        {
            var products = await productService.FilterProducts(filter);

            return JsonResponseStatus.Success(products);
        }

        #endregion
    }
}