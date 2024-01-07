namespace URLDocumentIdentification
{
    public class UrlMetadata
    {
        public static async Task DownloadFile(string url)
        {
            using HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                response.EnsureSuccessStatusCode();

                string? mimeType = response.Content.Headers.ContentType?.MediaType;

                string fileExtension = Path.GetExtension(url);

                byte[] fileContent = await response.Content.ReadAsByteArrayAsync();

                Console.WriteLine("MIME type: " + mimeType);
                Console.WriteLine("File extension: " + fileExtension);
                Console.WriteLine("File content: " + BitConverter.ToString(fileContent));
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("HTTP request failed: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                client?.Dispose();
            }
        }

        public static async Task<bool> CheckIfUrlSupportsMediaTypes(string url)
        {
            bool success = false;
            string[] supportedMediaTypes = {
            "text/html",
            "text/plain",
            "text/css",
            "application/json",
            "application/xml",
            "application/pdf",
            "application/msword",
            "application/vnd.ms-excel",
            "application/vnd.ms-powerpoint",
            "image/png",
            "image/jpeg",
            "image/gif",
            "audio/mpeg",
            "audio/mp4",
            "video/mp4",
            "video/mpeg",
            "application/zip"
        };
            HttpClient client = new();
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Head, url);
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Content Type: " + response.Content.Headers.ContentType);
                }
                success =  supportedMediaTypes.Contains(response.Content.Headers.ContentType?.MediaType);
            }
            catch(HttpRequestException ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                client?.Dispose();
            }
            return success;
        }
    }
}
