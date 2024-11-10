using System.Text.Json.Serialization;

namespace SilverJewelry_RazorPages.Models.Response
{
    public class ODataResponse<T>
    {
        [JsonPropertyName("value")]
        public T Value { get; set; }
    }
}
