using Microsoft.AspNetCore.Identity.UI.Services;

using System.ComponentModel;

namespace C_Utilities
{
    public class Emailsender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }
    }
}



