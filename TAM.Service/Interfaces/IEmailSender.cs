using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TAM.Service.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmail(string email, string subject, string htmlMessage);
    }
}
