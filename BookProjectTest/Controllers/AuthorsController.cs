using BookProjectTest.Dtos;
using BookProjectTest.Models;
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
    public class AuthorsController :Controller
    {
        private IAuthorRepository _authorRepository;
        private IBookRepository _bookRepository;
        public AuthorsController(IAuthorRepository authorRepository,IBookRepository bookRepository)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
        }

        //api/authors
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AuthorDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetAuthors()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var authors = _authorRepository.GetAuthors();
            var authorDto = new List<AuthorDto>();
            foreach (var author in authors)
            {
                authorDto.Add(new AuthorDto
                {
                    Id = author.Id,
                    FirstName = author.FirstName,
                    LastName = author.LastName
                });
            }
            return Ok(authorDto);
        }

        //api/authors/authorid
        [HttpGet("{authorId}", Name ="GetAuthor")]
        [ProducesResponseType(200, Type =typeof(AuthorDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult GetAuthor(int authorId)
        {
            if (!_authorRepository.AuthorExists(authorId))
                return NotFound();
            var author = _authorRepository.GetAuthor(authorId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var authorDto = new AuthorDto()
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName
            };
            return Ok(authorDto);
        }
        //api/authors/bookid/author
        //GetAuthorsOfABook
        [HttpGet("{bookId}/author")]

        [ProducesResponseType(200, Type = typeof(AuthorDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetAuthorsOfABook(int bookId)
        {
            if (!_bookRepository.BookExists(bookId))
                return NotFound();
            var authors = _authorRepository.GetAuthorsOfABook(bookId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var authorDto = new List<AuthorDto>();
            foreach (var author in authors)
            {
                authorDto.Add(new AuthorDto
                {
                    Id = author.Id,
                    FirstName = author.FirstName,
                    LastName =author.LastName
                });
            }
            return Ok(authorDto);
        }


        //api/authors/authorId/books

        [ProducesResponseType(200, Type = typeof(BookDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpGet("{authorId}/books")]
        public IActionResult GetBooksByAuthor(int authorId)
        {
            if (!_authorRepository.AuthorExists(authorId))
                return NotFound();
            var books = _authorRepository.GetBooksByAuthor(authorId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var bookDto = new List<BookDto>();
            foreach ( var book in books)
            {
                bookDto.Add(new BookDto
                {
                    Id = book.Id,
                    Isbn = book.Isbn,
                    DatePublished = book.DatePublished,
                    Title = book.Title
                });

            }
            return Ok(bookDto);
        }

        //create Author
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
       
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateAuthor([FromBody]Author authorToCreate)
        {
            if (authorToCreate == null)
                return BadRequest(ModelState);

            var author = _authorRepository.GetAuthors().
                Where(a => a.FirstName == authorToCreate.FirstName && a.LastName == authorToCreate.LastName).FirstOrDefault();
            if(author != null)
            {
                ModelState.AddModelError("", $"Author {authorToCreate.LastName} , {authorToCreate.LastName} already exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_authorRepository.CreateAuthor(author))
            {
                ModelState.AddModelError("", $"Something went wrong creating {authorToCreate.LastName} , {authorToCreate.LastName}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetAuthor", new { authorId = authorToCreate.Id }, authorToCreate);
        }
    }
}
