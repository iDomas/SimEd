using Jab;
using SimEd.Common.Mediator;

namespace SimEd;

[ServiceProvider]
[Singleton<IMiniPubSub, MiniPubSub>()]
[Singleton(typeof(ILanguageChooser), typeof(LanguageChooser))]
public partial class ServicesProvider
{
}