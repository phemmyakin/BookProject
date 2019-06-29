using BookProjectTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookProjectTest.Services
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewer(int reviewerId);
        ICollection<Review> GetReviewsByReviewer(int reveiwerId);
        Reviewer GetReviewerOfAReview(int reviewId);
        bool ReviewerExists (int reviewerId);
    }
}
