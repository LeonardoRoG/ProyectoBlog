using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;

namespace ProyectoFinal.Rules
{
    public class ContactoRule
    {
        private readonly IConfiguration _configuration;
        public ContactoRule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(string emailTo, string mensaje, string asunto, string? deQuienNombre, string? deQuienEmail)
        {
            var from = deQuienEmail ?? _configuration["Email:fromEmail"];
            var fromName = deQuienNombre ?? _configuration["Email:fromName"]; // Con ?? evalúa si "deQuien" es null, en caso de serlo asigna como valor la expresión que sigue

            // create email message
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(fromName, from));
            email.To.Add(MailboxAddress.Parse(emailTo));
            email.Subject = asunto;
            email.Body = new TextPart(TextFormat.Html) { Text = mensaje };

            // Send email
            using var smtp = new SmtpClient();
            smtp.Connect(_configuration["Email:smtpServer"], int.Parse(_configuration["Email:smtpPort"]));
            smtp.Authenticate(_configuration["Email:fromEmail"], _configuration["Email:password"]);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
