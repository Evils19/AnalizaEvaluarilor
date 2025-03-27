using System.Text.Json.Serialization;

namespace AnalizaEvaluarilor.Common.DTO;

public record AuthResponseDto([property: JsonPropertyName("token")]string Token,[property: JsonPropertyName("errormesage")]  string? ErrorMesage=null)
{
    [JsonIgnore]
    public bool HasError => ErrorMesage !=  null;   
}
