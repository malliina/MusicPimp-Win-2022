using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MusicPimp.Http;
using Windows.Security.Credentials;
using Windows.Storage;

namespace MusicPimp
{
    public class PimpVM : BindableBase
    {
        private string serverInput;
        private string usernameInput;
        private string passwordInput;
        private bool savePassword;
        private string submitButtonText;


        private string feedbackText;

        public PimpVM()
        {
            ServerInput = "";
            UsernameInput = "";
            PasswordInput = "";
            SavePassword = false;
            SubmitButtonText = "Sign in";
        }

        public string ServerInput
        {
            get => serverInput;
            set
            {
                SetProperty(ref serverInput, value);
                OnPropertyChanged(nameof(IsSubmitButtonEnabled));
            }
        }

        public string UsernameInput
        {
            get => usernameInput;
            set
            {
                SetProperty(ref usernameInput, value);
                OnPropertyChanged(nameof(IsSubmitButtonEnabled));
            }
        }

        public string PasswordInput
        {
            get => passwordInput;
            set
            {
                SetProperty(ref passwordInput, value);
                OnPropertyChanged(nameof(IsSubmitButtonEnabled));
            }
        }

        public bool SavePassword
        {
            get => savePassword;
            set
            {
                SetProperty(ref savePassword, value);
            }
        }

        public string SubmitButtonText
        {
            get => submitButtonText;
            set
            {
                SetProperty(ref submitButtonText, value);
            }
        }

        public string FeedbackText
        {
            get => feedbackText;
            set
            {
                SetProperty(ref feedbackText, value);
            }
        }

        public async void SubmitButtonClicked()
        {
            try
            {
                var creds = new Credential(ServerInput, UsernameInput, PasswordInput);
                using var client = new PimpHttpClient(creds);
                var version = await client.PingAuth();
                SaveCredentials(creds);
                Debug.Print($"Version {version.Version}");
                FeedbackText = $"Welcome to version {version.Version}.";
            }
            catch (HttpException he)
            {
                Debug.Print($"Invalid http response. {he.Reason}");
                FeedbackText = he.Reason;
            }
            catch (Exception e)
            {
                Debug.Print("Invalid http response.");
                FeedbackText = "Login failed.";
            }
        }

        private readonly string serverKey = "server";
        private readonly string credentialResource = "MusicPimp";


        private void SaveCredentials(Credential creds)
        {
            var settings = ApplicationData.Current.RoamingSettings;
            settings.Values[serverKey] = creds.Server;
            var vault = new PasswordVault();
            vault.Add(new PasswordCredential(credentialResource, creds.Username, creds.Password));
        }

        private Credential LoadCredentials()
        {
            var settings = ApplicationData.Current.RoamingSettings;
            var serverObj = settings.Values[serverKey];
            if (serverObj != null)
            {
                var server = serverObj as string;
                var vault = new PasswordVault();
                var credentials = vault.FindAllByResource(credentialResource);
                if (credentials.Count == 1)
                {
                    var cred = credentials[0];
                    return new Credential(server, cred.UserName, cred.Password);
                } else
                {
                    return null;
                }
            }
            return null;
        }

        public bool IsSubmitButtonEnabled => UsernameInput.Length > 0 && PasswordInput.Length > 0;

    }
}
