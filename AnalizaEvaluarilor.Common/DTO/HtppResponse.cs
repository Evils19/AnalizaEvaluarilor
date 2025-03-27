using System.Net;
using System.Text.Json.Serialization;

namespace AnalizaEvaluarilor.Common.DTO;

public record HtppResponse([property: JsonPropertyName("message")]string Message,[property: JsonPropertyName("statuscode")] HttpStatusCode StatusCode);
