#addin "Cake.Npm"
#r "artifacts/build/Cake.Webpack.dll"

private void EnsureTestFile(string input = "app.js") {
    if (!System.IO.File.Exists(input))
    {
        using (var wtr = System.IO.File.CreateText(input))
        {
            wtr.WriteLine("console.log(\"Hello world!\")");
            wtr.Flush();
            wtr.Close();
        }
    }
}

Task("Global")
    .Does(() => 
    {
        Npm.WithLogLevel(NpmLogLevel.Silent).FromPath(".").Install(p => p.Package("webpack").Globally());
        Webpack.FromPath(".").Global();
    });

Task("Local")
    .Does(() => 
    {
        Npm.WithLogLevel(NpmLogLevel.Silent).FromPath(".").Install(p => p.Package("webpack"));
        Webpack.FromPath(".").Local();
    });    

Task("CustomArgs")
    .Does(() => 
    {
        var input = "app.js";
        var bundle = "app.bundle.js";
        var args = string.Format("{0} {1}", input, bundle);

        EnsureTestFile(input);
        DeleteFile(bundle);
        Npm.WithLogLevel(NpmLogLevel.Silent).FromPath(".").Install(p => p.Package("webpack"));
        Webpack.FromPath(".").Local(w => w.WithArguments(args));
    });  

RunTarget(Argument("target", "Global"))