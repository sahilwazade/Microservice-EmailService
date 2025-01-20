namespace EmailService.Infrastructure.IRepository
{
    public class EmailTemplate
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string TemplateBody { get; set; }
        public string Placeholders { get; set; }
        public int TypeId { get; set; }
    }

    public enum EmailTemplateTypes
    {
        SendMAilWhenGetsId = 1
    }
}