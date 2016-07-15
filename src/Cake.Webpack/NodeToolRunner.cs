using System.Collections.Generic;
using System.IO;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Webpack
{
    /// <summary>
    /// Base Node Tool Runner
    /// </summary>
    /// <typeparam name="TSettings">tool settings</typeparam>
    public abstract class NodeToolRunner<TSettings> : Tool<TSettings> where TSettings : ToolSettings
    {
        private readonly IFileSystem _fileSystem;
        private DirectoryPath _workingDirectory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileSystem">the file system</param>
        /// <param name="environment">the cake environment</param>
        /// <param name="processRunner">the process runner</param>
        /// <param name="tools">the tools locator</param>
        protected NodeToolRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools) : base(fileSystem, environment, processRunner, tools)
        {
            _fileSystem = fileSystem;
        }

        /// <summary>
        /// Gets the possible names of the tool executable.
        /// </summary>
        /// <returns>
        /// The tool executable name.
        /// </returns>
        protected override IEnumerable<string> GetToolExecutableNames()
        {
            yield return "node.exe";
            yield return "node";
            yield return "nodejs";
        }

        /// <summary>
        /// Sets the working directory for npm commands
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public NodeToolRunner<TSettings> FromPath(DirectoryPath path)
        {
            _workingDirectory = path;
            return this;
        }

        /// <summary>
        /// Gets the working directory from the NpmRunnerSettings
        ///             Defaults to the currently set working directory.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>
        /// The working directory for the tool.
        /// </returns>
        protected override DirectoryPath GetWorkingDirectory(TSettings settings)
        {
            if (_workingDirectory == null) return base.GetWorkingDirectory(settings);
            if (!_fileSystem.Exist(_workingDirectory)) throw new DirectoryNotFoundException($"Working directory path not found [{_workingDirectory.FullPath}]");
            return _workingDirectory;
        }
    }
}