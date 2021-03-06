﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNET.Models
{
    public class LinqValueCalculator : IValueCalculator
    {
        private IDiscountHelper discounter;
        public static int counter = 0;

        public LinqValueCalculator(IDiscountHelper discountParam)
        {
            discounter = discountParam;
            System.Diagnostics.Debug.WriteLine(string.Format("Instance {0} created", ++counter));
        }
        public decimal ValueProducts(IEnumerable<Product> products)
        {
            //return products.Sum(p => p.Price);
            return discounter.ApplyDiscount(products.Sum(p => p.Price));
        }
    }

    public class ShoppingCart2
    {
        private IValueCalculator calc;
        public ShoppingCart2(IValueCalculator calcParam)
        {
            calc = calcParam;
        }
        public IEnumerable<Product> Products { get; set; }
        public decimal CalculateProductTotal()
        {
            return calc.ValueProducts(Products);
        }
    }

    public interface IValueCalculator
    {
        decimal ValueProducts(IEnumerable<Product> products);
    }

    public interface IDiscountHelper
    {
        decimal ApplyDiscount(decimal totalParam);
    }

    public class DefaultDiscountHelper : IDiscountHelper
    {
        public decimal discountSize;
        public DefaultDiscountHelper(decimal discountParam)
        {
            discountSize = discountParam;
        }
        public decimal ApplyDiscount(decimal totalParam)
        {
            return (totalParam - (discountSize / 100m * totalParam));
        }
    }

    public class FlexibleDiscountHelper : IDiscountHelper
    {
        public decimal ApplyDiscount(decimal totalParam)
        {
            decimal discount = totalParam > 100 ? 70 : 25;
            return (totalParam - (discount / 100m * totalParam));
        }
    }

    public class MinimumDiscountHelper:IDiscountHelper
    {
        public decimal ApplyDiscount(decimal totalParam)
        {
            if (totalParam<0)
            {
                throw new NotImplementedException();
            }
            else if (totalParam > 100)
            {
                return totalParam * 0.9M;
            }
            else if (totalParam >= 10 && totalParam <= 100)
            {
                return totalParam -5;
            }
            else
            {
                return totalParam;
            }
        }
    }

}