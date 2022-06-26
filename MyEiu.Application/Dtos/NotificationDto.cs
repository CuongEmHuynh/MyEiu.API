namespace MyEiu.API.Dtos
{
    public class NotificationDto
    {
        public List<string>? Emails { get; set; }
        public int? Type { get; set; }
        public NotificationDta? Data { get; set; }

        public NotificationDto()
        {
            Emails = new List<string>();
            Type = 1;
            Data = new NotificationDta();
        }
    }
    public class NotificationDta
    {
        public string? Title { get; set; }
        public string? body { get; set; }
        public string? Thumbnail { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public int PostId { get; set; }

    }
}
