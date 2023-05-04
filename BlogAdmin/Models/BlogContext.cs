using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace BlogAdmin.Models
{
    public class BlogContext:DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {

        }

        public DbSet<Author> Author { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Blog> Blog { get; set; }
    }
}
