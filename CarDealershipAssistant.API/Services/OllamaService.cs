using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using CarDealershipAssistant.API.DTOs;

namespace CarDealershipAssistant.API.Services;

public class OllamaService
{
    private readonly HttpClient _httpClient;
    private readonly CarToolService _carToolService;

    public OllamaService(HttpClient httpClient, CarToolService carToolService)
    {
        _httpClient = httpClient;
        _carToolService = carToolService;
    }

    public async Task<string> AskAsync(string message)
    {
        var request = new
        {
            model = "llama3.2:3b",
            messages = new[]
            {
                new
                {
                    role = "user",
                    content = message
                }
            },
            stream = false
        };

        var response = await _httpClient.PostAsJsonAsync(
            "api/chat",
            request);

        response.EnsureSuccessStatusCode();

        var result = await response.Content
            .ReadFromJsonAsync<OllamaResponse>();

        return result?.Message?.Content
            ?? "I could not generate a response.";
    }

    public async Task<string> AskWithToolsAsync(string message)
    {
        var messages = new List<object>
    {
        new
        {
            role = "system",
            content = """
            You are an AI assistant for a car dealership.

            You help users find cars from the dealership inventory.

            When the user asks to find, search, or recommend cars,
            use the SearchCars tool.

            Never invent cars, prices, mileage, or specifications.

            Only provide information that comes from the dealership inventory.

            When you receive results from SearchCars,
            summarize the results clearly for the user.
            """
        },

        new
        {
            role = "user",
            content = message
        }
    };

        var request = new
        {
            model = "llama3.2:3b",

            messages = messages,

            tools = new[]
            {
            new
            {
                type = "function",

                function = new
                {
                    name = "SearchCars",

                    description =
                        "Search the dealership inventory for cars matching the user's criteria.",

                    parameters = new
                    {
                        type = "object",

                        properties = new
                        {
                            brand = new
                            {
                                type = "string",
                                description = "The car brand."
                            },

                            maxPrice = new
                            {
                                type = "number",
                                description = "Maximum price in euros."
                            },

                            minYear = new
                            {
                                type = "integer",
                                description = "Minimum manufacturing year."
                            },

                            fuelType = new
                            {
                                type = "string",
                                description = "Fuel type such as Diesel, Petrol, or Hybrid."
                            },

                            transmission = new
                            {
                                type = "string",
                                description = "Transmission such as Automatic or Manual."
                            }
                        }
                    }
                }
            }
        },

            stream = false
        };

        var response = await _httpClient.PostAsJsonAsync(
            "api/chat",
            request);

        response.EnsureSuccessStatusCode();

        var rawResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine("OLLAMA RESPONSE:");
        Console.WriteLine(rawResponse);

        var result = JsonSerializer.Deserialize<OllamaToolResponse>(
    rawResponse);

        // var result = await response.Content
        //     .ReadFromJsonAsync<OllamaToolResponse>();

        if (result?.Message?.ToolCalls == null ||
            result.Message.ToolCalls.Count == 0)
        {
            return result?.Message?.Content
                ?? "No response generated.";
        }

        var toolCall = result.Message.ToolCalls[0];

        if (toolCall.Function.Name != "SearchCars")
        {
            return "Unknown tool requested.";
        }

        var searchRequest =
            toolCall.Function.Arguments
                .Deserialize<SearchCarsRequest>();

        if (searchRequest == null)
        {
            return "I could not understand the search criteria.";
        }

        var cars = await _carToolService
            .SearchCarsAsync(searchRequest);

        Console.WriteLine("TOOL CALLED:");
        Console.WriteLine(toolCall.Function.Name);

        Console.WriteLine("ARGUMENTS:");
        Console.WriteLine(toolCall.Function.Arguments.ToString());

        Console.WriteLine("TOOL RESULT:");
        Console.WriteLine(cars);

        messages.Add(new
        {
            role = "assistant",
            content = result.Message.Content,
            tool_calls = result.Message.ToolCalls
        });

        messages.Add(new
        {
            role = "tool",
            content = cars,
            tool_call_id = toolCall.Id
        });

        var finalRequest = new
        {
            model = "llama3.2:3b",

            messages = messages,

            stream = false
        };

        var finalResponse = await _httpClient.PostAsJsonAsync(
            "api/chat",
            finalRequest);

        finalResponse.EnsureSuccessStatusCode();

        var finalResult = await finalResponse.Content
            .ReadFromJsonAsync<OllamaResponse>();

        return finalResult?.Message?.Content
            ?? "I could not generate a final response.";
    }
}


public class OllamaResponse
{
    [JsonPropertyName("message")]
    public OllamaMessage? Message { get; set; }
}

public class OllamaToolResponse
{
    [JsonPropertyName("message")]
    public OllamaToolMessage? Message { get; set; }
}

public class OllamaToolMessage
{
    [JsonPropertyName("role")]
    public string Role { get; set; } = string.Empty;

    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    [JsonPropertyName("tool_calls")]
    public List<OllamaToolCall>? ToolCalls { get; set; }
}

public class OllamaToolCall
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("function")]
    public OllamaFunctionCall Function { get; set; } = new();
}

public class OllamaFunctionCall
{
    [JsonPropertyName("index")]
    public int Index { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("arguments")]
    public JsonElement Arguments { get; set; }
}
public class OllamaMessage
{
    [JsonPropertyName("role")]
    public string Role { get; set; } = string.Empty;

    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;
}