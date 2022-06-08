using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyEiu.Automapper.ViewModel
{
    public class PostViewModel
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
       
        [JsonPropertyName("Post_Date")]
        public DateTime Post_Date { get; set; }
        [JsonPropertyName("Post_Title")]
        public string? Post_Title { get; set; }
        [JsonPropertyName("Post_Description")]
        public string? Post_Description { get; set; }
        
        [JsonPropertyName("Post_Url")]
        public string? Post_Url { get; set; }
        [JsonPropertyName("Post_Author")]
        public string Post_Author { get; set; }
        [JsonPropertyName("Post_Thumbnail")]
        public string Post_Thumbnail { get; set; }
                  
    }
}
