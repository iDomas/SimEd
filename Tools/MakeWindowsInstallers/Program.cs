﻿// See https://aka.ms/new-console-template for more information

using MakeWindowsInstallers;

string pathOfSolution = OsDetector.GetParentOfSolution();

Directory.SetCurrentDirectory(pathOfSolution);

OsKind platformKind = OsDetector.DetectOsKind;
Dictionary<OsKind, string[]> platformsDict = new()
{
    { OsKind.Windows, ["win-arm64", "win-x64", "win-x86"] },
    { OsKind.Linux, ["linux-x64"] },
    { OsKind.MacOs, ["macos-arm64"] }
};
string[] platforms = platformsDict[platformKind];

await Task.WhenAll(
        platforms.Select(async platform => { await ProjectBundler.Bundle("SimEd", platform).ConfigureAwait(false); }))
    .ConfigureAwait(false);
