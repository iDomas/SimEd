// See https://aka.ms/new-console-template for more information

ProjectBundler bundler = new ProjectBundler();

Directory.SetCurrentDirectory(@"C:\oss\SimEd\");

string[] platforms = ["win-arm64", "win-x64", "win-x86"];

await Task.WhenAll(platforms.Select(async platform =>
{
    await bundler.Bundle("SimEd", platform).ConfigureAwait(false);
})).ConfigureAwait(false);

Console.WriteLine("Hello, World!");