using System.ComponentModel.DataAnnotations;

namespace Logeecom.DemoProjekat.PL.ViewModels.SubmitionModels
{
    public class ProductSubmitionModel
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string SKU { get; set; }
        
        [Required]
        [MaxLength(32)]
        public string Title { get; set; }
        
        [Required]
        [MaxLength(32)]
        public string Brand { get; set; }
        
        [Required]
        [Range(0, int.MaxValue)]
        public int Price { get; set; }
        
        [Required]
        [MaxLength(256)]
        public string ShortDescription { get; set; }
        
        [Required]
        [MaxLength(512)]
        public string Description { get; set; }
        
        [Required]
        [MaxLength(128)]
        public string Image { get; set; }
        
        [Required]
        public bool Enabled { get; set; }
        
        [Required]
        public bool Featured { get; set; }
        
        [Required]
        public int CategoryId { get; set; }
    }
}
