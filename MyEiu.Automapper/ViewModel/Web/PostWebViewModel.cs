using System.Text.Json.Serialization;

namespace MyEiu.Automapper.ViewModel.Web
{
    public class PostWebViewModel
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("Post_Date")]
        public DateTime Post_Date { get; set; }
        [JsonPropertyName("Post_Author")]
        public string? Post_Author { get; set; }
        [JsonPropertyName("Post_Title")]
        public string? Post_Title { get; set; }
        [JsonPropertyName("Post_Description")]
        public string? Post_Description { get; set; }
        [JsonPropertyName("Post_Url")]
        public string? Post_Url { get; set; }
        //public string Thumbnail_Url { get; set; }

        [JsonPropertyName("Post_Thumbnail")]
        public string Post_Thumbnail { get; set; }
    }
}
