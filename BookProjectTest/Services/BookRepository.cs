﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookProjectTest.Models;

namespace BookProjectTest.Services
{
    public class BookRepository : IBookRepository
    {
        private BookDbContext _bookContext;
        public BookRepository(BookDbContext bookContext)
        {
            _bookContext = bookContext;
        }
        public bool BookExists(int bookId)
        {
            return _bookContext.Books.Any(b => b.Id == bookId);
        }

        public bool BookExists(string bookIsbn)
        {
            return _bookContext.Books.Any(b => b.Isbn == bookIsbn);
        }

        public Book GetBook(int bookId)
        {
            return _bookContext.Books.Where(b => b.Id == bookId).FirstOrDefault();
        }

        public Book GetBook(string bookIsbn)
        {
            return _bookContext.Books.Where(b => b.Isbn == bookIsbn).FirstOrDefault();
        }

        public decimal GetBookRating(int bookId)
        {
            var reviews = _bookContext.Reviews.Where(b => b.Book.Id == bookId);
            if (reviews.Count() <= 0)
                return 0;
            return ((decimal)reviews.Sum(r => r.Rating) / reviews.Count());
        }

        public ICollection<Book> GetBooks()
        {
            return _bookContext.Books.OrderBy(b => b.Title).ToList();
        }

        public bool IsDuplicateIsbn(int bookId, string bookIsbn)
        {
            var book = _bookContext.Books.Where(b => b.Isbn.Trim().ToUpper() == bookIsbn.Trim().ToUpper() && b.Id != bookId).FirstOrDefault();

            return book == null ? false : true;
        }
    }
}
