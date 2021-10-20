using System.ComponentModel.DataAnnotations;

namespace Logeecom.DemoProjekat.PL.ViewModels.SubmitionModels
{
    public class CategorySubmitModel
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string Code { get; set; }

        [Required]
        [MaxLength(32)]
        public string Title { get; set; }

        [Required]
        [MaxLength(256)]
        public string Description { get; set; }

        public int? ParentId { get; set; }
    }
}
