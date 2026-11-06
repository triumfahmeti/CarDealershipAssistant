using CarDealershipAssistant.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarDealershipAssistant.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly OllamaService _ollamaService;

    public ChatController(OllamaService ollamaService)
    {
        _ollamaService = ollamaService;
    }

    [HttpPost]
    public async Task<IActionResult> Chat(
        [FromBody] ChatRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Message))
        {
            return BadRequest("Message cannot be empty.");
        }

        var response = await _ollamaService
    .AskWithToolsAsync(request.Message);

        return Ok(new
        {
            response
        });
    }
}

public class ChatRequest
{
    public string Message { get; set; } = string.Empty;
}