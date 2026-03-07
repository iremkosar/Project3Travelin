using MimeKit;
using MailKit.Net.Smtp;

namespace Project3Travelin.Services.EmailServices
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendBookingConfirmationAsync(string toEmail, string fullName, string tourTitle, DateTime StartDate, decimal totalPrice)
        {
            var settings = _config.GetSection("EmailSettings");

            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(settings["SenderName"], settings["SenderEmail"]));
            mimeMessage.To.Add(new MailboxAddress(fullName, toEmail));
            mimeMessage.Subject = "Rezervasyonunuz Onaylandı – " + tourTitle;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $@"
                <div style='font-family:Arial,sans-serif;max-width:600px;margin:0 auto;'>
                    <div style='background:#0aab97;padding:30px;text-align:center;border-radius:8px 8px 0 0;'>
                        <h1 style='color:white;margin:0;'>✓ Rezervasyon Onaylandı</h1>
                    </div>
                    <div style='background:#f9f9f9;padding:30px;border-radius:0 0 8px 8px;'>
                        <p style='font-size:16px;'>Merhaba <strong>{fullName}</strong>,</p>
                        <p>Aşağıdaki rezervasyonunuz onaylanmıştır:</p>
                        <table style='width:100%;border-collapse:collapse;margin:20px 0;'>
                            <tr style='background:#e8f5f3;'>
                                <td style='padding:10px;font-weight:bold;'>Tur</td>
                                <td style='padding:10px;'>{tourTitle}</td>
                            </tr>
                            <tr>
                                <td style='padding:10px;font-weight:bold;'>Giriş Tarihi</td>
                                <td style='padding:10px;'>{StartDate:dd MMMM yyyy}</td>
                            </tr>
                            <tr style='background:#e8f5f3;'>
                                <td style='padding:10px;font-weight:bold;'>Toplam Fiyat</td>
                                <td style='padding:10px;'>{totalPrice:N2} ₺</td>
                            </tr>
                        </table>
                        <p>İyi tatiller dileriz!</p>
                        <p style='color:#888;font-size:13px;'>Travelin Ekibi</p>
                    </div>
                </div>"
            };

            mimeMessage.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            client.Connect(settings["SmtpHost"], int.Parse(settings["SmtpPort"]), false);
            client.Authenticate(settings["SenderEmail"], settings["SenderPassword"]);
            await client.SendAsync(mimeMessage);
            client.Disconnect(true);
        }
    }
}
