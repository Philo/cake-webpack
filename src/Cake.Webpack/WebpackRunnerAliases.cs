using System;
using Cake.Core;
using Cake.Core.Annotations;
using LogLevel = Cake.Core.Diagnostics.LogLevel;

namespace Cake.Webpack
{
    /// <summary>
    /// Provides a wrapper around Webpack functionality within a Cake build script
    /// </summary>
    [CakeAliasCategory("Webpack")]
    public static class WebpackRunnerAliases
    {
        /// <summary>
        /// Webpack alias
        /// </summary>
        /// <example>
        /// <para>Cake task:</para>
        /// <code>
        /// <![CDATA[
        /// Task("Webpack")
        /// .Does(() =>
        /// {
        ///     Webpack.Global();
        ///     Webpack.FromPath(".").Global();
        ///     Webpack.Global(s => s.WithBuildMode(WebpackBuildMode.Production));
        ///     Webpack.Global(s => s.OutputAsJson());
        ///     Webpack.Global(s => s.WithArguments("in.js out.js --verbose"));
        /// 
        ///     Webpack.Local();
        ///     Webpack.FromPath(".").Local();
        ///     Webpack.Local(s => s.SetPathToWebpackJs("some-other-directory/node_modules/webpack.bin.webpack.js"));        
        ///     Webpack.Local(s => s.WithBuildMode(WebpackBuildMode.Production));
        ///     Webpack.Local(s => s.OutputAsJson());
        ///     Webpack.Local(s => s.WithArguments("in.js out.js --verbose"));        
        /// });
        /// ]]>
        /// </code>
        /// </example>
        /// <param name="context">the cake context</param>
        /// <returns>webpack runner options</returns>
        /// <exception cref="ArgumentNullException"></exception>
        [CakePropertyAlias]
        public static WebpackRunnerFactory Webpack(this ICakeContext context)
        {
            if(context == null) throw new ArgumentNullException(nameof(context));

            return new WebpackRunnerFactory(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
        }
    }
} 