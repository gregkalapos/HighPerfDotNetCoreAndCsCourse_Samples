using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFQueries_Demo1.Data
{
    public class SampleDataContext: DbContext
    {
        public DbSet<UserData> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserData>()
                .Property(b => b.UserName)
                .HasMaxLength(350);
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
			optionsBuilder.UseSqlServer(@"[YOUR-CONNECTION-STRING]");
        }
    }


    public class UserData
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public String FirstName { get; set; }
        [MaxLength(250)]
        public String MidName { get; set; }
        [MaxLength(250)]
        public String LastName { get; set; }
        [MaxLength(350)]
        public String UserName { get; set; }
        [MaxLength(350)]
        public String EmailAddress { get; set; }
        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }
        [MaxLength(1000)]
        public String Address { get; set; }
        public int ZipCode { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime RegistrationDate { get; set; }
        public List<Comment> Comments { get; set; }       
    }

    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public UserData Author { get; set; }
    }
}
