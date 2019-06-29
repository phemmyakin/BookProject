﻿using BookProjectTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookProjectTest.Services
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int categoryId);
        ICollection<Category> GetAllCategoriesForABook(int bookId);
        ICollection<Book> GetAllBooksForACategory(int categoryId);
        bool CategoryExist(int categoryId);
        bool IsDuplicateCategoryName(int categoryId, string categoryName);
    }
}
