using AcademyWebEF.BusinessEntities;

namespace AcademyWebEF.Interfaces
{
    public interface IUserService
    {
        User CreateUser(string userName, string password, string email,
            string role);

        User? GetUserByCredentails(string email, string password);
    }
}
