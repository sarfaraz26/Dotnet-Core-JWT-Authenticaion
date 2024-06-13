using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Book
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "decimal(4,2)")]
        public decimal Price { get; set; }

        public Guid AuthorId { get; set; }

        public bool IsDeleted { get; set; } = false;

        public string CreatedBy { get; set; }

        public DateTime CreatedDtTm { get; set; }

        public DateTime ModifiedBy { get; set; }

        public DateTime ModifiedDtTm { get; set; }
    }
}
