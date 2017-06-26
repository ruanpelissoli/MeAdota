using Adopcat.Mobile.Helpers;
using Adopcat.Mobile.Interfaces;
using Adopcat.Mobile.Models;
using Adopcat.Mobile.Services;
using Adopcat.Mobile.Util;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(AzureService))]
namespace Adopcat.Mobile.Services
{
    public class AzureService
    {
        IAuthentication _auth;
        IPushNotification _pushNotification;

        List<AppServiceIdentity> identities = null;

        public MobileServiceClient Client { get; set; } = null;
        public static bool UseAuth { get; set; } = false;

        public void Initialize()
        {
            Client = new MobileServiceClient(ApplicationParameters.AzureAppServiceUrl);

            _auth = DependencyService.Get<IAuthentication>();
            _pushNotification = DependencyService.Get<IPushNotification>();

            if (!string.IsNullOrWhiteSpace(Settings.FacebookAuthToken) && !string.IsNullOrWhiteSpace(Settings.FacebookUserId))
            {
                Client.CurrentUser = new MobileServiceUser(Settings.FacebookUserId)
                {
                    MobileServiceAuthenticationToken = Settings.FacebookAuthToken
                };
            }
        }

        public async Task<bool> LoginAsync()
        {
            try
            {
                Initialize();

                var user = await _auth.LoginAsync(Client, MobileServiceAuthenticationProvider.Facebook);

                if (user == null)
                {
                    Settings.FacebookAuthToken = string.Empty;
                    Settings.FacebookUserId = string.Empty;

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Application.Current.MainPage.DisplayAlert("Ops!", "Nao conseguimos efetuar seu login, tente novamente", "OK");
                    });

                    return false;
                }
                else
                {
                    var newUser = false;

                    identities = await Client.InvokeApiAsync<List<AppServiceIdentity>>("/.auth/me");
                    var name = identities[0].UserClaims.Find(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")).Value;
                    var email = identities[0].UserClaims.Find(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")).Value;

                    var appUser = await App.ApiService.GetUserByEmail(email);

                    var userToken = identities[0].AccessToken;
                    var requestUrl = $"https://graph.facebook.com/v2.9/me/?fields=picture.width(640)&access_token={userToken}";
                    var httpClient = new HttpClient();
                    var userJson = await httpClient.GetStringAsync(requestUrl);

                    var facebookProfile = JsonConvert.DeserializeObject<FacebookProfile>(userJson);

                    if (appUser == null)
                    {
                        appUser = new User
                        {
                            Email = email,
                            FacebookId = Settings.FacebookUserId,
                            Name = name,
                            PictureUrl = facebookProfile.Picture.Data.Url
                        };
                        appUser = await App.ApiService.CreateFacebookUser(appUser);
                        newUser = true;
                    }

                    var loginResponse = await App.ApiService.LoginFacebook(new Login
                    {
                        Email = appUser.Email,
                        Password = Settings.FacebookUserId
                    });

                    if (loginResponse != null)
                    {
                        Settings.AuthToken = loginResponse.AuthToken;
                        Settings.UserId = loginResponse.UserId.ToString();
                    }

                    if (!appUser.PictureUrl.Equals(facebookProfile.Picture.Data.Url))
                    {
                        try
                        {
                            appUser.PictureUrl = facebookProfile.Picture.Data.Url;
                            await App.ApiService.UpdateUser(appUser.Email, appUser, "bearer " + Settings.AuthToken);
                        }
                        catch { }
                    }

                    if (newUser)
                        _pushNotification.RegisterPush();

                    return true;
                }
            }
            catch (Exception ex)
            {
                await App.ExceptionHandler.Handle(ex);
                return false;
            }
        }

        public async Task LogoutAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(Settings.FacebookUserId))
                    await _auth.Logout(Client);

                await App.ApiService.Logout("bearer " + Settings.AuthToken);
            }
            catch (Exception ex)
            {
                await App.ExceptionHandler.Handle(ex);
            }
            finally
            {
                Settings.Clear();
            }
        }
    }
}
