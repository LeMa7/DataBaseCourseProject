using System.ComponentModel.DataAnnotations;

namespace DataBaseCourseProject.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int? ProductId { get; set; }

        public int? UserId { get; set; }

        public int? Rating { get; set; }

        [StringLength(300)]
        public string Comments { get; set; }
    }
}