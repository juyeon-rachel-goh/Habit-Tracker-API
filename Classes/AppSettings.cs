public class AppSettings
{
    public int Port { get; set; }
    public string Host { get; set; }
    public bool EnableSsl { get; set; }
    public bool UseDefaultCredentials { get; set; }
    public string EmailAddress { get; set; }
    public string Password { get; set; }
}

// smtpClient. = 587;
// smtpClient. = "smtp.gmail.com";
// smtpClient. = true;
// smtpClient. = false;
// smtpClient. = new NetworkCredential("test@guerrillamail.biz", "");
// smtpClient. = SmtpDeliveryMethod.Network;
// smtpClient.(content);