using System.IO;
using Dock.Model.Mvvm.Controls;

namespace SimEd.ViewModels.Solution;

public class SolutionViewModel : Tool
{
    public string SolutionPath { get; set; } = Directory.GetCurrentDirectory();

    public async void OnSolutionChosen()
    {
    }
}