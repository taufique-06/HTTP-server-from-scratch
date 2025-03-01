# Simple HTTP Server in C#

A lightweight HTTP server project built from scratch using **C# and TcpListener**. It supports handling GET and POST requests, parsing query parameters.

## Features
- ✅ **Basic HTTP Request Handling** (GET, POST)
- ✅ **URL Query Parameter Parsing**
- ✅ **Static File Serving** (HTML, CSS, JS, images)
- ✅ **JSON Responses for API endpoints**
- ✅ **Custom Routing for dynamic content**

## Installation & Setup
### Prerequisites
- .NET SDK installed ([Download .NET](https://dotnet.microsoft.com/download))

### Clone the Repository
```sh
git clone https://github.com/taufique-06/HTTP-server-from-scratch.git
cd csharp-http-server
```

### Run the Server
```sh
dotnet run
```
The server will start on `http://localhost:4221`.

## Usage
### 1️⃣ Handling GET Requests
#### Retrieve Data from `/search`
```sh
curl -v "http://localhost:4221/search?query=author&page=19"
```
✅ **Response (JSON)**:
```json
{
    "search_term": "author",
    "page_number": "19"
}
```

### 2️⃣ Handling POST Requests
#### Send Form Data to `/submit`
```sh
curl -X POST -d "name=Tee&age=!20" http://localhost:4221/submit
```
✅ **Response:**
```json
{
    "name": "tee",
    "age": "!20"
}
```

## Next Steps
🚀 **Serving static files like HTML, CSS, JavaScript, and images.**  
🚀 **Add multithreading to support multiple clients**  
🚀 **Implement session handling and authentication**  

---
### 📌 Let's make it better
If you are interested make it better, give me a shout!!!

