using System;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Webpack
{
    /// <summary>
    /// Base Webpack runner
    /// </summary>
    public abstract class WebpackRunner<TSettings> : NodeToolRunner<TSettings> where TSettings : WebpackRunnerSettings
    {
        /// <summary>
        /// creates a new Webpack runner
        /// </summary>
        /// <param name="fileSystem">the file system</param>
        /// <param name="environment">The cake environment</param>
        /// <param name="processRunner">The cake process runner</param>
        /// <param name="tools">The tools locator</param>
        protected WebpackRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools) : base(fileSystem, environment, processRunner, tools)
        {
        }

        /// <summary>
        /// Gets the name of the tool.
        /// </summary>
        /// <returns>
        /// The name of the tool.
        /// </returns>
        protected override string GetToolName()
        {
            return "Webpack";
        }

        /// <summary>
        /// Executes Webpack
        /// </summary>
        public abstract void Execute(Action<TSettings> configure = null);
    }
}