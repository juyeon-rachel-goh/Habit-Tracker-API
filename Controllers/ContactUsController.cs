using System.Net;
using System.Net.Mail;
using Api.DataTransferObjects;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers;

[ApiController]
[Route("api/send-email")]
public class ContactUsController : ControllerBase
{
    public ContactUsController()
    {

    }

    [HttpPost]
    [Route("")]
    async public Task<ActionResult<Message>> SendEmail([FromBody] Message message)
    {
        // if (message == null)
        // {
        //     return BadRequest();
        // }
        // MailMessage content = new MailMessage();
        // SmtpClient smtpClient = new SmtpClient();
        // content.From = new MailAddress("noreply@habits.com");
        // content.To.Add("");
        // content.Subject = "New Inquiry";
        // content.IsBodyHtml = true;
        // content.Body = "<p>" + message.FullName + "</p>" + "<p>" + message.Email + "</p>" + "<p>" + message.Content + "</p>";


        // //configure smtp
        // smtpClient.Port = 587;
        // smtpClient.Host = "smtp.gmail.com";
        // smtpClient.EnableSsl = true;
        // smtpClient.UseDefaultCredentials = false;
        // smtpClient.Credentials = new NetworkCredential("test@guerrillamail.biz", "");
        // smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        // smtpClient.Send(content);
        return Ok();

    }


}
