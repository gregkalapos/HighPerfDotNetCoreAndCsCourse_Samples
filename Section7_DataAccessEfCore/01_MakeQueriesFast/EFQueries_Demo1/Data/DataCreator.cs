using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFQueries_Demo1.Data
{
    /// <summary>
    /// Utility class to fill the database
    /// </summary>
    public static class DataCreator
    {
        /// <summary>
        /// Fills the datacontext with random items
        /// </summary>
        /// <param name="sampleDataContext">The datacontext to fill</param>
        /// <param name="numberOfUsers">The number of random users that the method generates. With this you control the number of rows that the method generates in the users table</param>
        /// <param name="numberOfComments">the number of random comments that the method generates. With this you control the number of rows that the method generates in the comments table</param>
        public static void FillDb(SampleDataContext sampleDataContext, int numberOfUsers, int numberOfComments)
        {
            RandomNameGeneratorLibrary.PersonNameGenerator personGenerator = new RandomNameGeneratorLibrary.PersonNameGenerator();
            RandomNameGeneratorLibrary.PlaceNameGenerator placeGenerator = new RandomNameGeneratorLibrary.PlaceNameGenerator();
            Random rnd = new Random();

            for (int i = 0; i < numberOfUsers; i++)
            {
                var firstName = personGenerator.GenerateRandomFirstName();
                var lastLogin = DateTime.Now.AddDays(-rnd.Next(0, 5 * 365));
                UserData user = new UserData
                {
                    Address = "test1234",
                    BirthDate = DateTime.Now.AddYears(-(rnd.Next(17, 90))).AddDays(-(rnd.Next(1, 365))).Date,
                    EmailAddress = firstName + "@testmail.com",
                    UserName = firstName + i,
                    FirstName = firstName,
                    LastName = personGenerator.GenerateRandomLastName(),
                    MidName = rnd.Next(2) == 0 ? String.Empty : personGenerator.GenerateRandomFirstName(),
                    LastLogin = lastLogin,
                    RegistrationDate = lastLogin.AddDays(-(rnd.Next(0, 365))),
                    ZipCode = rnd.Next(0, 10000)
                };

                sampleDataContext.Users.Add(user);
            }

            sampleDataContext.SaveChanges();

            var authors = sampleDataContext.Users.ToList(); //need ToList, since ElementAt is currently not supported

            Comment comment = new Comment();


            for (int i = 0; i < numberOfComments; i++)
            {
                var author = authors.ElementAt(rnd.Next(0, authors.Count()));

                comment = new Comment()
                {
                    Author = author,
                    Created = author.LastLogin.AddDays(-rnd.Next(0, 150)),
                    Text = "comment text " + i
                };

                sampleDataContext.Comments.Add(comment);

                if (i % 100_000 == 0)
                {
                    sampleDataContext.SaveChanges();
                }
            }

            sampleDataContext.SaveChanges();
        }
    }
}
