using System;
using Microsoft.Extensions.Configuration;

// ReSharper disable UseStringInterpolation

namespace OpenMagic
{
    public class AppSettings(Func<IConfigurationSection> appSettingsFactory)
    {
        private readonly string _appSettingsPrefix = "";
        private readonly string _appSettingsPrefixDelimiter = "_";

        public AppSettings() : this(GetAppSettings)
        {
        }

        public AppSettings(string appSettingsPrefix) : this(appSettingsPrefix, "_", GetAppSettings)
        {
        }

        public AppSettings(string appSettingsPrefix, string appSettingsPrefixDelimiter) : this(appSettingsPrefix, appSettingsPrefixDelimiter, GetAppSettings)
        {
        }

        public AppSettings(string appSettingsPrefix, string appSettingsPrefixDelimiter, Func<IConfigurationSection> appSettingsFactory) : this(appSettingsFactory)
        {
            _appSettingsPrefix = appSettingsPrefix;
            _appSettingsPrefixDelimiter = appSettingsPrefixDelimiter;
        }

        public bool GetBoolean(string key, bool throwExceptionIsKeyNotFound = true)
        {
            var value = GetString(key, throwExceptionIsKeyNotFound);

            try
            {
                return bool.Parse(value!);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("AppSettings[{0}] must be a boolean.", GetFullKey(key)), exception);
            }
        }


        public string? GetString(string key, bool throwExceptionIfKeyNotFound = true)
        {
            var fullKey = GetFullKey(key);
            var appSettings = appSettingsFactory();
            var value = appSettings[fullKey];

            if (!string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            if (throwExceptionIfKeyNotFound)
            {
                throw new Exception(string.Format("AppSettings[{0}] must be defined. Maybe you need to create AppSettings.config. Maybe there is an AppSettings.Example.config file to copy.", fullKey));
            }

            return null;
        }

        private string GetFullKey(string key)
        {
            return string.Format("{0}{1}{2}", _appSettingsPrefix, _appSettingsPrefixDelimiter, key);
        }

        private static IConfigurationSection GetAppSettings()
        {
            var configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("AppSettings.json")
                .Build();

            var appSettings = configurationRoot.GetSection("AppSettings");

            return appSettings;
        }
    }
}