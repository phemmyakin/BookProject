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
    public class ReviewsController : Controller
    {
        private IReviewRepository _reviewRepository;
        private IBookRepository _bookRepository;
        public ReviewsController(IReviewRepository reviewRepository, IBookRepository bookRepository)
        {
            _reviewRepository = reviewRepository;
            _bookRepository = bookRepository;

        }

        //api/reviews
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviews()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var reviews = _reviewRepository.GetReviews();
            var reviewsDto = new List<ReviewDto>();
            foreach (var review in reviews)
            {
                reviewsDto.Add(new ReviewDto
                {
                    Id = review.Id,
                    HeadLine = review.HeadLine,
                    Rating = review.Rating,
                    ReviewText = review.ReviewText
                });

            }
            return Ok(reviewsDto);
        }

        //api/reviews/reviewId
        [HttpGet("{reviewId}", Name = "GetReview")]
        [ProducesResponseType(200, Type = typeof(ReviewDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();
            var review = _reviewRepository.GetReview(reviewId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var reviewDto = new ReviewDto()
            {
                HeadLine = review.HeadLine,
                Id = review.Id,
                Rating = review.Rating,
                ReviewText = review.ReviewText
            };
            return Ok(reviewDto);
        }


        //api/reviews/reviewid/book
        [HttpGet("{reviewId}/book")]
        [ProducesResponseType(200, Type = typeof(BookDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetBookOfAReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();
            var book = _reviewRepository.GetBookOfAReview(reviewId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var bookDto = new BookDto()
            {
                Id = book.Id,
                Title = book.Title,
                Isbn = book.Isbn,
                DatePublished = book.DatePublished
            };
            return Ok(bookDto);
        }

        //GetReviewsOfABook
        //api/reviews/bookId/review
        [HttpGet("{bookId}/review")]
        [ProducesResponseType(200, Type = typeof (IEnumerable<ReviewDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetReviewsOfABook(int bookId)
        {
            if (!_bookRepository.BookExists(bookId))
                return NotFound();
            var reviews = _reviewRepository.GetReviewsOfABook(bookId);
                if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var reviewDto = new List<ReviewDto>();
            foreach (var review in reviews)
            {
                reviewDto.Add(new ReviewDto
                {
                    Id = review.Id,
                    HeadLine = review.HeadLine,
                    Rating = review.Rating,
                    ReviewText = review.ReviewText
                });
            }
            return Ok(reviewDto);
        }

        //route==api/review
        //create review
        [HttpPost]
        public IActionResult CreateReview([FromBody]Review reviewToCreate)
        {
            if (reviewToCreate == null)
                return BadRequest(ModelState);
            var review = _reviewRepository.GetReviews().
                Where(r => r.HeadLine.Trim().ToLower() == reviewToCreate.HeadLine.Trim().ToLower()
                || r.ReviewText.Trim().ToLower() == reviewToCreate.ReviewText.Trim().ToLower()).FirstOrDefault();

            if(review != null)
            {
                ModelState.AddModelError("", $"Headline or review text already exist ");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.CreateReview(reviewToCreate))
            {
                ModelState.AddModelError("", $"Soemthing Went wrong creating this review");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetReview", new { reviewId = reviewToCreate.Id }, reviewToCreate);

        }


        //update review
        [HttpPut]
        public IActionResult UpdateReview(int reviewId, [FromBody]Review updateReviewInfo)
        {
            if (updateReviewInfo == null)
                return BadRequest(ModelState);

            if (reviewId != updateReviewInfo.Id)
                return BadRequest(ModelState);

            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.UpdateReview(updateReviewInfo))
            {
                ModelState.AddModelError("", $"Something went wrong updating");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


    }
}
