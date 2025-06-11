// See https://aka.ms/new-console-template for more information

var bundler = new ProjectBundler();

Directory.SetCurrentDirectory(@"C:\oss\SimEd\");

string[] platforms = ["win-arm64", "win-x64", "win-x86"];
foreach (string platform in platforms)
{
    await bundler.Bundle("SimEd", platform);
}


Console.WriteLine("Hello, World!");