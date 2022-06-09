using System.Text.Json.Serialization;

namespace MyEiu.API.Dtos
{
    public class StaffPagingDto
    {
        [JsonPropertyName("Search_Key")]
        public string? Search_Key { get; set; }      
        [JsonPropertyName("Current_Page")]
        public int Current_Page { get; set; }
        [JsonPropertyName("Page_Size")]
        public int Page_Size { get; set; }
    }
}
