﻿using AngularEshop.Core.DTOs.Paging;
using AngularEshop.DataLayer.Entities.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace AngularEshop.Core.DTOs.Producs
{
    public class FilterProdcutsDTO : BasePaging
    {
        public string Title { get; set; }

        public int StartPrice { get; set; }

        public int EndPrice { get; set; }

        public List<Product> Products { get; set; }

        public FilterProdcutsDTO SetPaging(BasePaging paging)
        {
            this.PageId = paging.PageId;
            this.PageCount = paging.PageCount;
            this.StartPage = paging.StartPage;
            this.EndPage = paging.EndPage;
            this.TakeEntity = paging.TakeEntity;
            this.SkipEntity = paging.SkipEntity;
            this.ActivePage = paging.ActivePage;
            return this;
        }

        public FilterProdcutsDTO SetProducts(List<Product> products)
        {
            this.Products = products;
            return this;
        }
    }
}