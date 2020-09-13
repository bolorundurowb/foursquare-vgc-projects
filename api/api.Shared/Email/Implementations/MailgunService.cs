using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using api.Shared.Email.Interfaces;
using api.Shared.Email.Models;
using dotenv.net.Interfaces;
using Microsoft.Extensions.Logging;

namespace api.Shared.Email.Implementations
{
    public class MailgunService : IEmailService
    {
        private const string Sender = "no-reply@bolorundurowb.com";
        private const string BaseUri = "https://api.mailgun.net/v3/";

        private readonly ILogger<IEmailService> _logger;
        private readonly IHttpClientFactory _httpFactory;
        private readonly string _apiKey;
        private readonly string _domain;

        public MailgunService(ILogger<IEmailService> logger, IHttpClientFactory httpFactory, IEnvReader envReader)
        {
            _logger = logger;
            _httpFactory = httpFactory;

            envReader.TryGetStringValue("MAILGUN_KEY", out _apiKey);
            envReader.TryGetStringValue("MAILGUN_DOMAIN", out _domain);
        }

        public async Task<bool> SendAsync(string recipient, EmailMessage emailMessage)
        {
            try
            {
                var requestUri = $"{_domain}/messages";
                var client = GetClient();
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("from", emailMessage?.Sender ?? Sender),
                    new KeyValuePair<string, string>("to", recipient),
                    new KeyValuePair<string, string>("subject", emailMessage?.Subject),
                    new KeyValuePair<string, string>("html", emailMessage?.Content)
                });

                var response = await client.PostAsync(requestUri, content);
                var isSuccessful = response.IsSuccessStatusCode;

                if (isSuccessful)
                {
                    _logger.LogInformation(
                        $"Email with subject '{emailMessage?.Subject}' successfully sent to '{recipient}'.");
                }
                else
                {
                    _logger.LogCritical(
                        $"Email with subject '{emailMessage?.Subject}' failed being sent to '{recipient}'. Error message: {await response.Content.ReadAsStringAsync()}");
                }

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
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes(_apiKey)));
            return client;
        }
    }
}