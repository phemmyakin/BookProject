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

        [HttpGet("{categoryId}", Name ="GetCategory")]
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

        //create category method
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateCategory([FromBody]Category categoryToCreate)
        {
            if (categoryToCreate == null)
                return BadRequest(ModelState);

            var category = _categoryRepository.GetCategories().
                Where(c => c.Name.Trim().ToLower() == categoryToCreate.Name.Trim().ToLower()).FirstOrDefault();
            if(category != null)
            {
                ModelState.AddModelError("", $"Category {categoryToCreate.Name} already exist");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_categoryRepository.CreateCategory(categoryToCreate))
            {
                ModelState.AddModelError("", $"Something went wrong saving {categoryToCreate.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetCategory", new { categoryId = categoryToCreate.Id},categoryToCreate);
        }


        //update category APi
        [HttpPut("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult UpdateCategory(int categoryId, [FromBody]Category categoryUpdateInfo)
        {
            if (categoryUpdateInfo == null)
                return BadRequest(ModelState);

            if(categoryId != categoryUpdateInfo.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_categoryRepository.CategoryExist(categoryId))
                return NotFound();

            if (_categoryRepository.IsDuplicateCategoryName(categoryId, categoryUpdateInfo.Name))
            {
                ModelState.AddModelError("", $"{categoryUpdateInfo.Name} category already exist");
                return StatusCode(422, ModelState);

            }

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_categoryRepository.UpdateCategory(categoryUpdateInfo))
            {
                ModelState.AddModelError("", $"Something went wrong updating {categoryUpdateInfo.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


        //delete method Api
        [HttpDelete("{categoryId}")]
        [ProducesResponseType(204)] //no content
        [ProducesResponseType(400)]  //badrequest
        [ProducesResponseType(404)]  //not found
        [ProducesResponseType(409)]   //conflict
        [ProducesResponseType(500)] 
        public IActionResult DeleteCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExist(categoryId))
                return NotFound();
            var categoryToDelete = _categoryRepository.GetCategory(categoryId);
            if(_categoryRepository.GetAllBooksForACategory(categoryId).Count()> 0)
            {
                ModelState.AddModelError("", $"Category {categoryToDelete.Name} cannot be deleted because it is used by at least one book");
                return StatusCode(409, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_categoryRepository.DeleteCategory(categoryToDelete))
            {
                ModelState.AddModelError("", $"Something went wrong deleting {categoryToDelete.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
