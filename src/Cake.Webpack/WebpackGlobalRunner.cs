using System;
using System.Collections.Generic;
using System.IO;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Webpack
{
    /// <summary>
    /// webpack runner foir global node installation
    /// </summary>
    public class WebpackGlobalRunner : WebpackRunner<WebpackRunnerSettings>
    {
        private readonly IFileSystem _fileSystem;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileSystem">the filesystem</param>
        /// <param name="environment">the environment</param>
        /// <param name="processRunner">the process runner</param>
        /// <param name="tools">the tools locator</param>
        public WebpackGlobalRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools) : base(fileSystem, environment, processRunner, tools)
        {
            _fileSystem = fileSystem;
        }

        /// <summary>
        /// Executes Webpack
        /// </summary>
        public override void Execute(Action<WebpackRunnerSettings> configure = null)
        {
            var settings = new WebpackRunnerSettings(_fileSystem);
            configure?.Invoke(settings);

            var args = new ProcessArgumentBuilder();
            settings.Evaluate(args);
            Run(settings, args);
        }

        /// <summary>
        /// Gets the possible names of the tool executable.
        /// </summary>
        /// <returns>
        /// The tool executable name.
        /// </returns>
        protected override IEnumerable<string> GetToolExecutableNames()
        {
            yield return "webpack.cmd";
            yield return "webpack";
        }
    }
}