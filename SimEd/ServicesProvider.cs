using Jab;

namespace SimEd;
[ServiceProvider]
[Singleton(typeof(ILanguageChooser), typeof(LanguageChooser))]
public partial class ServicesProvider
{
    
}