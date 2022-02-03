using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using api.Shared.Email.Interfaces;
using api.Shared.Email.Models;
using dotenv.net.Utilities;
using Microsoft.Extensions.Logging;

namespace api.Shared.Email.Implementations;

public class MailgunService : IEmailService
{
    private const string Sender = "no-reply@foursquarevgc.org.com";
    private const string BaseUri = "https://api.mailgun.net/v3/";

    private readonly ILogger<IEmailService> _logger;
    private readonly IHttpClientFactory _httpFactory;
    private readonly string _apiKey;
    private readonly string _domain;

    public MailgunService(ILogger<IEmailService> logger, IHttpClientFactory httpFactory)
    {
        _logger = logger;
        _httpFactory = httpFactory;

        EnvReader.TryGetStringValue("MAILGUN_KEY", out _apiKey);
        EnvReader.TryGetStringValue("MAILGUN_DOMAIN", out _domain);
    }

    public async Task<bool> SendAsync(string recipient, EmailMessage emailMessage)
    {
        try
        {
            var requestUri = $"{_domain}/messages";
            var client = GetClient();
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(emailMessage.Sender ?? Sender), "from");
            content.Add(new StringContent(recipient), "to");
            content.Add(new StringContent(emailMessage.Subject), "subject");
            content.Add(new StringContent( emailMessage.Content), "html");

            foreach (var attachment in emailMessage.Attachments)
            {
                var file = new ByteArrayContent(attachment.Content);
                file.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "attachment",
                    FileName = attachment.Name
                };
                content.Add(file);
            }

            var response = await client.PostAsync(requestUri, content)
                .ConfigureAwait(false);
            var isSuccessful = response.IsSuccessStatusCode;

            if (isSuccessful)
                _logger.LogInformation(
                    $"Email with subject '{emailMessage.Subject}' successfully sent to '{recipient}'.");
            else
                _logger.LogCritical(
                    $"Email with subject '{emailMessage.Subject}' failed being sent to '{recipient}'. Error message: {await response.Content.ReadAsStringAsync()}");

            return isSuccessful;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Email sending errored out.");
            return false;
        }
    }

    private HttpClient GetClient()
    {
        var client = _httpFactory.CreateClient();
        client.BaseAddress = new Uri(BaseUri);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
            Convert.ToBase64String(Encoding.ASCII.GetBytes($"api:{_apiKey}")));
        return client;
    }
}