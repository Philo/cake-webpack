using System;
using System.IO;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Webpack
{
    /// <summary>
    /// Webpack runner for local node installation
    /// </summary>
    public class WebpackLocalRunner : WebpackRunner<WebpackLocalRunnerSettings>
    {
        private readonly IFileSystem _fileSystem;

        /// <summary>
        /// Webpack runner for local node installation
        /// </summary>
        /// <param name="fileSystem">the file system</param>
        /// <param name="environment">the cake environment</param>
        /// <param name="processRunner">the process runner</param>
        /// <param name="tools">the tools locator</param>
        public WebpackLocalRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools) : base(fileSystem, environment, processRunner, tools)
        {
            _fileSystem = fileSystem;
        }

        /// <summary>
        /// Executes Webpack
        /// </summary>
        public override void Execute(Action<WebpackLocalRunnerSettings> configure = null)
        {
            var settings = new WebpackLocalRunnerSettings(_fileSystem);

            configure?.Invoke(settings);

            if (!_fileSystem.Exist(settings.PathToWebpackJs)) throw new FileNotFoundException($"unable to find local webpack installation at specified path [{settings.PathToWebpackJs}], have you ran 'npm install webpack'?");

            var args = new ProcessArgumentBuilder();
            args.AppendQuoted(settings.PathToWebpackJs.ToString());
            settings.Evaluate(args);
            Run(settings, args);
        }
    }
}