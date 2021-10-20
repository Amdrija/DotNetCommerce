namespace Logeecom.DemoProjekat.DAL.QueryModels
{
    public enum ProductOrderBy
    {
        PriceAscending,
        PriceDescending,
        BrandAscending,
        BrandDescending,
        TitleAscending,
        TitleDescending,
        Relevance
    }
    
    public class ProductQuery
    {
        public int Offset { get; set; }

        public int Take { get; set; }

        public ProductOrderBy Order { get; set; }

        public string Keyword { get; set; }

        public int? MinPrice { get; set; }

        public int? MaxPrice { get; set; }

        public int? CategoryId { get; set; }

        public bool? Enabled { get; set; }
    }
}
