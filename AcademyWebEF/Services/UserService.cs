using AcademyWebEF.BusinessEntities;

namespace AcademyWebEF.Services
{
    public class UserService
    {
        private readonly AcademyDbContext _dbContext;
        public UserService()
        {
            _dbContext = new AcademyDbContext();
        }

        public User CreateUser(string userName, string password, string email, string role)
        {
            User userObj = new User
            {
                UserName = userName,
                Email = email,
                Password = password,
                Role = role
            };

            _dbContext.Users.Add(userObj); // give this object to DBContext  to save the data in the database

            _dbContext.SaveChanges();

            return userObj;
        }
    }
}
