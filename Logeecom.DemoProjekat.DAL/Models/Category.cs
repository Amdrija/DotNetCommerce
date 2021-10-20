using System.Collections.Generic;

namespace Logeecom.DemoProjekat.DAL.Models
{
    public class Category
    {
        public Category()
        {
            this.Subcategories = new List<Category>();
            this.Products = new List<Product>();
        }

        public int Id { get; set; }
        
        public string Code { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int? ParentId { get; set; }

        public virtual Category ParentCategory { get; set; }

        public virtual List<Category> Subcategories { get; private set; }

        public virtual List<Product> Products { get; private set; }
    }
}
