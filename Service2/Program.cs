

HttpClient client = new();
HttpResponseMessage result = await client.GetAsync("https://localhost:7080/api/values");

if(result.IsSuccessStatusCode)
{
    string content = await result.Content.ReadAsStringAsync();
    Console.WriteLine(content);
}
else
{
    Console.WriteLine("Error: " + result.StatusCode);
}