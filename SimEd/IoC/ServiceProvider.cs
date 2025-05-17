using Avalonia.Controls.Templates;
using Dock.Model.Mvvm;
using Jab;
using SimEd.Common.Mediator;
using SimEd.ViewModels;
using SimEd.Views;

namespace SimEd.IoC;

[ServiceProvider]
[Singleton<IMiniPubSub, MiniPubSub>()]
[Singleton(typeof(ILanguageChooser), typeof(LanguageChooser))]
[Singleton<IDataTemplate, ViewLocator>]
[Singleton(typeof(NotepadFactory))]
[Singleton<IGetService, GetServiceHost>]
[Singleton(typeof(MainWindowViewModel))]
public partial class ServiceProvider
{
}