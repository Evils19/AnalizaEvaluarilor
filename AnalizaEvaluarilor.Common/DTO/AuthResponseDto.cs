using System.Text.Json.Serialization;

namespace AnalizaEvaluarilor.Common.DTO;

public record AuthResponseDto(string Token, string? ErrorMesage=null)
{
    [JsonIgnore]
    public bool HasError => ErrorMesage !=  null;   
}
