using System.Text;

namespace SimEd.Models.Languages.Common;

public record struct BytesArea (byte[] Data, int Start, int Len)
{
    public override string ToString()
    {
        byte[] data = Data.AsSpan(Start, Len).ToArray();
        return Encoding.UTF8.GetString(data);
    }
}