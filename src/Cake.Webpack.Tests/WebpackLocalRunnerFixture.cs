using System;

using Cake.Testing.Fixtures;

namespace Cake.Webpack.Tests {
	public class WebpackLocalRunnerFixture : ToolFixture<WebpackLocalRunnerSettings>
	{
		public WebpackLocalRunnerFixture() : base("node") {}

		public Action<WebpackLocalRunnerSettings> InstallSettings { get; set; }

		protected override void RunTool()
		{
			var tool = new WebpackLocalRunner(FileSystem, Environment, ProcessRunner, Tools);
			tool.Execute(InstallSettings);
		}
	}
}