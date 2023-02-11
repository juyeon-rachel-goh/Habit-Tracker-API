using System.Net;
using System.Net.Mail;
using Api.DataTransferObjects;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Microsoft.Extensions.Options;

namespace Api.Controllers;

[ApiController]
[Route("api/send-email")]
public class ContactUsController : ControllerBase
{
    private readonly IOptions<AppSettings> appSettings;
    public ContactUsController(IOptions<AppSettings> appSettings)
    {
        this.appSettings = appSettings;
    }

    [HttpPost]
    [Route("")]
    async public Task<ActionResult<Message>> SendEmail([FromBody] Message message)
    {
        if (message == null)
        {
            return BadRequest();
        }
        MailMessage content = new MailMessage();
        SmtpClient smtpClient = new SmtpClient();
        content.From = new MailAddress(message.Email);
        content.To.Add(appSettings.Value.EmailAddress);
        content.Subject = "New Inquiry";
        content.IsBodyHtml = true;
        content.Body = "<p>" + message.FullName + "</p>" + "<p>" + message.Email + "</p>" + "<p>" + message.Content + "</p>";

        //configure smtp
        smtpClient.Port = appSettings.Value.Port;
        smtpClient.Host = appSettings.Value.Host;
        smtpClient.EnableSsl = appSettings.Value.EnableSsl;
        smtpClient.UseDefaultCredentials = appSettings.Value.UseDefaultCredentials;
        smtpClient.Credentials = new NetworkCredential(appSettings.Value.EmailAddress, appSettings.Value.Password);
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.Send(content);
        return Ok();

    }


}
