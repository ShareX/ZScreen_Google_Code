using System.IO;
public class cFile
{

    private double mSize;
    private string mPath;

    public cFile(string filePath)
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

    public double GetSize(cDir.BinaryPrefix prefix)
    {
        switch (prefix)
        {
            case cDir.BinaryPrefix.Bytes:
                return mSize;
            case cDir.BinaryPrefix.Kibibits:
                return (mSize / 128);
            case cDir.BinaryPrefix.Kibibytes:
                return (mSize / 1024);
            case cDir.BinaryPrefix.Mebibytes:
                return (mSize / (1024 * 1024));
            case cDir.BinaryPrefix.Gibibytes:
                return (mSize / (1024 * 1024 * 1024));
        }
        return mSize;
    }

    public string GetSizeToString(cDir.BinaryPrefix prefix)
    {
        string bp = "";
        switch (prefix)
        {
            case cDir.BinaryPrefix.Gibibytes:
                bp = " GiB";
                break;
            case cDir.BinaryPrefix.Mebibytes:
                bp = " MiB";
                break;
            case cDir.BinaryPrefix.Kibibytes:
                bp = " KiB";
                break;
        }
        return GetSize(prefix).ToString("N") + bp;
    }

}
