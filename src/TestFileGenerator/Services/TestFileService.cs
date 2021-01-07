// ***********************************************************************
// Assembly         : TestFileGenerator
// Author           : Rku
// Created          : 12-30-2020
// ***********************************************************************
namespace TestFileGenerator.Services
{
    using System;
    using System.IO;
    using System.Threading;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Models;

    /// <summary>
    /// Interface ITestFileService
    /// Implements the <see cref="System.IDisposable" />
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface ITestFileService
    {
        /// <summary>
        /// Runs this instance.
        /// </summary>
        void Run();
    }

    /// <summary>
    /// Class TestFileService.
    /// Implements the <see cref="TestFileGenerator.Services.ITestFileService" />
    /// </summary>
    /// <seealso cref="TestFileGenerator.Services.ITestFileService" />
    public class TestFileService : ITestFileService
    {
        private readonly ILogger<TestFileService> _logger;
        private readonly AppSettings _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestFileService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="config">The configuration.</param>
        public TestFileService(ILogger<TestFileService> logger,
                               IOptions<AppSettings> config)
        {
            _logger = logger;
            _config = config.Value;
        }

        /// <inheritdoc />
        public void Run()
        {
            _logger.LogWarning($"Wow! We are now in the test service of: {_config.ConsoleTitle}");


            if (_config.ClearOnRestart && File.Exists(_config.FileToGenerate))
            {
                File.Delete(_config.FileToGenerate);
                File.WriteAllText(_config.FileToGenerate, string.Empty);
            }

            if (!File.Exists(_config.FileToGenerate))
            {
                var fs = File.Create(_config.FileToGenerate);
                fs.Close();
            }

            while (true)
            {
                File.AppendAllText(_config.FileToGenerate, $"{DateTime.Now:s}, info(this is a testlogentry), guid({Guid.NewGuid()}) {Environment.NewLine}");
                _logger.LogInformation("new line");

                Thread.Sleep(_config.IntervalToGenerate);
                //var asdf = Console.ReadLine();
            }

        }

    }
}
