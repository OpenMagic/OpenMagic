using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using NullGuard;

namespace OpenMagic
{
    public class AppSettings
    {
        private readonly Func<NameValueCollection> _appSettingsFactory;
        private readonly string _appSettingsPrefix;
        private readonly string _appSettingsPrefixDelimiter;

        public AppSettings()
            : this(() => ConfigurationManager.AppSettings)
        {
        }

        public AppSettings(string appSettingsPrefix)
            : this(appSettingsPrefix, "_", () => ConfigurationManager.AppSettings)
        {
        }

        public AppSettings(string appSettingsPrefix, string appSettingsPrefixDelimiter)
            : this(appSettingsPrefix, appSettingsPrefixDelimiter, () => ConfigurationManager.AppSettings)
        {
        }

        public AppSettings(Func<NameValueCollection> appSettingsFactory)
        {
            _appSettingsFactory = appSettingsFactory;
        }

        public AppSettings(string appSettingsPrefix, string appSettingsPrefixDelimiter, Func<NameValueCollection> appSettingsFactory)
            : this(appSettingsFactory)
        {
            _appSettingsPrefix = appSettingsPrefix;
            _appSettingsPrefixDelimiter = appSettingsPrefixDelimiter;
        }

        public bool GetBoolean(string key, bool throwExceptionIsKeyNotFound = true, bool throwExceptionIsValueIsNullOrWhitespace = true)
        {
            var value = GetString(key, throwExceptionIsKeyNotFound, throwExceptionIsValueIsNullOrWhitespace);

            try
            {
                return bool.Parse(value);
            }
            catch (Exception exception)
            {
                throw new Exception($"AppSettings[{GetFullKey(key)}] must be a boolean.", exception);
            }
        }

        [return: AllowNull]
        public string GetString(string key, bool throwExceptionIfKeyNotFound = true, bool throwExceptionIsValueIsNullOrWhitespace = true)
        {
            var fullKey = GetFullKey(key);
            var appSettings = _appSettingsFactory();
            var value = appSettings[fullKey];

            if (!string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            if (appSettings.AllKeys.Contains(fullKey))
            {
                if (throwExceptionIsValueIsNullOrWhitespace)
                {
                    throw new Exception($"AppSettings[{fullKey}] cannot be null or whitespace.");
                }
                return value;
            }

            if (throwExceptionIfKeyNotFound)
            {
                throw new Exception($"AppSettings[{fullKey}] must be defined. Maybe you need to create AppSettings.config. Maybe there is an AppSettings.Example.config file to copy.");
            }
            return null;
        }

        private string GetFullKey(string key)
        {
            return $"{_appSettingsPrefix}{_appSettingsPrefixDelimiter}{key}";
        }
    }
}