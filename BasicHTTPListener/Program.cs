using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace BasicHTTPListener
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("HttpListener is not supported.");
                return;
            }

            int port = 9999; //todo: read port number from config file
            var prefixes = new List<string>() { $"http://*:{port}/" };

            HttpListener listener = new HttpListener();
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
            }
            listener.Start();
            Console.WriteLine($"Listening on port: {port}");
            
            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                string documentContents;
                using (Stream receiveStream = request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        documentContents = readStream.ReadToEnd();
                    }
                }
                Console.WriteLine($"Recived request for {request.Url}");
                Console.WriteLine(documentContents);
                Console.WriteLine("------------------------------------------");
               
            }
            listener.Stop();
        }
    }
}
