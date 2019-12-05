using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebServer
{
    class WebServer
    {
        public HttpListener listener;
        public string url = "http://localhost:8080/";
        JsonSerializer serializer;

        public WebServer()
        {
            listener = new HttpListener();
            serializer = new JsonSerializer();
        }

        public WebServer(string prefix)
        {
            listener = new HttpListener();
            serializer = new JsonSerializer();
            url = prefix;
        }

        public void Start()
        {
            listener.Prefixes.Add(url);
            listener.Start();

            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;


                if (request.HttpMethod == "POST")
                {
                    Post(request);
                }

                // Construct a response.
                string responseString = request.Headers.ToString();
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // You must close the output stream.
                output.Close();
            }
        }

        private string Post(HttpListenerRequest request)
        {
            string contentType = request.ContentType;

            string jsonResponse = "";

            if (contentType == "application/json" || contentType == "json")
            {
                string requestJson = 

                
            } 
            else
            {
                jsonResponse = @"{
                    'error': 'Invalid Content-Type. Must be json or application/json'
                }";
            }
            return jsonResponse;
        }

        private string GetRequestBody(HttpListenerRequest request)
        {
            System.IO.Stream body = request.InputStream;
            System.Text.Encoding encoding = request.ContentEncoding;
            System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);

            if (!request.HasEntityBody)
            {
                return "";
            }

            return reader.ReadToEnd();
        }

        private List<string> GetRequestJson(HttpListenerRequest request)
        {
            List<string> requestList = new List<string>();
            //JsonConvert.DeserializeObject()
            return requestList;
        }

    }
}
