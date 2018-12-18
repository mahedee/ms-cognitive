using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CSPredictionSample
{
    static class Program
    {
        static void Main()
        {
            Console.Write("Enter image file path: ");
            string imageFilePath = @"D:\Projects\Github\ms-cognitive\CustomVision\Images\Test\Mahedee.JPG";

            //Console.ReadLine();

            //Predict uploaded image
            MakePredictionRequest(imageFilePath).Wait();

            Console.WriteLine("\n\n\nHit ENTER to exit...");
            Console.ReadLine();
        }

        //Convert image as byte stream
        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }

        static async Task MakePredictionRequest(string imageFilePath)
        {
            var client = new HttpClient();

            //To get prediction key, url and content type, please go to
            //Project -> Performance -> Prediction URL
            //Request headers - replace this example key with your valid subscription key.

            client.DefaultRequestHeaders.Add("Prediction-Key", "9ee5bdd3110442efac28a8a8ac1797c9");
            //client.DefaultRequestHeaders.Add("Prediction-Key", "71c950638c3c4f34be320cc952174ba7");

            // Prediction URL - replace this example URL with your valid prediction URL.
            string url = "https://southcentralus.api.cognitive.microsoft.com/customvision/v2.0/Prediction/0cb90ad3-84e0-4ccc-860c-5222efc8e227/image?iterationId=af197714-139b-43a1-9032-d757c4bb0d29";
            //string url = "https://southcentralus.api.cognitive.microsoft.com/customvision/v2.0/Prediction/94a4ed5a-dc9a-4121-9d3f-aee7294d3d48/image";
            //"http://southcentralus.api.cognitive.microsoft.com/customvision/v1.0/prediction/d16e136c-5b0b-4b84-9341-6a3fff8fa7fe/image?iterationId=f4e573f6-9843-46db-8018-b01d034fd0f2";

            HttpResponseMessage response;

            // Request body. Try this sample with a locally stored image.
            byte[] byteData = GetImageAsByteArray(imageFilePath);

            using (var content = new ByteArrayContent(byteData))
            {
                //content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(url, content);
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
        }
    }
}