using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;

namespace PlaywrightTests;

public class ApiTest1
{
    private IAPIRequestContext requestContext;

    [SetUp]
    public async Task Setup()
    {
        // Initialize Playwright and create an API request context
        var playwright = await Playwright.CreateAsync();
        requestContext = await playwright.APIRequest.NewContextAsync();
    }

    [Test]
    public async Task TestGetRequest()
    {
        var response = await requestContext.GetAsync("https://jsonplaceholder.typicode.com/posts/1");

        Assert.That(response.Status, Is.EqualTo(200));

        var jsonResponse = await response.JsonAsync();
        
        Assert.That(jsonResponse?.GetProperty("id").GetInt32(), Is.EqualTo(1));
    }

    [Test]
    public async Task TestPostRequest()
    {
        var data = new JsonObject
        {
            ["title"] = "foo",
            ["body"] = "bar",
            ["userId"] = 1
        };

        var response = await requestContext.PostAsync("https://jsonplaceholder.typicode.com/posts",
            new APIRequestContextOptions
            {
                DataObject = data
            });

        Assert.That(response.Status, Is.EqualTo(201));

        var jsonResponse = await response.JsonAsync();
        
        Assert.That(jsonResponse?.GetProperty("title").GetString(), Is.EqualTo("foo"));
    }

    [Test]
    public async Task TestPutRequest()
    {
        var data = new JsonObject
        {
            ["id"] = 1,
            ["title"] = "updated title",
            ["body"] = "updated body",
            ["userId"] = 1
        };

        var response = await requestContext.PutAsync("https://jsonplaceholder.typicode.com/posts/1",
            new APIRequestContextOptions
            {
                DataObject = data
            });

        Assert.That(response.Status, Is.EqualTo(200));

        var jsonResponse = await response.JsonAsync();

        Assert.That(jsonResponse?.GetProperty("title").GetString(), Is.EqualTo("updated title"));
    }

    [Test]
    public async Task TestPatchRequest()
    {
        var data = new JsonObject
        {
            ["title"] = "patched title"
        };

        var response = await requestContext.PatchAsync("https://jsonplaceholder.typicode.com/posts/1",
            new APIRequestContextOptions
            {
                DataObject = data
            });

        Assert.That(response.Status, Is.EqualTo(200));

        var jsonResponse = await response.JsonAsync();
        
        Assert.That(jsonResponse?.GetProperty("title").GetString(), Is.EqualTo("patched title"));
    }

    [Test]
    public async Task TestDeleteRequest()
    {
        var response = await requestContext.DeleteAsync("https://jsonplaceholder.typicode.com/posts/1");

        Assert.That(response.Status, Is.EqualTo(200));
    }

    [Test]
    public async Task TestGetNegativeRequest()
    {
        var response = await requestContext.GetAsync("https://jsonplaceholder.typicode.com/posts/999");
        
        Assert.That(response.Status, Is.EqualTo(404));
    }

    [TearDown]
    public async Task Teardown()
    {
        // Dispose the request context
        await requestContext.DisposeAsync();
    }
}

