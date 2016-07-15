using System;
using System.IO;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Webpack
{
    /// <summary>
    /// Returns a Webpack runner based on either a local or global Webpack installation via npm
    /// </summary>
    public class WebpackRunnerFactory
    {
        private readonly IFileSystem _fileSystem;
        private readonly ICakeEnvironment _environment;
        private readonly IProcessRunner _processRunner;
        private readonly IToolLocator _tools;
        private DirectoryPath _workingDirectoryPath;

        internal WebpackRunnerFactory(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner,
            IToolLocator tools)
        {
            _fileSystem = fileSystem;
            _environment = environment;
            _processRunner = processRunner;
            _tools = tools;
        }

        /// <summary>
        /// Sets the working directory when webpack is executed
        /// </summary>
        /// <param name="workingDirectoryPath"></param>
        /// <returns></returns>
        public WebpackRunnerFactory FromPath(DirectoryPath workingDirectoryPath)
        {
            _workingDirectoryPath = workingDirectoryPath;
            return this;
        }

        /// <summary>
        /// Get a Webpack local runner based on a local Webpack installation, a local installation is achieved through `npm install Webpack`
        /// </summary>
        /// <param name="configure"></param>
        public void Local(Action<WebpackLocalRunnerSettings> configure = null)
        {
            var runner = new WebpackLocalRunner(_fileSystem, _environment, _processRunner, _tools);
            if (_workingDirectoryPath != null) runner.FromPath(_workingDirectoryPath);
            runner.Execute(configure);
        }

        /// <summary>
        /// Get a Webpack global runner based on a global Webpack installation, a global installation is achieved through `npm install Webpack -g`
        /// </summary>
        /// <param name="configure"></param>
        public void Global(Action<WebpackRunnerSettings> configure = null)
        {
            var runner = new WebpackGlobalRunner(_fileSystem, _environment, _processRunner, _tools);
            if (_workingDirectoryPath != null) runner.FromPath(_workingDirectoryPath);
            runner.Execute(configure);
        }
    }
}