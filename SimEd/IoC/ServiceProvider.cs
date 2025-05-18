using Avalonia.Controls.Templates;
using Jab;
using SimEd.Common.Interfaces;
using SimEd.Common.Mediator;
using SimEd.IoC.Tools;
using SimEd.Models;
using SimEd.ViewModels;
using SimEd.ViewModels.Solution;

namespace SimEd.IoC;

[ServiceProvider]
[Singleton<IMiniPubSub, MiniPubSub>()]
[Singleton<IDataTemplate, ViewLocator>]
[Singleton(typeof(NotepadFactory))]
[Singleton<IInjector, InjectorHost>]
[Singleton<IAppSettingsReader, AppSettingsReader>]
[Singleton(typeof(SolutionViewModel))]
[Singleton(typeof(MainWindowViewModel))]
public partial class ServiceProvider
{
}