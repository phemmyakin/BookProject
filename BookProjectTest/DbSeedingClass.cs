using BookProjectTest.Models;
using BookProjectTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookProjectTest
{
    public static class DbSeedingClass
    {
        public static void SeedDataContext(this BookDbContext context)
        {
            var booksAuthors = new List<BookAuthor>()
            {
                new BookAuthor()
                {
                    Book = new Book()
                    {
                        Isbn = "123",
                        Title = "Call Of Duty",
                        DatePublished = new DateTime(1903,1,1),
                        BookCategories = new List<BookCategory>()
                        {
                            new BookCategory { Category = new Category() { Name = "Action"}}
                        },
                        Reviews = new List<Review>()
                        {
                            new Review { HeadLine = "Awesome Book", ReviewText = "Reviewing Call of Duty and it is awesome beyond words", Rating = 5,
                             Reviewer = new Reviewer(){ FirstName = "John", LastName = "Smith" } },
                            new Review{ HeadLine = "Nice to read", ReviewText=" its a cool book for the evening", Rating = 4,
                            Reviewer= new Reviewer(){FirstName = "Raphael", LastName ="Akinnigbagbe"}}
                        }
                    },
                    Author = new Author()
                    {
                        FirstName = "Jack",
                        LastName = "Smith",
                        Country = new Country()
                        {
                            Name = "USA"
                        }
                    }
                },
                new BookAuthor()
                {
                    Book = new Book()
                    {
                        Isbn = "1234",
                        Title = "Winnetou",
                        DatePublished = new DateTime(1878,10,1),
                        BookCategories = new List<BookCategory>()
                        {
                            new BookCategory { Category = new Category() { Name = "Western"}}
                        },
                        Reviews = new List<Review>()
                        {
                            new Review { HeadLine = "Awesome Western Book", ReviewText = "Reviewing Winnetou and it is awesome book", Rating = 4,
                             Reviewer = new Reviewer(){ FirstName = "Frank", LastName = "Gnocci" } }
                        }
                    },
                    Author = new Author()
                    {
                        FirstName = "Karl",
                        LastName = "May",
                        Country = new Country()
                        {
                            Name = "Germany"
                        }
                    }
                },
                new BookAuthor()
                {
                    Book = new Book()
                    {
                        Isbn = "12345",
                        Title = "Pavols Best Book",
                        DatePublished = new DateTime(2019,2,2),
                        BookCategories = new List<BookCategory>()
                        {
                            new BookCategory { Category = new Category() { Name = "Educational"}},
                            new BookCategory { Category = new Category() { Name = "Computer Programming"}}
                        },
                        Reviews = new List<Review>()
                        {
                            new Review { HeadLine = "Awesome Programming Book", ReviewText = "Reviewing Pavols Best Book and it is awesome beyond words", Rating = 5,
                             Reviewer = new Reviewer(){ FirstName = "Pavol2", LastName = "Almasi2" } }
                        }
                    },
                    Author = new Author()
                    {
                        FirstName = "Pavol",
                        LastName = "Almasi",
                        Country = new Country()
                        {
                            Name = "Slovakia"
                        }
                    }
                },
                new BookAuthor()
                {
                    Book = new Book()
                    {
                        Isbn = "123456",
                        Title = "Three Musketeers",
                        DatePublished = new DateTime(2019,2,2),
                        BookCategories = new List<BookCategory>()
                        {
                            new BookCategory { Category = new Category() { Name = "Action"}},
                            new BookCategory { Category = new Category() { Name = "History"}}
                        }
                    },
                    Author = new Author()
                    {
                        FirstName = "Alexander",
                        LastName = "Dumas",
                        Country = new Country()
                        {
                            Name = "France"
                        }
                    }
                },
                new BookAuthor()
                {
                    Book = new Book()
                    {
                        Isbn = "1234567",
                        Title = "Big Romantic Book",
                        DatePublished = new DateTime(1879,3,2),
                        BookCategories = new List<BookCategory>()
                        {
                            new BookCategory { Category = new Category() { Name = "Romance"}}
                        },
                        Reviews = new List<Review>()
                        {
                            new Review { HeadLine = "Good Romantic Book", ReviewText = "This book made me cry a few times", Rating = 5,
                             Reviewer = new Reviewer(){ FirstName = "Allison", LastName = "Kutz" } },
                            new Review { HeadLine = "Horrible Romantic Book", ReviewText = "My wife made me read it and I hated it", Rating = 1,
                             Reviewer = new Reviewer(){ FirstName = "Kyle", LastName = "Kutz" } }
                        }
                    },
                    Author = new Author()
                    {
                        FirstName = "Anita",
                        LastName = "Powers",
                        Country = new Country()
                        {
                            Name = "Canada"
                        }
                    }
                },
                new BookAuthor()
                {
                    Book = new Book()
                    {
                        Isbn = "234987",
                        Title = "Things Fall Apart",
                        DatePublished = new DateTime(1964, 07,03),
                        BookCategories = new List<BookCategory>()
                        {
                            new BookCategory { Category = new Category(){ Name = "Politics"}}
                        },
                        Reviews = new List<Review>()
                        {
                            new Review { HeadLine = "Very nice Book", ReviewText = "its is a very good book for political students", Rating = 5,
                            Reviewer = new Reviewer(){ FirstName = " Seun", LastName = " Akin"}}
                        }

                    },
                    Author = new Author()
                    {
                        FirstName = "Wole",
                        LastName = "Soyinka",
                        Country = new Country()
                        {
                            Name = "Nigeria"
                        }
                    }
                },
                new BookAuthor()
                {
                    Book = new Book()
                    {
                        Isbn = "21390",
                        Title = "A dance in the Rain",
                        DatePublished = new DateTime(2002, 06, 09),
                        BookCategories = new List<BookCategory>()
                        {
                            new BookCategory{ Category = new Category(){Name = "Music"}}
                        },
                        Reviews = new List<Review>()
                        {
                            new Review {HeadLine = "Soul Touching", ReviewText = "A book for heart and soul", Rating = 4,
                            Reviewer = new Reviewer(){ FirstName = "Jack", LastName = "Robb"} }
                        }
                    },
                    Author = new Author()
                    {
                        FirstName = "Chris",
                        LastName = "Brown",
                        Country = new Country()
                        {
                            Name = "Bulgaria"
                        }
                    }
                }
            };

            context.BookAuthors.AddRange(booksAuthors);
            context.SaveChanges();

        }
    }
}

