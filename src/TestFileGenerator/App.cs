// ***********************************************************************
// Assembly         : TestFileGenerator
// Author           : Rku
// Created          : 12-30-2020
// ***********************************************************************
namespace TestFileGenerator
{
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Models;
    using Services;

    /// <summary>
    /// Class App.
    /// </summary>
    public class App
    {
        private readonly ITestFileService testFileService;
        private readonly ILogger<App> _logger;
        private readonly AppSettings _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        /// <param name="testFileService">The test file service.</param>
        /// <param name="config">The configuration.</param>
        /// <param name="logger">The logger.</param>
        public App(ITestFileService testFileService,
            IOptions<AppSettings> config,
            ILogger<App> logger)
        {
            this.testFileService = testFileService;
            _logger = logger;
            _config = config.Value;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            _logger.LogInformation($"This is a console application for {_config.ConsoleTitle}");
            testFileService.Run();
            System.Console.ReadKey();
        }
    }
}
