using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace DAL.DBContext
{

    public class CustomDbContext : DbContext
    {
        public CustomDbContext(DbContextOptions<CustomDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}

