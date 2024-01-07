using URLDocumentIdentification;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter the url");
        string? url = Console.ReadLine();

        if (!string.IsNullOrEmpty(url))
        {
            bool result = UrlMetadata.CheckIfUrlSupportsMediaTypes(url).Result;
            if (result)
            {
                Console.WriteLine($"The given url is of supported type: {result}");
                UrlMetadata.DownloadFile(url).Wait();
            }
        }
    }
}