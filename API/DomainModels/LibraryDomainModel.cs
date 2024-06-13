namespace API.DomainModels
{
    public class LibraryDomainModel
    {
        public Guid BookID { get; set; }
        public string BookName { get; set; }
        public decimal BookPrice { get; set; }
        public string BookDescription { get; set; } 

        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorAddress { get; set; }
        public bool IsDeleted { get; set; }



        public LibraryDomainModel()
        {
                
        }
    }
}
