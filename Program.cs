class Program
{
    static async Task Main()
    {
        List<string> urls = [
            "https://jsonplaceholder.typicode.com/posts/1",
            "https://jsonplaceholder.typicode.com/posts/2",
            "https://jsonplaceholder.typicode.com/posts/3"
        ];

        await FetchDataConcurrently(urls);
    }

    static async Task FetchDataConcurrently(List<string> urls)
    {
        using HttpClient client = new();

        // Create a list of tasks for fetching data
        List<Task<string>> fetchTasks = new();
        foreach (var url in urls)
        {
            fetchTasks.Add(FetchDataAsync(client, url));
        }

        // Wait for all requests to complete
        string[] results = await Task.WhenAll(fetchTasks);

        // Display results
        for (int i = 0; i < results.Length; i++)
        {
            Console.WriteLine($"Response from {urls[i]}:\n{results[i]}\n");
        }
    }

    static async Task<string> FetchDataAsync(HttpClient client, string url)
    {
        Console.WriteLine($"Fetching data from {url}...");
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
