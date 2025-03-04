using System.Net.Sockets;
using System.Text;
using http_server.Models;
using http_server.Responses;

namespace http_server.Steps;

public class SecondStep
{
    //Step 2: Reading and Parsing the HTTP Request
    public HttpReqInfo ReturnReqInfo(Socket socket)
    {
        CreateResponses createResponses = new CreateResponses();
        var buffer = new byte[1024];
        var receivedData = socket.Receive(buffer);
        var request = Encoding.UTF8.GetString(buffer, 0, receivedData);
        Console.WriteLine(request);
        Console.WriteLine("Received the Response");
        
        var lines = request.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

        if (lines.Length == 0)
        {
            createResponses.CreateErrorResponse(404, "Bad Request");
        }
        
        //Get Request Method, Path & Protocol
        var firstLine = lines[0].Split(" ");
        var httpMethod = firstLine[0];
        var reqPath = firstLine[1];
        var protocol = firstLine[2];
        
        Console.WriteLine("Method: " + httpMethod);
        Console.WriteLine("Path: " + reqPath);
        Console.WriteLine("Protocol: " + protocol);
        Console.WriteLine(lines[2]);
        Console.WriteLine(lines[1]);

        return new HttpReqInfo
        {
            Protocol = protocol,
            HttpMethod = httpMethod,
            ReceivedData = buffer,
            ReceivedDataLength = receivedData,
            ReqPath = reqPath,
        };
    }
}