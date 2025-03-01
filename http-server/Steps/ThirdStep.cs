using System.Net;
using System.Text;

namespace http_server.Steps;

public class ThirdStep
{
    //Step 3: Handling Different Routes With Different method AND Return Content Response
    //Goal is to:
    // Direct If the client requests /, you can respond with "Hello World".
    // Direct If the client requests /about, you can respond with "About Us".
    // Direct If the client requests a path that does not exist, return a 404 Not Found error.

    private const string Post = "POST";
    private const string Get = "GET";

    public string ReturnResponse(string method, int receivedBytes, byte[] buffer, string path)
    {
        var responseContent = "";

        if (method.Equals(Get, StringComparison.OrdinalIgnoreCase))
        {
            if (path.Equals("/"))
            {
                responseContent = "Helloo World!";
            }
            else if (path.StartsWith("/contact"))
            {
                responseContent = "You can contact Taufique by emailing him";
            }
            else if (path.StartsWith("/search"))
            {
                //Query Parameter exists
                var queries = ParseQueryParams(path);
                
                var query = queries.ContainsKey("query") && queries["query"].Count > 0 
                    ? string.Join(", ", queries["query"])
                    : "No query parameter found.";
                
                var page = queries.ContainsKey("page") && queries["page"].Count > 0 
                    ? string.Join(", ", queries["page"]) 
                    : "1";
                
                responseContent = $"GET: Search results for '{query}' (Page {page})";
            }
            else if (path.StartsWith("/json"))
            {
                var response = new
                {
                    message = "Hello World",
                    status = HttpStatusCode.OK,
                    data = new
                    {
                        name = "Taufique"
                    }
                };
                
                responseContent = System.Text.Json.JsonSerializer.Serialize(response);
            }
            else
            {
                responseContent = "404 - Path Not Found";
            }
        }
        else if (method.Equals(Post, StringComparison.OrdinalIgnoreCase))
        {
            if (method.Equals(Post, StringComparison.OrdinalIgnoreCase))
            {
                string data = "";
                if (receivedBytes > 0)
                {
                    data = Encoding.UTF8.GetString(buffer, 0, receivedBytes);
                }
                
                responseContent = "POST: Data received: " + data;
            }
        }
        else
        {
            responseContent = "Other Method is not allowed at the moment. Work in Progress";
        }

        return
            "HTTP/1.1 200 OK\r\n" +
            "Content-Type: text/plain; charset=UTF-8\r\n" +
            "Content-Length: " + responseContent.Length + "\r\n" +
            "\r\n" +
            responseContent;
    }

    private Dictionary<string, List<string>> ParseQueryParams(string url)
    {
        //URL = "/search?query=cat&page=2&query=dog&page=3"
        var queryParams = new Dictionary<string, List<string>>();
        var queryStart = url?.IndexOf("?") ?? -1;

        if (queryStart != -1)
        {
            var queryString = url!.Substring(queryStart + 1); //query=cat&page=2&query=dog&page=3
            var parameters = queryString.Split('&');

            foreach (var param in parameters)
            {
                var keyValue = param.Split("=");
                if (keyValue.Length == 2)
                {
                    if (!queryParams.ContainsKey(keyValue[0]))
                    {
                        queryParams[keyValue[0]] = new List<string>();
                    }
                    queryParams[keyValue[0]].Add(keyValue[1]);
                }
            }
        }

        return queryParams;
    }
    
    static Dictionary<string, string> ParseFormData(string body)
    {
        var formData = new Dictionary<string, string>();
        string[] parameters = body.Split('&');
        
        foreach (var param in parameters)
        {
            var keyValue = param.Split('=');
            if (keyValue.Length == 2)
            {
                formData[Uri.UnescapeDataString(keyValue[0])] = Uri.UnescapeDataString(keyValue[1]);
            }
        }

        return formData;
    }
}