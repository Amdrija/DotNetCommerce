using Logeecom.DemoProjekat.PL.ViewModels.RepsonseModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Logeecom.DemoProjekat.PL.Filters
{
    public class ProductListImageFilter : ImageNameFilter
    {
        protected override ContentResult ModifyContentResultImage(ObjectResult result, string hostName)
        {
            var products = result.Value as IEnumerable<ProductResponseModel>;
            products = products.Select(product =>
            {
                product.Image = this.ProductImageToURI(product.Image, hostName);
                return product;
            });

            return new ContentResult { Content = JsonSerializer.Serialize(products), ContentType = "application/json" };
        }
    }
}
