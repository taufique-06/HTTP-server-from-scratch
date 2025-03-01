namespace http_server.Responses;

public class RegisterRoutes
{
    public Dictionary<string, Func<string>> Routes()
    {
        CreateResponses createResponses = new CreateResponses();
        var routes = new Dictionary<string, Func<string>>();
        
        routes["/"] = () => createResponses.CreateJsonResponse(200, new { message = "Welcome!" });
        routes["/json"] = () => createResponses.CreateJsonResponse(200, new { status = "success", data = new { name = "Taufique", age = "!20" } });
        routes["/time"] = () => createResponses.CreateJsonResponse(200, new { time = DateTime.UtcNow.ToString("o") });
        routes["/hello"] = () => "HTTP/1.1 200 OK\r\nContent-Type: text/plain; charset=UTF-8\r\nContent-Length: 5\r\n\r\nHello";

        return routes;
    }
}