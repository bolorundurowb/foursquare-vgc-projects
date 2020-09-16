namespace api.Shared.Email.Models
{
    public class EmailAttachment
    {
        public byte[] Content { get; set; }
        
        public string Name { get; set; }
        
        public string MimeType { get; set; }
    }
}