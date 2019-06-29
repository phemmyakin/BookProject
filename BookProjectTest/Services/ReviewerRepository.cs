using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookProjectTest.Models;

namespace BookProjectTest.Services
{
    public class ReviewerRepository : IReviewerRepository
    {
        private BookDbContext _reviewerContext;
        public ReviewerRepository(BookDbContext reviewerContext)
        {
            _reviewerContext = reviewerContext;
        }
        public Reviewer GetReviewer(int reviewerId)
        {
            return _reviewerContext.Reviewers.Where(r => r.Id == reviewerId).FirstOrDefault();
        }

        public Reviewer GetReviewerOfAReview(int reviewId)
        {
            return _reviewerContext.Reviews.Where(r => r.Id == reviewId).Select(r => r.Reviewer).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _reviewerContext.Reviewers.OrderBy(r => r.FirstName).ToList();
        }

        public ICollection<Review> GetReviewsByReviewer(int reveiwerId)
        {
            return _reviewerContext.Reviews.Where(r => r.Reviewer.Id == reveiwerId).OrderBy(c => c.Id).ToList();
        }

        public bool ReviewerExists(int reviewerId)
        {
            return _reviewerContext.Reviewers.Any(r => r.Id == reviewerId);
        }
    }
}
