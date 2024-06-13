using API.DomainModels;
using API.DTO;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryRepository _libraryRepository;
        public LibraryController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Welcome to Books - Authors");
        }

        [HttpGet]
        [Route("Authenticate")]
        [Authorize]
        public IActionResult Verify()
        {
            return Ok("Authenticate Route");
        }

        [HttpGet]
        [Route("Admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminRoute()
        {
            return Ok("Admin");
        }

        [HttpGet]
        [Route("Non-Admin")]
        [Authorize(Roles = "Admin,Non-Admin")]
        public IActionResult NonAdminRoute()
        {
            return Ok("Non-Admin");
        }

        [HttpGet]
        [Route("GetBooksWithAuthors")]
        public IActionResult GetBooksWithAuthors()
        {
            try
            {
                List<LibraryDomainModel> data = _libraryRepository.GetBooksWithAuthors();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAuthors")]
        public IActionResult GetAuthors()
        {
            try
            {
                List<DropdownDomainModel> data = _libraryRepository.GetAuthors();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddBook")]
        public IActionResult AddBook(BookDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest("Please provide valid arguments");
                }
                else
                {
                    BookDomainModel data = _libraryRepository.AddBook(dto);

                    return Ok(data);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteBook/{bookId}")]
        public IActionResult DeleteBook(Guid bookId)
        {
            try
            {
                String msg = _libraryRepository.DeleteBook(bookId); 
                JsonResult res = new JsonResult(msg);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateBook/{bookId}")]
        public IActionResult UpdateBook([FromBody] BookDTO dto, Guid bookId)
        {
            try
            {
                String msg = _libraryRepository.UpdateBook(dto, bookId);
                JsonResult res = new JsonResult(msg);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
