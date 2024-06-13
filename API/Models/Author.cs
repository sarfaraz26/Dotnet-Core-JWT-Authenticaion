using System.Security.Cryptography;

namespace API.Models
{
    public class Author
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public bool IsWorking { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDtTm { get; set; }

        public DateTime ModifiedBy { get; set; }

        public DateTime ModifiedDtTm { get; set; }

    }
}
