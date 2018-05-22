using owncloudsharp;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OwnCloudDemo
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            // curl -u admin:admin --upload-file Louis.jpg "http://berts-mbp/remote.php/webdav/Louis.jpg"
            var res = await UploadFile("http://berts-mbp/remote.php/webdav/", "admin:admin",  "bin.jpg");
        }

        async static Task<string> UploadFile(string uri, string auth, string file)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://berts-mbp/remote.php/webdav/");
            var byteArray = Encoding.ASCII.GetBytes(auth);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var fileContent = new ByteArrayContent(File.ReadAllBytes(file));
            var response = await client.PutAsync(file, fileContent);
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
