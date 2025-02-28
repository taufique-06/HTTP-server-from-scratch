namespace http_server.Steps;

public class ThirdStep
{
    //Step 3: Handling Different Routes AND Return Content Response
    //Goal is to:
    // Direct If the client requests /, you can respond with "Hello World".
    // Direct If the client requests /about, you can respond with "About Us".
    // Direct If the client requests a path that does not exist, return a 404 Not Found error.

    public string ReturnResponse(string path)
    {
        var responseContent = "";

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
        
        return 
            "HTTP/1.1 200 OK\r\n" +
            "Content-Type: text/plain; charset=UTF-8\r\n" +
            "Content-Length: " + responseContent.Length + "\r\n" +
            "\r\n" +
            responseContent;
    }
}