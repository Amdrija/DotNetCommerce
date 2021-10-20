using AutoMapper;
using Logeecom.DemoProjekat.BL.Services;
using Logeecom.DemoProjekat.DAL.Models;
using Logeecom.DemoProjekat.Exceptions;
using Logeecom.DemoProjekat.PL.Filters;
using Logeecom.DemoProjekat.PL.ViewModels.QueryModels;
using Logeecom.DemoProjekat.PL.ViewModels.RepsonseModels;
using Logeecom.DemoProjekat.PL.ViewModels.SubmitionModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DALProductQuery = Logeecom.DemoProjekat.DAL.QueryModels.ProductQuery;
using Logeecom.FileManager;
using Microsoft.AspNetCore.Hosting;

namespace Logeecom.DemoProjekat.PL.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ProductService productService;
        private readonly IMapper mapper;

        public ProductController(ProductService productService, IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }

        // GET: <ProductController>
        [HttpGet]
        [ProductListImageFilter]
        public IEnumerable<ProductResponseModel> Get([FromQuery] ProductQuery productQuery)
        {
            DALProductQuery dalProductQuery = this.mapper.Map<DALProductQuery>(productQuery);
            IEnumerable<Product> products = this.productService.GetProducts(dalProductQuery, User);

            return  products.Select(product => this.mapper.Map<ProductResponseModel>(product));
        }

        [HttpGet]
        [Authorize]
        [Route("count")]
        public int Count()
        {
            return this.productService.GetProductCount();
        }

        // GET <ProductController>/5
        [HttpGet("{id}")]
        [ProductImageFilter]
        public ProductResponseModel Get(int id)
        {
            return this.mapper.Map<ProductResponseModel>(this.productService.GetProduct(id, User));
        }

        // POST <ProductController>
        [HttpPost]
        [Authorize]
        [ProductImageFilter]
        public ProductResponseModel Post(ProductSubmitionModel sourceProduct, [FromServices] ImageService imageService)
        {
            if (!imageService.ImageExists(sourceProduct.Image, Request.Host.ToString()).Result)
            {
                throw new ImageNotFoundException($"Specified image: {sourceProduct.Image} not found. Try uploading the image first.");
            }

            Product product = this.mapper.Map<Product>(sourceProduct);
            Product createdProduct = this.productService.CreateProduct(product);

            return this.mapper.Map<ProductResponseModel>(createdProduct);
        }

        // PUT <ProductController>/5
        [HttpPut("{id}")]
        [Authorize]
        [ProductImageFilter]
        public ProductResponseModel Put(int id, ProductSubmitionModel sourceProduct, [FromServices] ImageService imageService)
        {
            if (!imageService.ImageExists(sourceProduct.Image, Request.Host.ToString()).Result)
            {
                throw new ImageNotFoundException($"Specified image: {sourceProduct.Image} not found. Try uploading the image first.");
            }

            sourceProduct.Id = id;

            Product product = this.mapper.Map<Product>(sourceProduct);
            Product updatedProduct = this.productService.UpdateProduct(product);

            return this.mapper.Map<ProductResponseModel>(updatedProduct); ;
        }

        // DELETE <ProductController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public void Delete(int id)
        {
            this.productService.DeleteProduct(id);
        }

        [HttpPost]
        [Route("image")]
        [Authorize]
        [ImageNameFilter]
        public async Task<string> AddImage(IFormFile image, [FromServices] ImageService imageService)
        {
            if (image == null)
            {
                throw new ImageNotFoundException($"Image not provided.");
            }

            if (!imageService.ImageExtensionValid(image))
            {
                throw new InvalidImageTypeException($"Specified image type not supported: {image.ContentType}");
            }

            if (!imageService.ImageSizeValid(image))
            {
                throw new InvalidImageSizeException($"Invalid image size. Image should be at least {ImageService.MINIMUM_WIDTH}px wide " +
                    $"and have an aspect ratio between: {ImageService.GetMinimumRatio()} and {ImageService.GetMaximumRatio()} ");
            }

            return await imageService.SaveImage(image);
        }
    }
}
