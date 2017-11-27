using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore_DepenentEntities_Demo2.Data
{
    public class SampleDataContext: DbContext
    {
        public DbSet<UserData> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
			optionsBuilder.UseSqlServer(@"[YOUR-CONNECTION-STRING]");
		}
    }

    public class UserData
    {
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String MidName { get; set; }
        public String LastName { get; set; }
        public String UserName { get; set; }
        public String EmailAddress { get; set; }
        public DateTime BirthDate { get; set; }
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
