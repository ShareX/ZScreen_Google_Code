using System.IO;
public class TreeFile
{

    private double mSize;
    private string mPath;

    public TreeFile(string filePath)
    {

        this.mPath = filePath;

        if (File.Exists(filePath))
        {
            FileInfo fi = new FileInfo(GetFilePath());
            SetSize(fi.Length);
        }
    }

    public string GetFileExtension()
    {
        return Path.GetExtension(mPath);
    }
    public string GetFilePath()
    {
        return this.mPath;
    }

    public string GetFileName()
    {
        return Path.GetFileName(GetFilePath());
    }

    public string GetFileNameWithoutExtension()
    {
        return Path.GetFileNameWithoutExtension(GetFilePath());
    }
    public void SetSize(double size)
    {
        this.mSize = size;
    }

    public double GetSize(TreeDir.BinaryPrefix prefix)
    {
        switch (prefix)
        {
            case TreeDir.BinaryPrefix.Bytes:
                return mSize;
            case TreeDir.BinaryPrefix.Kibibits:
                return (mSize / 128);
            case TreeDir.BinaryPrefix.Kibibytes:
                return (mSize / 1024);
            case TreeDir.BinaryPrefix.Mebibytes:
                return (mSize / (1024 * 1024));
            case TreeDir.BinaryPrefix.Gibibytes:
                return (mSize / (1024 * 1024 * 1024));
        }
        return mSize;
    }

    public string GetSizeToString(TreeDir.BinaryPrefix prefix)
    {
        string bp = "";
        switch (prefix)
        {
            case TreeDir.BinaryPrefix.Gibibytes:
                bp = " GiB";
                break;
            case TreeDir.BinaryPrefix.Mebibytes:
                bp = " MiB";
                break;
            case TreeDir.BinaryPrefix.Kibibytes:
                bp = " KiB";
                break;
        }
        return GetSize(prefix).ToString("N") + bp;
    }

}
