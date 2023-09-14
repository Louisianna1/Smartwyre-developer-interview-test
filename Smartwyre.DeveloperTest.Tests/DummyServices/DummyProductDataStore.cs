using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Tests.DummyServices
{
    public class DummyProductDataStore : IProductDataStore
    {
        public enum DummyProductIdentifier
        {
            NonExistentProductIdentifier,
            ProductThatDoesNotSupportFixedRateRebate,
            ProductThatHasPriceZero,
            ProductThatSupportsAllIncentiveTypes
        }


        public Product GetProduct(string productIdentifier)
        {
            if (productIdentifier == DummyProductIdentifier.NonExistentProductIdentifier.ToString())
            {
                return null;
            }

            Product dummyProduct = new Product();
            dummyProduct.Identifier = productIdentifier;

            if (productIdentifier == DummyProductIdentifier.ProductThatDoesNotSupportFixedRateRebate.ToString())
            {
                dummyProduct.Price = 5m;
                dummyProduct.Uom = "Uom";
                dummyProduct.SupportedIncentives = SupportedIncentiveType.FixedCashAmount | SupportedIncentiveType.AmountPerUom;
            }
            else if (productIdentifier == DummyProductIdentifier.ProductThatSupportsAllIncentiveTypes.ToString())
            {
                dummyProduct.Price = 5m;
                dummyProduct.Uom = "Uom";
                dummyProduct.SupportedIncentives = SupportedIncentiveType.FixedCashAmount | SupportedIncentiveType.AmountPerUom | SupportedIncentiveType.FixedRateRebate;
            }
            else if (productIdentifier == DummyProductIdentifier.ProductThatHasPriceZero.ToString())
            {
                dummyProduct.Price = 0m; // Invalid
                dummyProduct.Uom = "Uom";
                dummyProduct.SupportedIncentives = SupportedIncentiveType.FixedCashAmount | SupportedIncentiveType.AmountPerUom | SupportedIncentiveType.FixedRateRebate;
            }

            return dummyProduct;
            
        }
    }
}
