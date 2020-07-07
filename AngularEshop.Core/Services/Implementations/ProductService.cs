using AngularEshop.Core.DTOs.Paging;
using AngularEshop.Core.DTOs.Producs;
using AngularEshop.Core.Services.Interfaces;
using AngularEshop.Core.Utilities.Paging;
using AngularEshop.DataLayer.Entities.Product;
using AngularEshop.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEshop.Core.Services.Implementations
{
    public class ProductService : IProductService
    {

        #region constructor

        private IGenericRepository<Product> productRepository;
        private IGenericRepository<ProductCategory> productCategoryRepository;
        private IGenericRepository<ProductGallery> productGalleryRepository;
        private IGenericRepository<ProductSelectedCategory> productSelectedCategoryRepository;
        private IGenericRepository<ProductVisit> productVisitRepository;

        public ProductService(IGenericRepository<Product> productRepository, IGenericRepository<ProductCategory> productCategoryRepository, IGenericRepository<ProductGallery> productGalleryRepository, IGenericRepository<ProductSelectedCategory> productSelectedCategoryRepository, IGenericRepository<ProductVisit> productVisitRepository)
        {
            this.productRepository = productRepository;
            this.productCategoryRepository = productCategoryRepository;
            this.productGalleryRepository = productGalleryRepository;
            this.productSelectedCategoryRepository = productSelectedCategoryRepository;
            this.productVisitRepository = productVisitRepository;
        }

        #endregion

        #region product

        public async Task AddProduct(Product product)
        {
            await productRepository.AddEntity(product);
            await productRepository.SaveChanges();
        }

        public async Task UpdateProduct(Product product)
        {
            productRepository.UpdateEntity(product);
            await productRepository.SaveChanges();
        }

        public async Task<FilterProductsDTO> FilterProducts(FilterProductsDTO filter)
        {
            var productsQuery = productRepository.GetEntitiesQuery().AsQueryable();

            switch (filter.OrderBy)
            {
                case ProductOrderBy.PriceAsc:
                    productsQuery = productsQuery.OrderBy(s => s.Price);
                    break;
                case ProductOrderBy.PriceDec:
                    productsQuery = productsQuery.OrderByDescending(s => s.Price);
                    break;
            }

            if (!string.IsNullOrEmpty(filter.Title))
                productsQuery = productsQuery.Where(s => s.ProductName.Contains(filter.Title));

            if (filter.StartPrice != 0)
                productsQuery = productsQuery.Where(s => s.Price >= filter.StartPrice);

            if (filter.EndPrice != 0)
                productsQuery = productsQuery.Where(s => s.Price <= filter.EndPrice);

            productsQuery = productsQuery.Where(s => s.Price >= filter.StartPrice);

            if (filter.Categories != null && filter.Categories.Any())
                productsQuery = productsQuery.SelectMany(s =>
                        s.ProductSelectedCategories.Where(f => filter.Categories.Contains(f.ProductCategoryId)).Select(t => t.Product));

            if (filter.EndPrice != 0)
                productsQuery = productsQuery.Where(s => s.Price <= filter.EndPrice);

            var count = (int)Math.Ceiling(productsQuery.Count() / (double)filter.TakeEntity);

            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);

            var products = await productsQuery.Paging(pager).ToListAsync();

            return filter.SetProducts(products).SetPaging(pager);
        }

        public async Task<Product> GetProductById(long productId)
        {
            return await productRepository.GetEntityById(productId);
        }

        #endregion

        #region product categories

        public async Task<List<ProductCategory>> GetAllActiveProductCategories()
        {
            return await productCategoryRepository.GetEntitiesQuery().Where(s => !s.IsDelete).ToListAsync();
        }

        #endregion

        #region dispose

        public void Dispose()
        {
            productRepository?.Dispose();
            productCategoryRepository?.Dispose();
            productGalleryRepository?.Dispose();
            productSelectedCategoryRepository?.Dispose();
            productVisitRepository?.Dispose();
        }


        #endregion
    }
}
