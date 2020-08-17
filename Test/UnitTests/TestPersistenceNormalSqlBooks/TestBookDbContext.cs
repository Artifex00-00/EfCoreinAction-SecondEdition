﻿// Copyright (c) 2020 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using BookApp.Domain.Books;
using BookApp.Persistence.EfCoreSql.Books;
using Test.TestHelpers;
using TestSupport.EfHelpers;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace Test.UnitTests.TestPersistenceNormalSqlBooks
{
    public class TestBookDbContext
    {
        [Fact]
        public void TestBookDbContextAddFourBooksOk()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<BookDbContext>();
            using var context = new BookDbContext(options);
            context.Database.EnsureCreated();

            //ATTEMPT
            context.SeedDatabaseFourBooks();

            //VERIFY
            context.Books.Count().ShouldEqual(4);
            context.Authors.Count().ShouldEqual(3);
            context.Set<Review>().Count().ShouldEqual(2);
        }

        [Fact]
        public void TestBookDbContextBookWithTagsOk()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<BookDbContext>();
            using var context = new BookDbContext(options);
            context.Database.EnsureCreated();

            //ATTEMPT
            var status = Book.CreateBook("title", null, new DateTime(2020,1,1), 
                "Manning", 123, "imageRul", 
                new List<Author>{new Author("author1", null)},
                new List<Tag>{new Tag("tag1"), new Tag("tag2")});
            context.Add(status.Result);
            context.SaveChanges();

            //VERIFY
            status.IsValid.ShouldBeTrue(status.GetAllErrors());
            context.Books.Count().ShouldEqual(1);
            context.Authors.Count().ShouldEqual(1);
            context.Tags.Count().ShouldEqual(2);
        }
    }
}