using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TvMazeApi.Models
{
    public record class Record
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecordId { get; set; }

        [JsonPropertyName("id")]
        public int ShowId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("premiered")]
        public string Premiered { get; set; }

        [JsonPropertyName("summary")]
        public string Summary { get; set; }
       
    }
}
