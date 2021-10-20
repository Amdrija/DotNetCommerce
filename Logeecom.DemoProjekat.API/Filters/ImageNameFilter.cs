using Logeecom.DemoProjekat.BL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Logeecom.DemoProjekat.PL.Filters
{
    public class ImageNameFilter : ActionFilterAttribute
    {

        //Template method pattern
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                throw context.Exception;
            }

            context.Result = this.ModifyContentResultImage(context.Result as ObjectResult, context.HttpContext.Request.Host.ToString());
        }

        protected string ProductImageToURI(string imageName, string hostName)
        {
            return hostName + "/" + ImageService.DEFAULT_IMAGE_PATH + "/" + imageName;
        }

        protected virtual ContentResult ModifyContentResultImage(ObjectResult result, string hostName)
        {
            string image = result.Value as string;
            image = this.ProductImageToURI(image, hostName);
            return new ContentResult { Content = image, ContentType = "text/plain; charset" };
        }
    }
}
