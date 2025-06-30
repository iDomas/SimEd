namespace SimEd.Models.FileChoosers;

public interface IFileDialogChooser
{
    public Task<string?> ChooseDirectory();
}