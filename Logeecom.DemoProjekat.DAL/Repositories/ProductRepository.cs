using System.Collections.Generic;
using System.Linq;
using Logeecom.DemoProjekat.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Logeecom.DemoProjekat.DAL.QueryModels;
using Logeecom.DemoProjekat.Exceptions;
using Microsoft.Data.SqlClient;

namespace Logeecom.DemoProjekat.DAL.Repositories
{
    public class ProductRepository
    {
        private readonly DemoDbContext db;

        public ProductRepository(DemoDbContext db)
        {
            this.db = db;
        }

        public int GetProductCount()
        {
            return this.db.Products.Count();
        }

        public bool ProductExsists(int id)
        {
            return this.db.Products.Where(product => product.Id == id).Any();
        }

        public bool ProductSKUExsists(string SKU, int? id = null)
        {
            return this.db.Products.Where(product => product.SKU == SKU && product.Id != id).Any();
        }

        public bool ProductHasImage(string imagePath, int? id = null)
        {
            return this.db.Products.Where(product => product.Image == imagePath && product.Id != id).Any();
        }

        public Product GetProduct(int id)
        {
            return this.db.Products.Find(id);
        }

        public IEnumerable<Product> GetProducts(ProductQuery productQuery)
        {
            IQueryable<Product> query = this.db.Products.AsNoTrackingWithIdentityResolution();

            if(productQuery?.MinPrice != null)
            {
                query = query.Where(product => product.Price >= productQuery.MinPrice);
            }

            if (productQuery?.MaxPrice != null)
            {
                query = query.Where(product => product.Price <= productQuery.MaxPrice);
            }

            if(productQuery?.CategoryId != null)
            {
                query = query.Where(product => product.CategoryId == productQuery.CategoryId);
            }

            if(productQuery?.Enabled != null)
            {
                query = query.Where(product => product.Enabled == productQuery.Enabled);
            }

            if (productQuery?.Keyword != null)
            {
                query = query.Where(product => product.Title.Contains(productQuery.Keyword) ||
                                                product.Brand.Contains(productQuery.Keyword) ||
                                                product.Category.Title.Contains(productQuery.Keyword) ||
                                                product.ShortDescription.Contains(productQuery.Keyword) ||
                                                product.Description.Contains(productQuery.Keyword));
                                
            }

            switch(productQuery.Order)
            {
                case ProductOrderBy.PriceAscending:
                    query = query.OrderBy(product => product.Price);
                    break;
                case ProductOrderBy.PriceDescending:
                    query = query.OrderByDescending(product => product.Price);
                    break;
                case ProductOrderBy.BrandAscending:
                    query = query.OrderBy(product => product.Brand);
                    break;
                case ProductOrderBy.BrandDescending:
                    query = query.OrderByDescending(product => product.Brand);
                    break;
                case ProductOrderBy.TitleAscending:
                    query = query.OrderBy(product => product.Title);
                    break;
                case ProductOrderBy.TitleDescending:
                    query = query.OrderByDescending(product => product.Title);
                    break;
                case ProductOrderBy.Relevance:
                    if (productQuery?.Keyword != null)
                    {
                        query = query.OrderBy(product => product.Title.Contains(productQuery.Keyword) ? 0 :
                                                        product.Category.Title.Contains(productQuery.Keyword) ? 1 :
                                                        product.Brand.Contains(productQuery.Keyword) ? 2 :
                                                        product.ShortDescription.Contains(productQuery.Keyword) ? 3 :
                                                        product.Description.Contains(productQuery.Keyword) ? 4 : 5);
                    }
                    break;
            }

            return query.Take(productQuery.Take).Skip(productQuery.Offset);
        }

        public Product CreateProduct(Product product)
        {
            this.db.Add(product);

            return product;
        }

        public Product UpdateProduct(Product product)
        {
            this.db.Update(product);

            return product;
        }

        public void DeleteProduct(Product product)
        {
            this.db.Remove(product);
        } 
    }
}
