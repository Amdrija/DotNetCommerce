using System;
using System.ComponentModel.DataAnnotations;

namespace Logeecom.DemoProjekat.PL.ViewModels.QueryModels
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
        [Range(0, int.MaxValue)]
        public int Offset { get; set; } = 0;

        [Range(1, 100)]
        public int Take { get; set; } = 10;

        public ProductOrderBy Order { get; set; }

        [MaxLength(512)]
        public string Keyword { get; set; }

        [Range(0, int.MaxValue)]
        public int? MinPrice { get; set; }

        [Range(1, int.MaxValue)]
        public int? MaxPrice { get; set; }

        [Range(1, int.MaxValue)]
        public int? CategoryId { get; set; }

        public bool? Enabled { get; set; }
    }
}
