using System.ComponentModel.DataAnnotations.Schema;

namespace API.DomainModels
{
    public class BookDomainModel
    {
        public Guid Id { get; set; } 

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public Guid AuthorId { get; set; }

        public bool IsDeleted { get; set; }

        public string Message { get; set; }

        public BookDomainModel()
        {
                
        }
    }
}
