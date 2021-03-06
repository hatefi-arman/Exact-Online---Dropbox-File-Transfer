﻿using System.Web.Configuration;

namespace FileTransfer.Core.Web
{
    /// <summary>
    /// A centralized helper class for managing application configuration in Web.config
    /// </summary>
    public static class ConfigurationHelper
    {
        #region Configuration keys constant values
        //These keys represent the keys to store and retrieve the configured values to/from AppSettings section of Web.config
        private const string EXACT_ONLINE_CLIENT_APP_ID_CONFIG_KEY = "ExactOnlineClientId";
        private const string EXACT_ONLINE_CLIENT_APP_SECRET_CONFIG_KEY = "ExactOnlineClientSecret";
        private const string EXACT_ONLINE_BASE_URL_CONFIG_KEY = "ExactOnlineBaseUrl";

        private const string DROPBOX_APP_KEY_CONFIG_KEY = "DropboxAppKey";
        private const string DROPBOX_APP_SECRET_CONFIG_KEY = "DropboxAppSecret";
        private const string DROPBOX_ACCESS_TOKEN_CONFIG_KEY = "DropboxAccessToken";

        #endregion


        #region Public properties

        public static string ExactOnlineClientId
        {
            get
            {
                return WebConfigurationManager.AppSettings[EXACT_ONLINE_CLIENT_APP_ID_CONFIG_KEY];
            }
        }
        public static string ExactOnlineClientSecret
        {
            get
            {
                return WebConfigurationManager.AppSettings[EXACT_ONLINE_CLIENT_APP_SECRET_CONFIG_KEY];
            }
        }
        public static string ExactOnlineBaseUrl
        {
            get
            {
                return WebConfigurationManager.AppSettings[EXACT_ONLINE_BASE_URL_CONFIG_KEY];
            }
        }

        public static string DropboxAppKey
        {
            get
            {
                return WebConfigurationManager.AppSettings[DROPBOX_APP_KEY_CONFIG_KEY];
            }
        }
        public static string DropboxAppSecret
        {
            get
            {
                return WebConfigurationManager.AppSettings[DROPBOX_APP_SECRET_CONFIG_KEY];
            }
        }
        public static string DropboxAccessToken
        {
            get
            {
                return WebConfigurationManager.AppSettings[DROPBOX_ACCESS_TOKEN_CONFIG_KEY];
            }
        }

        #endregion

        /// <summary>
        /// Stores the configiration details in AppSettings section of the Web.config.
        /// </summary>
        public static void UpdateSettings(
            string exactOnlineClientId,
            string exactOnlineClientSecret,
            string exactOnlineBaseUrl,
            string dropboxAppKey,
            string dropboxAppSecret,
            string dropboxAccessToken
            )
        {
            var configuration = WebConfigurationManager.OpenWebConfiguration("~");

            configuration.AppSettings.Settings.Remove(EXACT_ONLINE_CLIENT_APP_ID_CONFIG_KEY);
            configuration.AppSettings.Settings.Add(EXACT_ONLINE_CLIENT_APP_ID_CONFIG_KEY, exactOnlineClientId);

            configuration.AppSettings.Settings.Remove(EXACT_ONLINE_CLIENT_APP_SECRET_CONFIG_KEY);
            configuration.AppSettings.Settings.Add(EXACT_ONLINE_CLIENT_APP_SECRET_CONFIG_KEY, exactOnlineClientSecret);

            configuration.AppSettings.Settings.Remove(EXACT_ONLINE_BASE_URL_CONFIG_KEY);
            configuration.AppSettings.Settings.Add(EXACT_ONLINE_BASE_URL_CONFIG_KEY, exactOnlineBaseUrl);


            configuration.AppSettings.Settings.Remove(DROPBOX_APP_KEY_CONFIG_KEY);
            configuration.AppSettings.Settings.Add(DROPBOX_APP_KEY_CONFIG_KEY, dropboxAppKey);

            configuration.AppSettings.Settings.Remove(DROPBOX_APP_SECRET_CONFIG_KEY);
            configuration.AppSettings.Settings.Add(DROPBOX_APP_SECRET_CONFIG_KEY, dropboxAppSecret);

            configuration.AppSettings.Settings.Remove(DROPBOX_ACCESS_TOKEN_CONFIG_KEY);
            configuration.AppSettings.Settings.Add(DROPBOX_ACCESS_TOKEN_CONFIG_KEY, dropboxAccessToken);

            configuration.Save();
        }
    }
}