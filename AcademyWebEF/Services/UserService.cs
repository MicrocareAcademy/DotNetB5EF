using AcademyWebEF.BusinessEntities;

namespace AcademyWebEF.Services
{
    public class UserService
    {
        public User CreateUser(string userName, string password, string email, string role)
        {
            User userObj = new User
            {
                UserName = userName,
                Email = email,
                Password = password,
                Role = role
            };

            var dbContext = new AcademyDbContext();

            dbContext.Users.Add(userObj); // give this object to DBContext  to save the data in the database

            dbContext.SaveChanges();

            return userObj;
        }
    }
}
