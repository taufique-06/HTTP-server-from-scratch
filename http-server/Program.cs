﻿using System.Net;
using System.Net.Sockets;
using http_server.Steps;

Console.WriteLine("Staring application");

//TcpListener is used to get the HTTP server up and running and will listen to 4221 port

// By specifying the port, telling my server to listen for any incoming connections on port 4221, regardless of the network IP.
//For example:
// Port is like the door to your house and different doors lead to different rooms/applications
// Socket is like the conversation happening, allowing messages to go back and forth.
// So when a client connects to my server, it connects to my IP and port && server creates a new socket for that connection, allowing the client to communicate
TcpListener server = new TcpListener(IPAddress.Any, 4221);
server.Start();
var socket = server.AcceptSocket();

//HTTP Response is made up of 3 parts
// Status Line
// Headers
// Response Body

var secondStep = new SecondStep();
var path = secondStep.Execute(socket);

var thirdStep = new ThirdStep();
var response = thirdStep.ReturnResponse(path);

socket.Send(System.Text.Encoding.UTF8.GetBytes(response));
socket.Shutdown(SocketShutdown.Both);
socket.Close();

