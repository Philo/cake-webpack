# Build Script

You can reference Cake.Webpack in your build script as a cake addin:

```cake
#addin "Cake.Webpack"
```

or nuget reference:

```cake
#addin "nuget:https://www.nuget.org/api/v2?package=Cake.Webpack"
```

Then some examples:

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