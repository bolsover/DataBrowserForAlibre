using System;
using System.Collections;
using System.IO;
using AlibreX;

namespace Bolsover.DataBrowser;

public class AlibreFileSystem : IEquatable<AlibreFileSystem>
{
    private string _description;
 //   private AlibreConnector alibreConnector = new();

    public AlibreFileSystem(FileSystemInfo info)
    {
        Info = info;
    }

    public FileSystemInfo Info { get; }
    public bool IsDirectory => AsDirectory != null;
    public DirectoryInfo AsDirectory => Info as DirectoryInfo;
    public FileInfo AsFile => Info as FileInfo;

    public string Name => Info.Name;
    public string FullName => Info.FullName;

    public bool Exists { get; }
    public bool IsChecked { get; set; }

    public string AlibreDescription{ get; set; }

    public ADUnits AlibreAngleDisplayUnits { get; set; }
    public double AlibreDensity { get; set; }
    public ADUnits AlibreLengthDisplayUnits { get; set; }
    public ADUnits AlibreMassUnits { get; set; }
    public ADUnits AlibreModelUnits { get; set; }
    public string AlibrePartNo { get; set; }
    public string AlibreMaterial { get; set; }
    public string AlibreComment { get; set; }
    public string AlibreCostCenter { get; set; }
    public string AlibreCreatedBy { get; set; }
    public string AlibreCreatedDate { get; set; }
    public string AlibreCreatingApplication { get; set; }
    public string AlibreDocumentNumber { get; set; }
    public string AlibreEngApprovalDate { get; set; }
    public string AlibreEngApprovedBy { get; set; }
    public string AlibreEstimatedCost { get; set; }
    public string AlibreKeywords { get; set; }
    public string AlibreLastAuthor { get; set; }
    public string AlibreLastUpdateDate { get; set; }
    public string AlibreMfgApprovedDate { get; set; }
    public string AlibreMfgApprovedBy { get; set; }
    public string AlibreModified { get; set; }
    public string AlibreProduct { get; set; }
    public string AlibreReceivedFrom { get; set; }
    public string AlibreStockSize { get; set; }
    public string AlibreSupplier { get; set; }
    public string AlibreRevision { get; set; }
    public string AlibreTitle { get; set; }
    public string AlibreVendor { get; set; }
    public string AlibreWebLink { get; set; }


    public bool Equals(AlibreFileSystem other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Equals(other.Info.FullName, Info.FullName);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != typeof(AlibreFileSystem)) return false;
        return Equals((AlibreFileSystem) obj);
    }

    public override int GetHashCode()
    {
        return Info != null ? Info.FullName.GetHashCode() : 0;
    }

    public static bool operator ==(AlibreFileSystem left, AlibreFileSystem right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(AlibreFileSystem left, AlibreFileSystem right)
    {
        return !Equals(left, right);
    }

    public IEnumerable GetFileSystemInfos()
    {
        var children = new ArrayList();
        if (IsDirectory)
            foreach (var x in AsDirectory.GetFileSystemInfos())

                children.Add(new AlibreFileSystem(x));

        return children;
    }
}