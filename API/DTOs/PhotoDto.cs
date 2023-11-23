using System.Text.Json.Serialization;
using API.Helper;

namespace API.DTOs
{
    public class PhotoDto
    {
        public int Id { get; set; }
        public string url { get; set; }
        public bool IsMain{ get; set; }

    }
}
