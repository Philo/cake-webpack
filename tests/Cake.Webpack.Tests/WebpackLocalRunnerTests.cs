using System;
using System.IO;

using Cake.Core.IO;
using Cake.Testing;

using Shouldly;

using Xunit;

namespace Cake.Webpack.Tests {
	public class WebpackLocalRunnerTests {
		private readonly WebpackLocalRunnerFixture fixture;
		private readonly string WebpackFile;
        private const string pathToWebpackJs = "node_modules/webpack/bin/webpack.js";

        public WebpackLocalRunnerTests()
		{
			fixture = new WebpackLocalRunnerFixture();
			WebpackFile = "../Webpackfile.js";
			
			fixture.FileSystem.CreateFile(pathToWebpackJs);
			fixture.FileSystem.CreateFile("/abc");
			fixture.FileSystem.CreateFile(WebpackFile);
            fixture.FileSystem.CreateFile("path-to-Webpack/webpack.js");
        }

		[Fact]
		public void Install_Settings_With_Webpack_File_Should_Add_Webpackfile_Argument()
		{
			fixture.InstallSettings = s => s.FromConfig(WebpackFile);

			var result = fixture.Run();

			result.Args.ShouldBe($"\"{pathToWebpackJs}\" --config \"{WebpackFile}\"");

        }

		[Fact]
		public void No_Install_Settings_Specified_Should_Execute_Command_Without_Arguments()
		{
			fixture.InstallSettings = null;

			var result = fixture.Run();

			result.Args.ShouldBe($"\"{pathToWebpackJs}\"");
        }

        [Fact]
        public void Custom_Webpack_Path()
        {
            fixture.InstallSettings = s => s.SetPathToWebpackJs("path-to-Webpack/webpack.js");
            var result = fixture.Run();

            result.Args.ShouldBe("\"path-to-Webpack/webpack.js\"");
        }
    }
}