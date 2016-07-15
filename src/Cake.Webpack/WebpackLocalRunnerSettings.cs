using System.IO;
using Cake.Core.IO;

namespace Cake.Webpack
{
    /// <summary>
    /// Webpack settings specific to local webpack execution
    /// </summary>
    public class WebpackLocalRunnerSettings : WebpackRunnerSettings
    {
        /// <summary>
        /// Webpack settings specific to local webpack execution
        /// </summary>
        public WebpackLocalRunnerSettings() { }

        internal WebpackLocalRunnerSettings(IFileSystem fileSystem) : base(fileSystem) { }

        /// <summary>
        /// Path to local webpack installation
        /// </summary>
        public FilePath PathToWebpackJs { get; private set; } = "./node_modules/webpack/bin/webpack.js";

        /// <summary>
        /// Sets the location of local webpack node installation
        /// </summary>
        /// <param name="webpackJs">Path to local webpack installation</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException">when path not found</exception>
        public WebpackLocalRunnerSettings SetPathToWebpackJs(FilePath webpackJs)
        {
            if (!FileSystem.Exist(webpackJs)) throw new FileNotFoundException("webpack not found", webpackJs.FullPath);
            PathToWebpackJs = webpackJs;
            return this;
        }
    }
}