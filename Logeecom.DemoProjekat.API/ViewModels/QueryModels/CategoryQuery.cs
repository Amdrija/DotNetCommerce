using System;
using System.ComponentModel.DataAnnotations;

namespace Logeecom.DemoProjekat.PL.ViewModels.QueryModels
{
    public class CategoryQuery
    {
        [Range(0, int.MaxValue)]
        public int Offset { get; set; } = 0;

        [Range(1, 100)]
        public int Take { get; set; } = 10;

        [MaxLength(32)]
        public string Code { get; set; }

        [MaxLength(32)]
        public string Title { get; set; }

        [MaxLength(256)]
        public string Description { get; set; }

        public int? ParentId { get; set; }
    }
}
