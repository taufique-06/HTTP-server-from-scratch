namespace http_server.Responses;

public class CreateResponses
{
    public string CreateJsonResponse(int statusCode, object responseObject)
    {
        string json = System.Text.Json.JsonSerializer.Serialize(responseObject);
        return $"HTTP/1.1 {statusCode} OK\r\n" +
               "Content-Type: application/json; charset=UTF-8\r\n" +
               $"Content-Length: {json.Length}\r\n" +
               "\r\n" +
               json;
    }
    
    public string CreateErrorResponse(int statusCode, string message)
    {
        string response = $"HTTP/1.1 {statusCode} {message}\r\n" +
                          "Content-Type: text/plain; charset=UTF-8\r\n" +
                          $"Content-Length: {message.Length}\r\n" +
                          "\r\n" +
                          message;
        return response;
    }
    
    public string DefaultResponse(string responseContent)
    {
        return
            "HTTP/1.1 200 OK\r\n" +
            "Content-Type: text/plain; charset=UTF-8\r\n" +
            "Content-Length: " + responseContent.Length + "\r\n" +
            "\r\n" +
            responseContent;
    }
}