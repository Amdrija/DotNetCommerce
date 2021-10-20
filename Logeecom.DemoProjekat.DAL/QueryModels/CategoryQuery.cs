namespace Logeecom.DemoProjekat.DAL.QueryModels
{

    public class CategoryQuery
    {
        public int Take { get; set; }

        public int Offset { get; set; }

        public string Code { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int? ParentId { get; set; }
    }
}
