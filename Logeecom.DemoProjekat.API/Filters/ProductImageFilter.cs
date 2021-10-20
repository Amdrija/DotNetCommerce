using Logeecom.DemoProjekat.PL.ViewModels.RepsonseModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Logeecom.DemoProjekat.PL.Filters
{
    public class ProductImageFilter : ImageNameFilter
    {
        protected override ContentResult ModifyContentResultImage(ObjectResult result, string hostName)
        {
            var product = result.Value as ProductResponseModel;
            product.Image = this.ProductImageToURI(product.Image, hostName);

            return new ContentResult { Content = JsonSerializer.Serialize(product), ContentType = "application/json" };
        }
    }
}
