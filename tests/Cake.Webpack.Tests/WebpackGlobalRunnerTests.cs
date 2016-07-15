using Cake.Testing;

using Shouldly;

using Xunit;

namespace Cake.Webpack.Tests {
	public class WebpackGlobalRunnerTests {
		private readonly WebpackGlobalRunnerFixture _fixture;
		private readonly string _webpackFile;

		public WebpackGlobalRunnerTests()
		{
			_fixture = new WebpackGlobalRunnerFixture();
			_webpackFile = "../mywebpack.config.js";
			_fixture.FileSystem.CreateFile(this._webpackFile);
		}

		[Fact]
		public void Install_Settings_With_Webpack_Config_File_Should_Add_Config_Argument()
		{
			_fixture.InstallSettings = s => s.FromConfig(_webpackFile);

			var result = _fixture.Run();

			result.Args.ShouldBe($"--config \"{_webpackFile}\"");
		}

		[Fact]
		public void Install_Settings_With_Webpack_Config_File_And_Arguments_Should_Add_Webpack_File_And_Additional_Arguments()
		{
			_fixture.InstallSettings = s => s.FromConfig(_webpackFile).WithArguments("--help");

			var result = _fixture.Run();

			result.Args.ShouldBe($"--config \"{_webpackFile}\" --help");
        }

		[Fact]
		public void No_Install_Settings_Specified_Should_Execute_Command_Without_Arguments()
		{
			_fixture.InstallSettings = null;

			var result = _fixture.Run();

			result.Args.ShouldBe("");
		}

        [Theory]
        [InlineData(true, "--json")]
        [InlineData(false, "")]
        public void Install_Settings_With_OutputAsJson(bool enabled, string args)
        {
            _fixture.InstallSettings = s => s.OutputAsJson(enabled);

            var result = _fixture.Run();

            result.Args.ShouldBe(args);
        }

        [Theory]
        [InlineData(WebpackBuildMode.Development, "-d")]
        [InlineData(WebpackBuildMode.Production, "-p")]
        public void Install_Settings_With_DevelopmentBuildMode(WebpackBuildMode buildMode, string args)
        {
            _fixture.InstallSettings = s => s.WithBuildMode(buildMode);

            var result = _fixture.Run();

            result.Args.ShouldBe(args);
        }
    }
}