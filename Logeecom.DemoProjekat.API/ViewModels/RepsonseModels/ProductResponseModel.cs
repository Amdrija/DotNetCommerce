namespace Logeecom.DemoProjekat.PL.ViewModels.RepsonseModels
{
    public class ProductResponseModel
    {
        public int Id { get; set; }

        public string SKU { get; set; }

        public string Title { get; set; }

        public string Brand { get; set; }

        public int Price { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public bool Enabled { get; set; }

        public bool Featured { get; set; }

        public int ViewCount { get; set; }

        public int CategoryId { get; set; }
    }
}
