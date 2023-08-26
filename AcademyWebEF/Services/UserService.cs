using AcademyWebEF.BusinessEntities;
using AcademyWebEF.Interfaces;
using AcademyWebEF.SMS;

namespace AcademyWebEF.Services
{
    public class UserService : IUserService
    {
        private readonly AcademyDbContext _dbContext;
        //private readonly JioService _jioService;
        //private readonly AirtelService _airtelService;

        private readonly ISmsService _smsService;

        public UserService()
        {
            _dbContext = new AcademyDbContext();
            //_jioService = new JioService();
            //_airtelService = new AirtelService();
            _smsService = new AirtelService();
        }

        public User CreateUser(string userName, string password, string email, 
            string role)
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

            // send sms to user
            _smsService.SendSms("Your account was created", "9464562556");

            return userObj;
        }


        public User? GetUserByCredentails(string email,string password)
        {
           return _dbContext.Users.FirstOrDefault(p => p.Email == email && p.Password == password);
        }
    }
}
