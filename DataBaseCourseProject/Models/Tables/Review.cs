using System.ComponentModel.DataAnnotations;

namespace DataBaseCourseProject.Models.Tables
{
    public class Review : BaseTableEntity
    {
        public int? ProductId { get; set; }

        public int? UserId { get; set; }

        public int? Rating { get; set; }

        [StringLength(300)]
        public string Comments { get; set; }
    }
}