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

    public string ReturnResponse(string method, int receivedBytes, byte[] buffer,string path)
    {
        var responseContent = "";

        if (method.Equals(Get, StringComparison.OrdinalIgnoreCase))
        {
            if (path.Equals("/"))
            {
                responseContent = "Helloo World!";
            } else if (path.Equals("/contact"))
            {
                responseContent = "You can contact Taufique by emailing him";
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
}