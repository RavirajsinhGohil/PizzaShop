using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace test.Services{
    public class EmailSender : IEmailSender
{
     public async Task SendEmailAsync(string email, string subject, string htmlMessage){
        var emailToSend = new MimeMessage();
        emailToSend.From.Add(MailboxAddress.Parse("test.dotnet@etatvasoft.com"));
        emailToSend.To.Add(MailboxAddress.Parse(email));
        emailToSend.Subject = subject;
        emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html){Text = htmlMessage};

        using (var emailClient = new SmtpClient())
        {
            emailClient.Connect("mail.etatvasoft.com", 587, SecureSocketOptions.StartTls);
            emailClient.Authenticate("test.dotnet@etatvasoft.com", "P}N^{z-]7Ilp");
            emailClient.Send(emailToSend);
            emailClient.Disconnect(true);
        }

        await Task.CompletedTask;
    }
}

}

