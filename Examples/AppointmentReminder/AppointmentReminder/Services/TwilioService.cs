using AppointmentReminder.Models.Config;
using AppointmentReminder.Services.Interfaces;
using System;
using System.Threading.Tasks;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace AppointmentReminder.Services
{
    public class TwilioService : ITwilioService
    {
        private readonly ITwilioRestClient _twilioRestClient;
        private readonly TwilioServiceConfig _config;
        private Boolean _disposed;


        public TwilioService(ISettingsService settingsService)
        {
            _config = settingsService.GetConfig().TwilioServiceConfig;
            _twilioRestClient = new TwilioRestClient(_config.AccountSid, _config.AuthToken);
        }


        // ITwilioService /////////////////////////////////////////////////////////////////////////
        public async Task SendSmsMessageAsync(String phoneNumber, String message)
        {
            await MessageResource.CreateAsync(
                to: new PhoneNumber(phoneNumber),
                from: new PhoneNumber(_config.TwilioNumber),
                body: message,
                client: _twilioRestClient);
        }
    }
}