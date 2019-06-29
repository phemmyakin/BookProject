﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookProjectTest.Models;

namespace BookProjectTest.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private BookDbContext _categoryContext;
        public CategoryRepository(BookDbContext categoryContext)
        {
            _categoryContext = categoryContext;
        }
        public bool CategoryExist(int categoryId)
        {
            return _categoryContext.Categories.Any(c => c.Id == categoryId);
        }

        public ICollection<Book> GetAllBooksForACategory(int categoryId)
        {
            return _categoryContext.BookCategories.Where(bc => bc.CategoryId == categoryId).Select(b => b.Book).ToList();
        }

        public ICollection<Category> GetCategories()
        {
            return _categoryContext.Categories.OrderBy(c => c.Name).ToList();
        }

        public ICollection<Category> GetAllCategoriesForABook(int bookId)
        {
            return _categoryContext.BookCategories.Where(bc => bc.BookId == bookId).Select(c => c.Category).ToList();
        }


        public Category GetCategory(int categoryId)
        {
            return _categoryContext.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
        }

        public bool IsDuplicateCategoryName(int categoryId, string categoryName)
        {
            var category = _categoryContext.Categories.Where(c => c.Name.Trim().ToUpper() == categoryName.Trim().ToUpper() && c.Id != categoryId).FirstOrDefault();
            return category == null ? false : true;
        }

        public bool CreateCategory(Category category)
        {
            _categoryContext.Categories.Add(category);
            return Save();
        }

        public bool UpdateCategory(Category category)
        {
            _categoryContext.Categories.Update(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _categoryContext.Categories.Remove(category);
            return Save();

        }

        public bool Save()
        {
            var saved = _categoryContext.SaveChanges();
            return saved >=0 ? true : false;
        }
    }
}
