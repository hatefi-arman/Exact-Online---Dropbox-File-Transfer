using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using DotNetOpenAuth.OAuth2;
using ExactOnline.Client.Sdk.Controllers;
using FileTransfer.Core.Web;

namespace FileTransfer.Service
{
	public class WebClientServiceBase : IDisposable
	{
		#region Fields

		private WebClient _webClient;
		private readonly IAuthorizationState _authorizationState;

        private readonly ExactOnlineClient _client;

        #endregion

        #region Properties

        protected static Uri ExactOnlineUrl
		{
			get
			{
				var baseUri = ConfigurationHelper.ExactOnlineBaseUrl;

				return new Uri(baseUri.EndsWith("/") ? baseUri : baseUri + "/");
			}
		}

		protected WebClient WebClient
		{
			get
			{
				if (_webClient == null)
				{
					_webClient = new WebClient();

					_webClient.Headers.Add("Content-type", "application/json");
					_webClient.Headers.Add("Accept", "application/json");

					if (_authorizationState != null)
					{
						_webClient.Headers.Add("Authorization", "Bearer " + _authorizationState.AccessToken);
					}
				}
				return _webClient;
			}
		}

        protected ExactOnlineClient Client {
            get
            {
                return _client;
            }
        }

        #endregion

        #region Constructor

        public WebClientServiceBase(IAuthorizationState authorizationState)
		{
			_authorizationState = authorizationState;

            _client = new ExactOnlineClient(ExactOnlineUrl.ToString(), () => authorizationState.AccessToken);
        }

        #endregion

        #region Methods

        protected static string GetErrorMessage(WebException exception)
		{
			string message = exception.Message + Environment.NewLine;

			WebResponse response = exception.Response;
			if (response != null)
			{
				using (Stream data = response.GetResponseStream())
				{
					if (data != null)
					{
						string jsonString = new StreamReader(data).ReadToEnd();
						if (!string.IsNullOrEmpty(jsonString) && jsonString.StartsWith("{"))
						{
							var serializer = new JavaScriptSerializer();
							var jsonObject = serializer.DeserializeObject(jsonString) as Dictionary<string, object>;
							if (jsonObject != null && jsonObject.ContainsKey("error"))
							{
								var errorDictionary = jsonObject["error"] as Dictionary<string, object>;
								if (errorDictionary != null && errorDictionary.ContainsKey("message"))
								{
									var messageDictionary = errorDictionary["message"] as Dictionary<string, object>;
									if (messageDictionary != null && messageDictionary.ContainsKey("value"))
									{
										message += messageDictionary["value"] as string;
									}
								}
							}
						}
					}
				}
			}
			return message;
		}

		#endregion

		#region IDisposable

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				// dispose managed resources
				if (_webClient != null)
				{
					_webClient.Dispose();
				}
			}
		}

		#endregion
	}
}