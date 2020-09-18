﻿// Copyright (c) 2020 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System.Linq;
using BookApp.Domain.Books;
using BookApp.ServiceLayer.DefaultSql.Books.Dtos;

namespace BookApp.ServiceLayer.DefaultSql.Books.QueryObjects
{
    public static class BookListDtoSelect
    {
        public static IQueryable<BookListDto> 
            MapBookToDto(this IQueryable<Book> books) 
        {
            return books.Select(p      => new BookListDto
            {
                BookId                 = p.BookId, 
                Title                  = p.Title, 
                PublishedOn            = p.PublishedOn, 
                EstimatedDate          = p.EstimatedDate,
                OrgPrice               = p.OrgPrice, 
                ActualPrice            = p.ActualPrice, 
                PromotionText          = p.PromotionalText,
                AuthorsOrdered         = string.Join(", ",
                    p.AuthorsLink
                        .OrderBy(q     => q.Order)
                        .Select(q      => q.Author.Name)),
                TagStrings             = p.Tags.Select(x => x.TagId).ToArray(),
                ReviewsCount           = p.Reviews.Count(),
                ReviewsAverageVotes    =
                    p.Reviews.Select(y =>
                        (double?)y.NumStars).Average(),
                ManningBookUrl         = p.ManningBookUrl
            });
        }

        /*********************************************************
        #A This method takes in IQueryable<Book> and returns IQueryable<BookListDto>
        #B These are simple copies of existing columns in the Books table
        #C This calculates the selling price, which is the normal price, or the promotion price if that relationship exists 
        #D The PromotionalText depends on whether a PriceOffer exists for this book
        #E This obtains an array of Authors' names, in the right order. We are using a Client vs. Server evaluation as we want the author's names combined into one string
        #F We need to calculate how many reviews there are
        #G To get EF Core to turn the LINQ average into the SQL AVG command I need to cast the NumStars to (double?)
        * *******************************************************/
    }
}