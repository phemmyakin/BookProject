using BookProjectTest.Dtos;
using BookProjectTest.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookProjectTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        private IBookRepository _bookRepository;
        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        //api/books
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookDto>))]
        [ProducesResponseType(404)]
        public IActionResult GetBooks()
        {
          
            var books = _bookRepository.GetBooks();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var booksDto = new List<BookDto>();
            foreach (var book in books)
            {
                booksDto.Add(new BookDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    DatePublished = book.DatePublished,
                    Isbn = book.Isbn
                });
            }

            return Ok(booksDto);
        }


        //api/books/bookId
        [HttpGet("{bookId}")]
        [ProducesResponseType(200, Type = typeof(BookDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetBook(int bookId)
        {
            if (!_bookRepository.BookExists(bookId))
                return NotFound();
            var book = _bookRepository.GetBook(bookId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var bookDto = new BookDto()
            {
                Id = book.Id,
                Title = book.Title,
                DatePublished = book.DatePublished,
                Isbn = book.Isbn
            };

            return Ok(bookDto);
        }


        //api/books/isbn/bookIsbn
        [HttpGet("isbn/{bookIsbn}")]
        [ProducesResponseType(200, Type = typeof(BookDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetBook(string bookIsbn)
        {
            if (!_bookRepository.BookExists(bookIsbn))
                return NotFound();
            var book = _bookRepository.GetBook(bookIsbn);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var bookDto = new BookDto()
            {
                Id = book.Id,
                Title = book.Title,
                DatePublished = book.DatePublished,
                Isbn = book.Isbn
            };

            return Ok(bookDto);
        }


        //api/books/bookId/rating
        [HttpGet("{bookId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetBookRating(int bookId)
        {
            if (!_bookRepository.BookExists(bookId))
                return NotFound();
            var rating = _bookRepository.GetBookRating(bookId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(rating);
        }


    }
}
