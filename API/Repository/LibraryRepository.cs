using API.Data;
using API.DomainModels;
using API.DTO;
using API.Interfaces;
using API.Models;
using Microsoft.VisualBasic;
using System.Xml;

namespace API.Repository
{
    public class LibraryRepository : ILibraryRepository
    {
        private readonly AppDbContext _context;

        public LibraryRepository(AppDbContext context)
        {
            _context = context;
        }

        public BookDomainModel AddBook(BookDTO dto)
        {
            try
            {
                Book obj = DtoToModelMapper(dto);

                Book addedObj = _context.Books.Add(obj).Entity;
                int output = _context.SaveChanges();

                BookDomainModel data = ModelToDmMapper(addedObj);

                if (output == 1)
                    data.Message = "Added successfully";
                else
                    data.Message = "Not added";

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DropdownDomainModel> GetAuthors()
        {
            try
            {
                var query = (from a in _context.Authors
                             select new DropdownDomainModel()
                             {
                                 Display = a.Name,
                                 Value = a.ID
                             }).ToList();

                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LibraryDomainModel> GetBooksWithAuthors()
        {
            try
            {
                var query = (from a in _context.Authors
                             join b in _context.Books
                             on a.ID equals b.AuthorId
                             //where b.IsDeleted == false
                             select new LibraryDomainModel()
                             {
                                 BookID = b.Id,
                                 BookName = b.Name,
                                 BookPrice = b.Price,
                                 BookDescription = b.Description,
                                 AuthorName = a.Name,
                                 AuthorId = a.ID,
                                 AuthorAddress = a.Address,
                                 IsDeleted = b.IsDeleted
                             })
                             .ToList();

                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string DeleteBook(Guid bookId)
        {
            try
            {
                Book foundBook = _context.Books.ToList().Find(book => book.Id == bookId);
                string msg = String.Empty;

                if (foundBook != null)
                {
                    foundBook.IsDeleted = true;
                    int output = _context.SaveChanges();

                    msg = output == 1 ? "Deleted Successfully" : "Not Deleted Successfully";
                }
                else
                {
                    msg = "Please provide correct GUID";
                }

                return msg;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public String UpdateBook(BookDTO dto, Guid bookId)
        {
            Book foundBook = _context.Books.ToList().Find(book => book.Id == bookId);
            string msg = String.Empty;

            if (foundBook != null)
            {
                foundBook.Name = dto.BookName;
                foundBook.Description = dto.BookDescription;
                foundBook.Price = dto.BookPrice;
                foundBook.AuthorId = dto.AuthorId;
                foundBook.IsDeleted = dto.IsDeleted;

                int output = _context.SaveChanges();

                msg = output == 1 ? "Updated Successfully" : "Not Updated Successfully";
            }
            else
            {
                msg = "Please provide correct GUID";
            }

            return msg;
        }

        #region Mapper Methods
        public Book DtoToModelMapper(BookDTO dto)
        {
            if (dto == null)
                return new Book();
            else
            {
                Book book = new()
                {
                    Id = dto.Id,
                    Name = dto.BookName,
                    Description = dto.BookDescription,
                    Price = dto.BookPrice,
                    AuthorId = dto.AuthorId,
                    IsDeleted = dto.IsDeleted,
                    CreatedBy = dto.CreatedBy,
                    CreatedDtTm = dto.CreatedDtTm,
                    ModifiedBy = dto.ModifiedBy,
                    ModifiedDtTm = dto.ModifiedDtTm
                };

                return book;
            }
        }

        public BookDomainModel ModelToDmMapper(Book book)
        {
            if (book == null)
                return new BookDomainModel();
            else
            {
                return new BookDomainModel
                {
                    Id = book.Id,
                    Name = book.Name,
                    Description = book.Description,
                    Price = book.Price,
                    AuthorId = book.AuthorId,
                    IsDeleted = book.IsDeleted,
                };
            }
        }
        #endregion
    }
}
