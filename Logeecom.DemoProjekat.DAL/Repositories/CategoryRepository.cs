using System.Collections.Generic;
using System.Linq;
using Logeecom.DemoProjekat.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Logeecom.DemoProjekat.DAL.QueryModels;
using Microsoft.Data.SqlClient;
using Logeecom.DemoProjekat.Exceptions;

namespace Logeecom.DemoProjekat.DAL.Repositories
{
    public class CategoryRepository
    {
        private readonly DemoDbContext db;
        private const int DEFAULT_ROOT_PARENT_ID = 0;

        public CategoryRepository(DemoDbContext db)
        {
            this.db = db;
        }

        public int GetCategoryCount()
        {
            return this.db.Categories.Count();
        }

        public bool CategoryExsists(int id)
        {
            return this.db.Categories.Where(category => category.Id == id).Any();
        }

        public bool CategoryCodeExsists(string code, int? id = null)
        {
            return this.db.Categories.Where(category => category.Code == code && category.Id != id).Any();
        }

        public IEnumerable<Category> GetCategories(CategoryQuery categoryQuery)
        {
            IQueryable<Category> query = this.db.Categories.AsNoTrackingWithIdentityResolution();

            if (categoryQuery?.Code != null)
            {
                query = query.Where(category => category.Code == categoryQuery.Code);
            }

            if (categoryQuery?.Title != null)
            {
                query = query.Where(category => category.Title.Contains(categoryQuery.Title));
            }

            if (categoryQuery?.Description != null)
            {
                query = query.Where(category => category.Description.Contains(categoryQuery.Description));
            }

            if (categoryQuery?.ParentId != null)
            {
                categoryQuery.ParentId = categoryQuery.ParentId == DEFAULT_ROOT_PARENT_ID ? null : categoryQuery.ParentId;
                query = query.Where(category => category.ParentId == categoryQuery.ParentId);
            }
            
            return query.Skip(categoryQuery.Offset).Take(categoryQuery.Take);
        }

        public Category GetCategory(int id)
        {
            return this.db.Categories.Find(id);
        }

        public Category CreateCategory(Category category)
        {
            this.db.Categories.Add(category);

            return category;
        }

        public Category UpdateCategory(Category category)
        {
            this.db.Categories.Update(category);

            return category;
        }

        public void DeleteCategory(Category category)
        {
            this.DeleteSubcategories(category);
        }

        public void DeleteSubcategories(Category category)
        {
            // For some reason, EF doesn't want to Load either category.Subcategories
            // or with include, so I am doing it manually here.
            List<Category> subcategories = this.db.Categories.Where(cat => cat.ParentId == category.Id).ToList<Category>();

            foreach(Category subcategory in subcategories)
            {
                this.DeleteSubcategories(subcategory);
            }

            this.db.Remove(category);
        }
    }

   
}
