using SimEd.Models.Languages.Common;

namespace SimEd.Models.Languages.Languages;

public class CsDeclarationsExtraction : IDeclarationsExtraction
{
    public bool IsFileMatcher(string fileName)
    {
        string extension = Path.GetExtension(fileName);
        return extension == ".cs";
    }
}