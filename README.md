# cake-webpack
Aliases to assist with running Webpack from Cake build scripts

## Usage

```c#
  #addin "Cake.Webpack"

  Task("Webpack")
  .Does(() =>
  {
     Webpack.Global();
     Webpack.FromPath(".").Global();
     Webpack.Global(s => s.WithBuildMode(WebpackBuildMode.Production));
     Webpack.Global(s => s.OutputAsJson());
     Webpack.Global(s => s.WithArguments("in.js out.js --verbose"));
  
     Webpack.Local();
     Webpack.FromPath(".").Local();
     Webpack.Local(s => s.SetPathToWebpackJs("some-other-directory/node_modules/webpack.bin.webpack.js"));        
     Webpack.Local(s => s.WithBuildMode(WebpackBuildMode.Production));
     Webpack.Local(s => s.OutputAsJson());
     Webpack.Local(s => s.WithArguments("in.js out.js --verbose"));        
  });
```

## Documentation

Thanks to the cakebuild.net site, documentation can be found [here](http://cakebuild.net/api/cake.webpack/)

## Tests

Cake.Webpack is covered by a set of unit tests

## I cant do _insert-option-here_

If you have feature requests please submit them as issues, or better yet as pull requests :)

