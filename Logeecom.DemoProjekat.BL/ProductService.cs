using System;
using System.Collections.Generic;
using Logeecom.DemoProjekat.DAL.Models;
using Logeecom.DemoProjekat.DAL.Repositories;
using Logeecom.DemoProjekat.Exceptions;
using System.IO;
using Logeecom.DemoProjekat.DAL.QueryModels;
using System.Security.Claims;
using Logeecom.DemoProjekat.DAL;

namespace Logeecom.DemoProjekat.BL.Services
{
    public class ProductService
    {
        private const int DEFAULT_VIEW_COUNT = 0;
        private readonly UnitOfWork unitOfWork;
        
        public ProductService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public int GetProductCount()
        {
            return this.unitOfWork.ProductRepository.GetProductCount();
        }

        public Product GetProduct(int id, ClaimsPrincipal user)
        {
            Product product = this.unitOfWork.ProductRepository.GetProduct(id);

            if(product == null)
            {
                throw new ProductNotFoundException($"Product with id: {id} not found.");
            }

            //if the user isn't authenticated, he can only view enabled products.
            if (!user.Identity.IsAuthenticated && !product.Enabled)
            {
                throw new UnauthorizedAccessException("Forbidden.");
            }

            return product;
        }

        public IEnumerable<Product> GetProducts(ProductQuery product, ClaimsPrincipal user)
        {
            //if the user isn't authenticated, he can only view enabled products.
            if(!user.Identity.IsAuthenticated)
            {
                product.Enabled = true;
            }

            return this.unitOfWork.ProductRepository.GetProducts(product);
        }

        public Product CreateProduct(Product product)
        {
            product.Id = 0;

            this.ProductImageValid(product);

            product.ViewCount = DEFAULT_VIEW_COUNT;

            Product createdProduct = this.unitOfWork.ProductRepository.CreateProduct(product);
            try
            {
                this.unitOfWork.Save();
            }
            catch (UniqueConstraintException)
            {
                throw new SKUExsistsException($"The product with the SKU: {product.SKU} already exists. Try a different SKU.");
            }
            catch (ForeignKeyConstraintException)
            {
                throw new CategoryNotFoundException($"Couldn't find the category with id: {product.CategoryId}.");
            }

            return createdProduct;
        }

        public Product UpdateProduct(Product product)
        {
            this.ProductImageValid(product);
            
            this.unitOfWork.ProductRepository.UpdateProduct(product);
            
            try
            {
                this.unitOfWork.Save();
            }
            catch (UniqueConstraintException)
            {
                throw new SKUExsistsException($"The product with the SKU: {product.SKU} already exists. Try a different SKU.");
            }
            catch (ForeignKeyConstraintException)
            {
                throw new CategoryNotFoundException($"Couldn't find the category with id: {product.CategoryId}.");
            }
            catch (PrimaryKeyConstraintException)
            {
                throw new ProductNotFoundException($"Product with specified id: {product.Id} not found.");
            }

            return product;
        }

        public Product IncrementViewCount(Product product)
        {
            product.ViewCount++;
            return this.unitOfWork.ProductRepository.UpdateProduct(product);
        }

        public void DeleteProduct(int id)
        {
            Product product = this.unitOfWork.ProductRepository.GetProduct(id);
            if(product == null)
            {
                throw new ProductNotFoundException($"Product with specified id: {id} not found.");
            }

            this.unitOfWork.ProductRepository.DeleteProduct(product);
            this.unitOfWork.Save();
        }

        private void ProductImageValid(Product product)
        {
            product.Image = Path.GetFileName(product.Image);

            if (this.unitOfWork.ProductRepository.ProductHasImage(product.Image, product.Id))
            {
                throw new ImageAlreadyTakenException($"A product with image: {product.Image} already exists.");
            }
        }
    }
}
