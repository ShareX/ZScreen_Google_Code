using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
public class cDir
{
    private List<cDir> mSubDirCol = new List<cDir>();
    private double mSizeOfFiles = 0.0;
    private string mDirPath;
    private List<cFile> mFiles = new List<cFile>();

    public cDir(string dirPath)
    {
        mDirPath = dirPath;
    }

    public string DirectoryPath()
    {
        return mDirPath;
    }

    public string DirectoryName()
    {
        return Path.GetFileName(DirectoryPath());
    }

    public string SetFile(string filePath, [OptionalAttribute, DefaultParameterValueAttribute(BinaryPrefix.Kibibytes)] BinaryPrefix prefix)
    {

        cFile f = new cFile(filePath);
        mFiles.Add(f);

        mSizeOfFiles += f.GetSize(BinaryPrefix.Kibibytes);

        switch (prefix)
        {
            case BinaryPrefix.Mebibytes:
                return f.GetSize(BinaryPrefix.Mebibytes).ToString("N") + " MiB";
            case BinaryPrefix.Gibibytes:
                return f.GetSize(BinaryPrefix.Gibibytes).ToString("N") + " GiB";
            default:
                return f.GetSize(BinaryPrefix.Kibibytes).ToString("N") + " KiB";

        }
    }


    public List<cFile> GetFilesColl()
    {
        return mFiles;
    }

    public void AddDir(cDir mySubDir)
    {
        mSubDirCol.Add(mySubDir);
    }

    public List<cDir> GetSubDirColl()
    {
        return mSubDirCol;
    }

    public double DirectorySize()
    {
        double dirSize = this.mSizeOfFiles;

        foreach (cDir dir in this.GetSubDirColl())
        {
            dirSize += dir.DirectorySize();
        }

        return dirSize;
    }

    public string DirectorySizeToString(BinaryPrefix prefix)
    {

        switch (prefix)
        {
            case BinaryPrefix.Gibibytes:
                return (this.DirectorySize() / (1024 * 1024)).ToString("N") + " GiB";
            case BinaryPrefix.Mebibytes:
                return (this.DirectorySize() / 1024).ToString("N") + " MiB";
            case BinaryPrefix.Kibibytes:
                return (this.DirectorySize()).ToString("N") + " KiB";
        }


        return null;
    }

    public enum BinaryPrefix
    {
        Bytes,
        Kibibits,
        Kibibytes,
        Mebibytes,
        Gibibytes
    }

    private double GetSizeOfFiles()
    {
        return mSizeOfFiles;
    }

}
