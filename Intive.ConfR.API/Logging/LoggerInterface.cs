using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Extensions.Logging;
using System;
using System.IO;

namespace Intive.ConfR.Logging
{
    /// <summary>
    /// Interface for logging 
    /// </summary>
    public interface ILoggerManager
    {
        /// <summary>
        /// Creates log with detailed message
        /// </summary>
        /// <param name="message"></param>
        void LogTrace(string message);

        /// <summary>
        /// Creates log with general message
        /// </summary>
        /// <param name="message"></param>
        void LogInfo(string message);

        /// <summary>
        /// Creates log with error message
        /// </summary>
        /// <param name="message"></param>
        void LogError(string message);
    }

    /// <summary>
    /// Interface implementation
    /// </summary>
    public class LoggerManager : ILoggerManager
    {
        private static ILogger _logger = LogManager.GetCurrentClassLogger();

        public LoggerManager()
        { }

        public void LogTrace(string message)
        {
            if (!_logger.IsTraceEnabled)
            {
                return;
            }

            _logger.Trace(message);
        }

        public void LogInfo(string message)
        {
            if (!_logger.IsInfoEnabled)
            {
                return;
            }

            _logger.Info(message);
        }      
        
        public void LogError(string message)
        {
            if (!_logger.IsErrorEnabled)
            {
                return;
            }

            _logger.Error(message);
        }

        public static void Initialize(IConfiguration configuration, IHostingEnvironment env)
        {
            ConfigSettingLayoutRenderer.DefaultConfiguration = new ConfigurationBuilder()
                .AddJsonFile(string.Concat(Directory.GetCurrentDirectory(), "/appsettings.json"))
                .Build();

            var logPath = configuration.GetValue<string>(env.IsDevelopment() ? "NLogConfig:Development:Path" : "NLogConfig:Production:Path")
                .Replace('\\', Path.DirectorySeparatorChar);

            var path =  string.Concat(Environment.CurrentDirectory, logPath);

            LogManager.LoadConfiguration(path);
        }
    }
}
