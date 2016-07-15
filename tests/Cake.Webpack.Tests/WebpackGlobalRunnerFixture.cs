using System;

using Cake.Testing.Fixtures;

namespace Cake.Webpack.Tests {
	public class WebpackGlobalRunnerFixture : ToolFixture<WebpackRunnerSettings> {
		public WebpackGlobalRunnerFixture() : base("webpack") { }

		public Action<WebpackRunnerSettings> InstallSettings { get; set; }

		protected override void RunTool() {
			var tool = new WebpackGlobalRunner(FileSystem, Environment, ProcessRunner, Tools);
			tool.Execute(InstallSettings);
		}
	}
}