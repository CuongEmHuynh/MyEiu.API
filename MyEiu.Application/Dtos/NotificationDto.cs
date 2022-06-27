namespace MyEiu.API.Dtos
{
    public class NotificationDto
    {
        public List<string>? Emails { get; set; }
        public int? Type { get; set; }//mac dinh = 0, do chua phan notification
        public string Topic { get; set; }//neu Emails =[] thì topic = "eiu_topic_all" de gui all
        public NotificationDta? Data { get; set; }

        public NotificationDto()
        {
            Emails = new List<string>();
            Type = 0;
            Topic = "";
            Data = new NotificationDta();
        }
    }
    public class NotificationDta
    {
        public string? Title { get; set; }
        public string? Body { get; set; }
        public string? Image { get; set; }       
        public int? PostId { get; set; }

    }
}
