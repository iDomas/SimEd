﻿using System.IO.Compression;

namespace WindowsInstallersCreator;

public static class ZipFileCreator
{
    /// <summary>
    /// Create a ZIP file of the files provided.
    /// </summary>
    /// <param name="fileName">The full path and name to store the ZIP file at.</param>
    /// <param name="files">The list of files to be added.</param>
    public static void CreateZipFile(string fileName, IEnumerable<string> files)
    {
        // Create and open a new ZIP file
        using ZipArchive zip = ZipFile.Open(fileName, ZipArchiveMode.Create);
        foreach (string file in files)
        {
            // Add the entry for each file
            zip.CreateEntryFromFile(file, Path.GetFileName(file), CompressionLevel.Optimal);
        }
    }
}