﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookProjectTest.Models;

namespace BookProjectTest.Services
{
    public class ReviewRepository : IReviewRepository
    {
        private BookDbContext _reviewContext;
        public ReviewRepository(BookDbContext reviewContext)
        {
            _reviewContext = reviewContext;
        }
        public Book GetBookOfAReview(int reviewId)
        {
            return _reviewContext.Reviews.Where(r => r.Id == reviewId).Select(b => b.Book).FirstOrDefault();
        }

        public Review GetReview(int reviewId)
        {
            return _reviewContext.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return _reviewContext.Reviews.OrderBy(r => r.Rating).ToList();
        }

        public ICollection<Review> GetReviewsOfABook(int bookId)
        {
            return _reviewContext.Reviews.Where(r => r.Book.Id == bookId).ToList();
        }

        public bool ReviewExists(int reviewId)
        {
            return _reviewContext.Reviews.Any(r => r.Id == reviewId);
        }
    }
}
