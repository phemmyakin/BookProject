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
    public class CategoriesController : Controller
    {
        private ICategoryRepository _categoryRepository;
        private IBookRepository _bookRepository;
        public CategoriesController(ICategoryRepository categoryRepository, IBookRepository bookRepository)
        {
            _categoryRepository = categoryRepository;
            _bookRepository = bookRepository;
        }
        //api/categories
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetCategories()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var categories = _categoryRepository.GetCategories().ToList();
            var categoryDto = new List<CategoryDto>();

            foreach (var category in categories)
            {
                categoryDto.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name
                });
            }
            return Ok(categoryDto);
        }



        //api/categories/categoryid

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExist(categoryId))
            {
                return NotFound();

            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var category = _categoryRepository.GetCategory(categoryId);
            var categoryDto = new CategoryDto()
            {
                Id = category.Id,
                Name = category.Name

            };
            return Ok(categoryDto);
        }



        //api/categories/books/bookId
        [HttpGet("books/{bookId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetAllCategoriesForABook(int bookId)
        {
            //TODO validate book exist
            if (!_bookRepository.BookExists(bookId))
                return NotFound();
            var categories = _categoryRepository.GetAllCategoriesForABook(bookId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryDto = new List<CategoryDto>();
            foreach (var category in categories)
            {
                categoryDto.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name
                });
            }

            return Ok(categoryDto);


        }

        //api/categories/categoryId/books
        [HttpGet("{categoryId}/books")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetAllBookForACategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExist(categoryId))
                return NotFound();

            var books = _categoryRepository.GetAllBooksForACategory(categoryId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookDto = new List<BookDto>();
            foreach (var book in books)
            {
                bookDto.Add(new BookDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    Isbn = book.Isbn,
                    DatePublished = book.DatePublished
                });
            }

            return Ok(bookDto);


        }

    }
}
