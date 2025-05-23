using System.Text;

namespace SimEd.ViewModels;

public static class FileTools
{
    public static Encoding GetEncoding(string path)
    {
        using StreamReader reader = new StreamReader(path, Encoding.Default, true);
        if (reader.Peek() >= 0)
        {
            reader.Read();
        }

        return reader.CurrentEncoding;
    }

}