using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Drawing;
using ParallelImageManipulator;
using System.Threading;

namespace WebServer
{
    class WebServer
    {
        public HttpListener listener;
        public string url = "http://localhost:8080/";
        private TextWriter output;
        JsonSerializer serializer;

        public WebServer()
        {
            output = Console.Out;
            listener = new HttpListener();
            serializer = new JsonSerializer();
        }

        public WebServer(string prefix)
        {
            output = Console.Out;
            listener = new HttpListener();
            serializer = new JsonSerializer();
            url = prefix;
        }

        public WebServer(TextWriter textWriter)
        {
            output = textWriter;
            listener = new HttpListener();
            serializer = new JsonSerializer();
        }

        public WebServer(string prefix, TextWriter textWriter)
        {
            output = textWriter;
            listener = new HttpListener();
            serializer = new JsonSerializer();
            url = prefix;
        }

        public void Start()
        {
            listener.Prefixes.Add(url);
            output.WriteLine("Starting WebServer on " + url);
            listener.Start();
            output.WriteLine("WebServer started");

            while (true)
            {
                HttpListenerContext context = listener.GetContext();

                Thread requestHandler = new Thread(new ParameterizedThreadStart(this.dowork));
                requestHandler.Start(context);
            }
        }

        public void dowork(object contextObj)
        {
            HttpListenerContext context = (HttpListenerContext)contextObj;
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            output.WriteLine($"========================\nReceived request: {request.HttpMethod} {request.Url} {request.Headers}");

            string responseString = "";

            if (request.HttpMethod == "POST")
            {
                responseString = Post(request);
            }

            response.ContentType = "application/json";
            // Construct a response.
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            System.IO.Stream responseOutput = response.OutputStream;
            responseOutput.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            responseOutput.Close();

            output.WriteLine($"========================\nSent response string: {response.Headers}");
        }

        // example json
        // { image: ..., filter: ..., args: { ... } }
        private string Post(HttpListenerRequest request)
        {
            try { 
                string contentType = request.ContentType;

                string jsonResponse = "";

                if (contentType == "application/json" || contentType == "json")
                {
                    dynamic requestJson = GetRequestJson(request);

                    if (requestJson.image == null)
                    {
                        return @"{'error': 'No image provided'}";
                    }


                    Bitmap bmp = BitmapFromBase64((string)requestJson["image"]);
                    string filter = ((string)requestJson["filter"]).ToLower();

                    ImageManipulator im = new ImageManipulator(bmp);

                    switch (filter) {
                        case "grayscale":
                            im.Grayscale();
                            break;
                        case "flip":
                            bool vertical;

                            if (requestJson["args"] != null && requestJson.args["vertical"] != null)
                            {
                                vertical = (bool)requestJson.args.vertical;
                                im.Flip(vertical);
                            } else {
                                jsonResponse = $@"{{'error': 'Missing ""args.vertical""'}}";
                                return jsonResponse;
                            }

                            break;
                        case "rotate":
                            if (requestJson["args"] != null 
                                && requestJson.args["clockwise"] != null 
                                && requestJson.args["rotates"] != null)
                            {
                                bool clockwise = (bool)requestJson.args.clockwise;
                                int rotates = (int)requestJson.args.rotates;
                                im.Rotate(1, true);
                            }
                            else
                            {
                                jsonResponse = $@"{{'error': 'Missing ""args.clockwise"" or ""args.rotates""'}}";
                                return jsonResponse;
                            }
                            
                            
                            break;
                        case "filter":
                            // color can be one of: R, G, B
                            if (requestJson["args"] != null && requestJson.args["color"] != null)
                            {
                                string color = (string)requestJson.args.color;
                                im.Filter(color);
                            }
                            else
                            {
                                jsonResponse = $@"{{'error': 'Missing ""args.clockwise"" or ""args.rotates""'}}";
                                return jsonResponse;
                            }
                            
                            break;
                        case "negate":
                            im.Grayscale();
                            break;
                        default:
                            break;
                    }
                    string b64img = Base64FromBitmap(im.ToBitmap());

                    jsonResponse = $@"{{
                        'image': {b64img}
                    }}";
                } 
                else
                {
                    jsonResponse = $@"{{'error': 'Invalid Content-Type. Must be json or application/json'}}";
                }
                return jsonResponse;
            }
            catch (Exception e)
            {
                
                return @"{
                    'error': ""Exception: " + System.Web.HttpUtility.JavaScriptStringEncode(e.Message) + @""",
                    'stacktrace': """ + e.StackTrace + @"""
            }
            ";
            }
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

        private dynamic GetRequestJson(HttpListenerRequest request)
        {
            List<string> requestList = new List<string>();
            string body = GetRequestBody(request);

            return JValue.Parse(body);
        }

        private Bitmap BitmapFromBase64(string b64str)
        {
            byte[] data = System.Convert.FromBase64String(b64str);
            MemoryStream ms = new MemoryStream(data);
            Image img = Image.FromStream(ms);
            Bitmap bmp = new Bitmap(img);
            return bmp;
        }

        private string Base64FromBitmap(Bitmap bmp)
        {
            System.IO.MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            string b64str = Convert.ToBase64String(ms.GetBuffer());
            return b64str;
        }
    }
}
