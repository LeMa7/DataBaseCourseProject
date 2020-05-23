namespace DataBaseCourseProject.Models.Tables
{
    public class Subcategory : BaseTableEntity
    {
        public string Name { get; set; }

        public int? CategoryId { get; set; }
    }
}