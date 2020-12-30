// ***********************************************************************
// Assembly         : TestFileGenerator
// Author           : Rku
// Created          : 12-30-2020
// ***********************************************************************
namespace TestFileGenerator.Models
{
    using System;

    /// <summary>
    /// Class AppSettings.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Sets header title in console window
        /// </summary>
        /// <value>The console title.</value>
        public string ConsoleTitle { get; set; }

        /// <summary>
        /// Path to the file that gets generated
        /// </summary>
        /// <value>The file to generate.</value>
        public string FileToGenerate { get; set; }

        /// <summary>
        /// Interval where lines are added to the file
        /// </summary>
        /// <value>The interval to generate.</value>
        public TimeSpan IntervalToGenerate { get; set; }

        /// <summary>
        /// Empties the file when the generator is restarted
        /// </summary>
        /// <value><c>true</c> if [clear on restart]; otherwise, <c>false</c>.</value>
        public bool ClearOnRestart { get; set; }
    }
}
