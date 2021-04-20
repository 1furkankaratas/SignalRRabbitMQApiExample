using System.Net;
using System.Net.Mail;

namespace EmailSenderExample
{
    public static class EmailSender
    {
        public static void Send(string to, string message)
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;

            NetworkCredential credential = new NetworkCredential("mail@gmail.com", "mailpassword");
            smtpClient.Credentials = credential;

            MailAddress gonderen = new MailAddress("mail@gmail.com", "Display Name");
            MailAddress alici = new MailAddress(to);

            MailMessage mail = new MailMessage(gonderen,alici);

            mail.Subject = "Subject";
            mail.Body = message;
            smtpClient.Send(mail);
        }
    }
}