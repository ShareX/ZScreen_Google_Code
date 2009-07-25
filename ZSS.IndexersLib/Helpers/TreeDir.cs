using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
public class TreeDir
{
    private List<TreeDir> mSubDirCol = new List<TreeDir>();
    private double mSizeOfFiles = 0.0;
    private string mDirPath;
    private List<TreeFile> mFiles = new List<TreeFile>();

    public TreeDir(string dirPath)
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

        TreeFile f = new TreeFile(filePath);
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


    public List<TreeFile> GetFilesColl()
    {
        return mFiles;
    }

    public void AddDir(TreeDir mySubDir)
    {
        mSubDirCol.Add(mySubDir);
    }

    public List<TreeDir> GetSubDirColl()
    {
        return mSubDirCol;
    }

    public double DirectorySize()
    {
        double dirSize = this.mSizeOfFiles;

        foreach (TreeDir dir in this.GetSubDirColl())
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
