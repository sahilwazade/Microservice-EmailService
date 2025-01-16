namespace EmailService.Application.Services
{
    public class EmailCommand
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHTMLBody { get; set; }
        public string HTMLView { get; set; }
        public string CcList { get; set; }
        public string AttactmentFileName { get; set; }
        public byte[] AttachmentFileByteArray { get; set; }
    }
}