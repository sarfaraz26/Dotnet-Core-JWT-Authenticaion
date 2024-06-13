namespace API.DTO
{
    public class BookDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string BookName { get; set; }
        public string BookDescription { get; set; }
        public decimal BookPrice { get; set; }
        public Guid AuthorId { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; } = "Admin";
        public DateTime CreatedDtTm { get; set; } = DateTime.Now;
        public DateTime ModifiedBy { get; set; } = DateTime.Now;
        public DateTime ModifiedDtTm { get; set; } = DateTime.Now;

    }
}
