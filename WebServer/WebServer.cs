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

                // NEED TO PARALLELIZE RIGHT HERE! send request/response, or entire context?

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
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // You must close the output stream.
                output.Close();
            }
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

                    if (!requestJson.image)
                    {
                        return @"{
                            'error': 'No image provided'
                        }";
                    }


                    Bitmap bmp = BitmapFromBase64(requestJson.img);
                    string filter = requestJson.filter.ToLower();

                    ImageManipulator im = new ImageManipulator(bmp);

                    switch (filter) {
                        case "grayscale":
                            im.Grayscale();
                            break;
                        case "flip":
                            // TODO support args!
                            im.Flip(false);
                            break;
                        case "rotate":
                            // TODO support args!
                            im.Rotate(1, true);
                            break;
                        case "filter":
                            // TODO support args!
                            im.Filter("R");
                            break;
                    }

                    string b64img = Base64FromBitmap(im.ToBitmap());

                    jsonResponse = $@"{{
                        'image': {b64img}
                    }}";
                } 
                else
                {
                    jsonResponse = $@"{{
                        'error': 'Invalid Content-Type. Must be json or application/json'
                    }}";
                }
                return jsonResponse;
            }
            catch (Exception e)
            {
                return @"{
                    'error': """ + System.Web.HttpUtility.JavaScriptStringEncode(e.Message) + @"""
                }";
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
