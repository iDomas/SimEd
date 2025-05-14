using System.Collections.Generic;
using TextMateSharp.Grammars;

namespace SimEd;

public class LanguageChooser : ILanguageChooser
{
    private readonly Dictionary<string, string> _languages = [];
    public string ScopeNameOfLanguage(string language)
    {
        if (_languages.TryGetValue(language, out string scopeName))
        {
            return scopeName;
        }
        var registryOptions = new RegistryOptions(ThemeName.Light);
        Language csharpLanguage = registryOptions.GetLanguageByExtension($".{language}");
        string newScope = registryOptions.GetScopeByLanguageId(csharpLanguage.Id);
        _languages.Add(language, newScope);
        return newScope;
    }
}