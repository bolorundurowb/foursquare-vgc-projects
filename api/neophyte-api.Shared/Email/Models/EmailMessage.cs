using System.Collections.Generic;

namespace neophyte.api.Shared.Email.Models;

public class EmailMessage
{
    public string Subject { get; set; }

    public string Content { get; set; }

    public List<EmailAttachment> Attachments { get; set; }

    public string Sender { get; set; }
}