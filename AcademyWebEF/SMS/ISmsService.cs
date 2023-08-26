namespace AcademyWebEF.SMS
{
    public interface ISmsService
    {
        void SendSms(string message, string receipient);
    }
}
