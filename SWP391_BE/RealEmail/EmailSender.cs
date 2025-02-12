using System.Net;
using System.Net.Mail;

using Microsoft.Extensions.Options;
using SWP391_BE.Modelonly.Email;
using SWP391_BE.Abstraction.Email;
namespace SWP391_BE.RealEmail
{
    public class EmailSender : IEmailSender
    {
        public EmailSetting _emailsetting { get; }
        public EmailSender(IOptions<EmailSetting> emailsetting)
        {
            _emailsetting = emailsetting.Value;
        }
        public async Task<bool> SendEmailAsync(EmailMessage emailContent)
        {
            try
            {
                MailMessage mailMessage = new MailMessage()
                {
                    Subject = emailContent.Subject,
                    Body = emailContent.Body,
                    IsBodyHtml = true,
                };
                mailMessage.From = new MailAddress(
                   _emailsetting.FromEmailAddress,
                   _emailsetting.FromDisplayName
                );
                mailMessage.To.Add(emailContent.To);
                var smtp = new SmtpClient()
                {
                    EnableSsl = _emailsetting.Smtp.EnableSsl,
                    Host = _emailsetting.Smtp.Host,
                    Port = _emailsetting.Smtp.Port,
                };
                var network = new NetworkCredential(
                   _emailsetting.Smtp.EmailAddress,
                   _emailsetting.Smtp.ApiKey
                );
                smtp.Credentials = network;
                await smtp.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error SendMail: {0}", ex);
                return false;
            }
        }

        public string GetTextRegisterFacility(string account, string password)
        {
            return $@"
                <div style=""font-family: Arial, sans-serif; color: #333; max-width: 600px; margin: 0 auto; border: 1px solid #ddd; border-radius: 8px; overflow: hidden;"">
                    <div style=""background-color: #f5f5f5; padding: 20px; text-align: center;"">
                        <img src=""https://www.fieldy.online/_next/image?url=%2Fassets%2Fimages%2Flogo.png&w=128&q=75"" alt=""Fieldy"" style="" width: 150px; margin-bottom: 10px;"" />
                        <h1 style=""color: #0056b3; font-size: 24px; margin: 0; margin-top: 20px;"">Chào mừng bạn đến với hệ thống Quản lý sân</h1>
                    </div>

                    <div style=""padding: 20px;"">
                        <p style=""font-size: 16px; line-height: 1.6;"">Chúng tôi rất vui vì bạn đã tham gia vào hệ thống Quản lý sân. Với tài khoản này, bạn có thể dễ dàng quản lý và đặt sân cho khách hàng, theo dõi doanh thu và lịch đặt sân hàng ngày. Chúng tôi luôn ở đây để hỗ trợ bạn.</p>

                        <p style=""font-size: 16px; line-height: 1.6;"">Thông tin tài khoản của bạn như sau:</p>

                        <table style=""border-collapse: collapse; width: 100%; margin-bottom: 20px;"">
                            <tr>
                                <td style=""padding: 12px; border: 1px solid #ddd; background-color: #f9f9f9; font-weight: bold; width: 40%;"">Tài khoản:</td>
                                <td style=""padding: 12px; border: 1px solid #ddd; background-color: #ffffff;"">{account}</td>
                            </tr>
                            <tr>
                                <td style=""padding: 12px; border: 1px solid #ddd; background-color: #f9f9f9; font-weight: bold;"">Mật khẩu:</td>
                                <td style=""padding: 12px; border: 1px solid #ddd; background-color: #ffffff;"">{password}</td>
                            </tr>
                        </table>

                        <p style=""font-size: 16px; line-height: 1.6;"">
                            Bạn có thể <a href=""https://www.hamazaki.online/admin/sign-in"" style=""color: #0056b3; text-decoration: none; font-weight: bold;"">bấm vào đây</a> để đăng nhập vào hệ thống và bắt đầu quản lý sân của mình.
                        </p>

                        <p style=""font-size: 16px; line-height: 1.6;"">
                            Nếu bạn cần hỗ trợ, vui lòng liên hệ với chúng tôi qua số điện thoại <a href=""tel:09766860068"" style=""color: #0056b3; text-decoration: none;""><strong>09766-860-068</strong></a> hoặc <a href=""https://www.hamazaki.online/contact"" style=""color: #0056b3; text-decoration: none;"">truy cập trang hỗ trợ</a>.
                        </p>

                        <p style=""font-size: 16px; line-height: 1.6;"">Trân trọng cảm ơn,<br>Công ty Fieldy</p>
                    </div>

                    <div style=""background-color: #f5f5f5; padding: 10px; text-align: center; color: #777;"">
                        <p style=""font-size: 14px; margin: 0;"">© 2024 Công ty Fieldy</p>
                    </div>
                </div>
            ";
        }

        public string GetResetPasswordEmail(string verificationCode)
        {
            return $@"
            <div style=""font-family: Arial, sans-serif; color: #333; max-width: 600px; margin: 0 auto; border: 1px solid #ddd; border-radius: 8px; overflow: hidden;"">
                <div style=""background-color: #f5f5f5; padding: 20px; text-align: center;"">
                    <img src=""https://www.fieldy.online/_next/image?url=%2Fassets%2Fimages%2Flogo.png&w=128&q=75"" alt=""Fieldy"" style="" width: 150px; margin-bottom: 10px;"" />
                    <h1 style=""color: #0056b3; font-size: 24px; margin: 0; margin-top: 20px;"">Yêu cầu đặt lại mật khẩu</h1>
                </div>

                <div style=""padding: 20px;"">
                    <p style=""font-size: 16px; line-height: 1.6;"">Chào bạn,</p>
                    <p style=""font-size: 16px; line-height: 1.6;"">Chúng tôi đã nhận được yêu cầu đặt lại mật khẩu cho tài khoản của bạn.</p>

                    <p style=""font-size: 16px; line-height: 1.6;"">Để xác nhận yêu cầu của bạn, hãy sử dụng mã xác nhận dưới đây:</p>
                    
                    <h2 style=""font-size: 24px; color: #0056b3; margin: 10px 0;"">{verificationCode}</h2>

                    <p style=""font-size: 16px; line-height: 1.6;"">
                        Mã xác nhận này sẽ hết hạn sau <strong>15 phút</strong>. 
                        Vui lòng nhập mã này trên trang đặt lại mật khẩu của chúng tôi trước khi hết hạn.
                    </p>

                    <p style=""font-size: 16px; line-height: 1.6;"">Nếu bạn không yêu cầu đặt lại mật khẩu, bạn có thể bỏ qua email này.</p>

                    <p style=""font-size: 16px; line-height: 1.6;"">
                        Để đảm bảo an toàn cho tài khoản của bạn, hãy không chia sẻ mã xác nhận này với bất kỳ ai khác.
                    </p>

                    <p style=""font-size: 16px; line-height: 1.6;"">Trân trọng cảm ơn,<br>Công ty Fieldy</p>
                </div>

                <div style=""background-color: #f5f5f5; padding: 10px; text-align: center; color: #777;"">
                    <p style=""font-size: 14px; margin: 0;"">© 2024 Công ty Fieldy</p>
                </div>
            </div>";
        }
        public string GetVerificationEmail(string verificationCode)
        {
            return $@"
                <div style=""font-family: Arial, sans-serif; color: #333; max-width: 600px; margin: 0 auto; border: 1px solid #ddd; border-radius: 8px; overflow: hidden;"">
                    <div style=""background-color: #f5f5f5; padding: 20px; text-align: center;"">
                        <img src=""https://www.fieldy.online/_next/image?url=%2Fassets%2Fimages%2Flogo.png&w=128&q=75"" alt=""Fieldy"" style="" width: 150px; margin-bottom: 10px;"" />
                        <h1 style=""color: #0056b3; font-size: 24px; margin: 0; margin-top: 20px;"">Xác nhận tài khoản mới</h1>
                    </div>

                    <div style=""padding: 20px;"">
                        <p style=""font-size: 16px; line-height: 1.6;"">Chào bạn,</p>
                        <p style=""font-size: 16px; line-height: 1.6;"">Cảm ơn bạn đã đăng ký tài khoản với chúng tôi!</p>

                        <p style=""font-size: 16px; line-height: 1.6;"">Để hoàn tất quá trình đăng ký, vui lòng sử dụng mã xác nhận dưới đây:</p>
                        
                        <h2 style=""font-size: 24px; color: #0056b3; margin: 10px 0;"">{verificationCode}</h2>

                        <p style=""font-size: 16px; line-height: 1.6;"">
                            Mã xác nhận này sẽ hết hạn sau <strong>15 phút</strong>. 
                            Vui lòng nhập mã này trên trang xác nhận tài khoản của chúng tôi trước khi hết hạn.
                        </p>

                        <p style=""font-size: 16px; line-height: 1.6;"">Nếu bạn không thực hiện đăng ký tài khoản, bạn có thể bỏ qua email này.</p>

                        <p style=""font-size: 16px; line-height: 1.6;"">Trân trọng cảm ơn,<br>Công ty Fieldy</p>
                    </div>

                    <div style=""background-color: #f5f5f5; padding: 10px; text-align: center; color: #777;"">
                        <p style=""font-size: 14px; margin: 0;"">© 2024 Công ty Fieldy</p>
                    </div>
                </div>";
        }

    }
}
