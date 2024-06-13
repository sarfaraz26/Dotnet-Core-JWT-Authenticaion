using API.DomainModels;
using API.DTO;

namespace API.Interfaces
{
    public interface ILibraryRepository
    {
        List<LibraryDomainModel> GetBooksWithAuthors();

        List<DropdownDomainModel> GetAuthors();

        BookDomainModel AddBook(BookDTO dto);

        string DeleteBook(Guid bookId);

        public String UpdateBook(BookDTO dto, Guid bookId);

    }
}
