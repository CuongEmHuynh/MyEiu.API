using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyEiu.Application.Dtos
{
    public class NotifPagingDto
    {
        [JsonPropertyName("Email")]
        public string? Email { get; set; }
        [JsonPropertyName("Current_Page")]
        public int Current_Page { get; set; }
        [JsonPropertyName("Page_Size")]
        public int Page_Size { get; set; }
    }
}
