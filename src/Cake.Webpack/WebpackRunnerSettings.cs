using System;
using System.IO;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Webpack
{
    /// <summary>
    /// Webpack settings
    /// </summary>
    public class WebpackRunnerSettings : ToolSettings
    {
        /// <summary>
        /// The file system
        /// </summary>
        protected readonly IFileSystem FileSystem;

        internal WebpackRunnerSettings(IFileSystem fileSystem)
        {
            FileSystem = fileSystem;
        }

        /// <summary>
        /// Create a new instance of the <see cref="WebpackRunnerSettings"/> class.
        /// </summary>
        public WebpackRunnerSettings()
        {

        }

        /// <summary>
        /// The config file to run
        /// </summary>
        public FilePath ConfigFile { get; private set; }

        /// <summary>
        /// specifies the --json switch
        /// </summary>
        public bool OutputJson { get; private set; }

        /// <summary>
        /// specifies the build mode
        /// </summary>
        public WebpackBuildMode? BuildMode { get; private set; }

        /// <summary>
        /// Argument string to pass to Webpack
        /// </summary>
        public string Arguments { get; private set; }

        /// <summary>
        /// The Webpackfile to use
        /// </summary>
        /// <param name="configFile">path to webpack config file</param>
        /// <returns>the settings</returns>
        public WebpackRunnerSettings FromConfig(FilePath configFile)
        {
            if (configFile.GetExtension() != ".js") throw new ArgumentException($"{nameof(configFile)} should be a javascript file with the .js extension");
            if (!FileSystem.Exist(configFile)) throw new FileNotFoundException($"{nameof(configFile)} not found", configFile.FullPath);

            ConfigFile = configFile;

            return this;
        }

        /// <summary>
        /// Whether to append the --json switch
        /// </summary>
        /// <param name="enable"></param>
        /// <returns></returns>
        public WebpackRunnerSettings OutputAsJson(bool enable = true)
        {
            OutputJson = enable;
            return this;
        }
        
        /// <summary>
        /// Whether to specifies either -d or -p switch
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public WebpackRunnerSettings WithBuildMode(WebpackBuildMode mode)
        {
            BuildMode = mode;
            return this;
        }

        /// <summary>
        /// The argument string to pass to Webpack
        /// </summary>
        /// <param name="arguments">an argument string</param>
        /// <returns>the settings</returns>
        public WebpackRunnerSettings WithArguments(string arguments)
        {
            Arguments = arguments;
            return this;
        }

        private void EvaluateBuildMode(ProcessArgumentBuilder args)
        {
            if (BuildMode.HasValue)
            {
                switch (BuildMode)
                {
                    case WebpackBuildMode.Development:
                        args.Append("-d");
                        break;
                    case WebpackBuildMode.Production:
                        args.Append("-p");
                        break;
                }
            }
        }

        internal void Evaluate(ProcessArgumentBuilder args)
        {
            if (ConfigFile != null) args.AppendSwitchQuoted("--config", ConfigFile.FullPath);
            if (OutputJson) args.Append("--json");
            EvaluateBuildMode(args);
            if (!string.IsNullOrWhiteSpace(Arguments)) args.Append(Arguments);
            EvaluateCore(args);
        }

        /// <summary>
        /// evaluate options
        /// </summary>
        /// <param name="args"></param>
        protected virtual void EvaluateCore(ProcessArgumentBuilder args)
        {

        }
    }
}