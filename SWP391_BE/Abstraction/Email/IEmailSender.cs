using System.Net.Mail;
using SWP391_BE.Modelonly.Email;
namespace SWP391_BE.Abstraction.Email
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(EmailMessage email);
        public string GetTextRegisterFacility(string account, string password);
        public string GetResetPasswordEmail(string verificationCode);
        public string GetVerificationEmail(string verificationCode);
    }
}
