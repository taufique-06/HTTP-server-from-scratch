using System.Net;
using System.Text;
using http_server.Responses;

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
        CreateResponses createResponses = new CreateResponses();
        RegisterRoutes registerRoutes = new RegisterRoutes();
        
        try
        {
            var routes = registerRoutes.Routes();
            var responseContent = "";

            if (method.Equals(Get, StringComparison.OrdinalIgnoreCase))
            {
                if (routes.ContainsKey(path))
                {
                    return routes[path]();
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
                else
                {
                    return createResponses.CreateErrorResponse(404, "Not Found");
                }
            }
            else if (method.Equals(Post, StringComparison.OrdinalIgnoreCase))
            {
                if (path.StartsWith("/submit"))
                {
                    var data = "";
                    if (receivedBytes > 0)
                    {
                        data = Encoding.UTF8.GetString(buffer, 0, receivedBytes);
                    }

                    responseContent = "POST: Data received: " + data;
                }
                else
                {
                    return createResponses.CreateErrorResponse(404,
                        "Not Found");
                }
            }
            else
            {
                return createResponses.CreateErrorResponse(405,
                    "Other Method is not allowed at the moment. Work in Progress");
            }

            return
                createResponses.DefaultResponse(responseContent);
        }
        catch (Exception e)
        {
            Console.WriteLine("Internal Server Error: " + e.Message);
            return createResponses.CreateErrorResponse(500, "Internal Server Error");
        }
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