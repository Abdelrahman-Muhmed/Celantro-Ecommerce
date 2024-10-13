﻿using EcommerceCoffe_DAL.Models;
using System.Data.Entity.ModelConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceCoffe_DAL.Models.Product;

namespace Amazon_EF.Data.Config
{
    internal class ProductBrandConfiguration : EntityTypeConfiguration<ProductBrand>
    {
        public ProductBrandConfiguration()
        {
            Property(p => p.Name)
                .IsRequired();


        }
    }
}
