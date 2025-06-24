namespace SimEd.Models.Languages.Common;

public interface IDeclarationsExtraction
{
    bool IsFileMatcher(string fileName);
    
    SolutionIndexItem[] ExtractFileDefinitions(string fileName, char[] fileData);
}
