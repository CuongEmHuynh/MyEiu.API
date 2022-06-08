using System.Text.Json.Serialization;

namespace MyEiu.API.Dtos
{
    public class PostPagingDto
    {
        [JsonPropertyName("Post_Type")]
        public string Post_Type{ get; set; }
        [JsonPropertyName("Post_Language")]
        public string Post_Language { get; set; }
        [JsonPropertyName("Current_Page")]
        public int Current_Page { get; set; }
        [JsonPropertyName("Page_Size")]
        public int Page_Size { get; set; }
    }
}
