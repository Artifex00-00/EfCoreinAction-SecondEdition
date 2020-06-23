﻿// Copyright (c) 2020 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore;

namespace Test.Chapter09Listings.MoveColumns
{
    public class MoveColumnsDbContext : DbContext
    {
        public MoveColumnsDbContext(
            DbContextOptions<MoveColumnsDbContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }

        //public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new { UserId = 123, Name = "Jack", Street = "Jake Street", City = "Jack City"},
                new { UserId = 456, Name = "Jill", Street = "Jill Street", City = "Jill City"}
            );
        }

        /**********************************************************
         To create a migration I had to
        1. Set the startup project to Test
        2. In PMC type:  Add-Migration Initial -context MoveColumnsDbContext -OutputDir Chapter09Listings/MoveColumns/Migrations
         **********************************************************/

    }
}