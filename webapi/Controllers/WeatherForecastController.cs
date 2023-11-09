using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace client_side_encryption.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet()]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("EncryptionKey")]
    public string GetEncryptionKey()
    {
        var rsa = RSA.Create();
        var pem = rsa.ExportRSAPublicKeyPem();
        var bytes = Encoding.UTF8.GetBytes(pem);
        return pem;
    }

    [HttpGet("EncryptionKeyFile")]
    public FileContentResult GetEncryptionKeyFile()
    {
        var pem = GetEncryptionKey();
        var bytes = Encoding.UTF8.GetBytes(pem);
        return File(bytes, "application/x-pem-file", fileDownloadName: "client-side-encryption-key.pem");
    }
}
