namespace http_server.Models;

public class HttpReqInfo
{
    public string HttpMethod { get; set; } 
    public string ReqPath { get; set; }
    public string Protocol { get; set; } 
    public int ReceivedDataLength { get; set; } 
    public byte[] ReceivedData { get; set; } 
}