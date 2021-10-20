namespace Logeecom.DemoProjekat.PL.ViewModels.RepsonseModels
{
    public class CategoryResponseModel
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int? ParentId { get; set; }
    }
}
