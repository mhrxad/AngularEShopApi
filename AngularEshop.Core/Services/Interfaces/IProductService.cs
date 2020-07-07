﻿using AngularEshop.Core.DTOs.Producs;
using AngularEshop.DataLayer.Entities.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AngularEshop.Core.Services.Interfaces
{
    public interface IProductService : IDisposable
    {
        #region product

        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task<FilterProductsDTO> FilterProducts(FilterProductsDTO filter);
        Task<Product> GetProductById(long productId);



        #endregion

        #region product categories

        Task<List<ProductCategory>> GetAllActiveProductCategories();

        #endregion

    }
}
