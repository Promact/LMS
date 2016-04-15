using LMS.Util.Email_Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Util.Email
{
    public class EmailUtil : IEmailUtil
    {
        public void ApplyLeave(string employeeId, string body, List<string> teamLeaderEmail)
        {
            var from = employeeId;

            foreach (var item in teamLeaderEmail)
            {
                var to = item;
                MailMessage message = new MailMessage(from, to);
                message.Subject = "Appling for Leave";
                message.Body = body;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("webmail.promactinfo.com", 587);
                client.EnableSsl = false;
                try
                {
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public void ApproveAndReject(string teamLeaderEmail, string body, string employeeEmail)
        {
            var from = teamLeaderEmail;
            var to = employeeEmail;
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Appling for Leave";
            message.Body = body;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("webmail.promactinfo.com", 587);
            client.EnableSsl = false;
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
