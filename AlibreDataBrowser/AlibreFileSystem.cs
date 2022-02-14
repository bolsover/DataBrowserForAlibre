using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using AlibreTests2.Annotations;
using AlibreX;
using BrightIdeasSoftware;

namespace Bolsover.DataBrowser;

public class AlibreFileSystem : IEquatable<AlibreFileSystem>, INotifyPropertyChanged
{
    private bool _isChecked;
   

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

    public bool IsChecked
    {
        get => _isChecked;
        set => SetPropertyField("IsChecked", ref _isChecked, value);
    }


    public string AlibreDescription { get; set; }

    public ADUnits AlibreAngleDisplayUnits { get; set; }
    public double AlibreDensity { get; set; }
    public ADUnits AlibreLengthDisplayUnits { get; set; }
    public ADUnits AlibreMassUnits { get; set; }
    public ADUnits AlibreModelUnits { get; set; }
    public string AlibrePartNo { get; set; }
    public string AlibreMaterial { get; set; }
    public string AlibreMaterialGuid { get; set; }
    public string AlibreExtMaterial { get; set; }
    public string AlibreComment { get; set; }
    public string AlibreCostCenter { get; set; }
    public string AlibreCreatedBy { get; set; }
    public DateTime? AlibreCreatedDate { get; set; }
    public string AlibreCreatingApplication { get; set; }
    public string AlibreDocumentNumber { get; set; }
    public DateTime? AlibreEngApprovalDate { get; set; }
    public string AlibreEngApprovedBy { get; set; }
    public string AlibreEstimatedCost { get; set; }
    public string AlibreKeywords { get; set; }
    public string AlibreLastAuthor { get; set; }
    public DateTime? AlibreLastUpdateDate { get; set; }
    public DateTime? AlibreMfgApprovedDate { get; set; }
    public string AlibreMfgApprovedBy { get; set; }
    public DateTime? AlibreModified { get; set; }
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

    public void Reset()
    {
        AlibreDescription = null;
        AlibrePartNo = null;
        AlibreMaterial = null;
        AlibreExtMaterial = null;
        // rowObject.AlibreAngleDisplayUnits = "";
        // rowObject.AlibreDensity = designProperties.Density;
        // rowObject.AlibreLengthDisplayUnits = designProperties.LengthDisplayUnits;
        // rowObject.AlibreMassUnits = designProperties.MassUnits;
        // rowObject.AlibreModelUnits = designProperties.ModelUnits;
        AlibreComment = null;

        AlibreCostCenter = null;
        AlibreCreatedBy = null;
        AlibreCreatedDate = null;
        AlibreCreatingApplication = null;
        AlibreDocumentNumber = null;
        AlibreEngApprovalDate = null;
        AlibreEngApprovedBy = null;
        AlibreEstimatedCost = null;
        AlibreKeywords = null;
        AlibreLastAuthor = null;
        AlibreLastUpdateDate = null;
        AlibreMfgApprovedBy = null;
        AlibreMfgApprovedDate = null;
        AlibreModified = null;
        AlibreProduct = null;
        AlibreReceivedFrom = null;
        AlibreRevision = null;
        AlibreStockSize = null;
        AlibreSupplier = null;
        AlibreTitle = null;
        AlibreVendor = null;
        AlibreWebLink = null;
    }


    /*
     * Retrieves data from the Alibre Session and populates the corresponding fields in the AlibreFileSystem rowObject
     */
    // [MethodImpl(MethodImplOptions.Synchronized)]
    public AlibreFileSystem RetrieveAlibreData()
    {
        if (!IsDirectory
            && AsFile is {IsReadOnly: false}
            // && AsFile.Extension != null
            && AsFile.Extension.StartsWith(".AD_P") |
            AsFile.Extension.StartsWith(".AD_A") |
            AsFile.Extension.StartsWith(".AD_S"))
        {
            var session = AlibreConnector.RetrieveSessionForFile(this);
            var designProperties = session.DesignProperties;

            AlibreDescription = designProperties.Description;
            AlibrePartNo = designProperties.Number;
            AlibreMaterial = designProperties.Material;
            // rowObject.AlibreAngleDisplayUnits = designProperties.AngleDisplayUnits;
            // rowObject.AlibreDensity = designProperties.Density;
            // rowObject.AlibreLengthDisplayUnits = designProperties.LengthDisplayUnits;
            // rowObject.AlibreMassUnits = designProperties.MassUnits;
            // rowObject.AlibreModelUnits = designProperties.ModelUnits;
            // rowObject.AlibreExtMaterial =
            //     (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_MATERIAL);
            AlibreComment =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_COMMENT);

            AlibreCostCenter =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_COST_CENTER);
            AlibreCreatedBy =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_CREATED_BY);

            var s = designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_CREATED_DATE);
            if (s != null) AlibreCreatedDate = DateTime.Parse((string) s);

            AlibreCreatingApplication =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_CREATING_APPLICATION);
            AlibreDocumentNumber =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_DOCUMENT_NUMBER);

            s = designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_ENG_APPROVAL_DATE);
            if (s != null) AlibreEngApprovalDate = DateTime.Parse((string) s);

            AlibreEngApprovedBy =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_ENG_APPROVED_BY);
            AlibreEstimatedCost =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_ESTIMATED_COST);
            AlibreKeywords =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_KEYWORDS);
            AlibreLastAuthor =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_LAST_AUTHOR);
            s = designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_LAST_UPDATE_DATE);
            if (s != null) AlibreLastUpdateDate = DateTime.Parse((string) s);

            AlibreMfgApprovedBy =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_MFG_APPROVED_BY);
            s = designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_MFG_APPROVED_DATE);
            if (s != null) AlibreMfgApprovedDate = DateTime.Parse((string) s);

            s = designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_MODIFIED);
            if (s != null) AlibreModified = DateTime.Parse((string) s);

            AlibreProduct =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_PRODUCT);
            AlibreReceivedFrom =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_RECEIVED_FROM);
            AlibreRevision =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_REVISION);
            AlibreStockSize =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_STOCK_SIZE);
            AlibreSupplier =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_SUPPLIER);
            AlibreTitle =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_TITLE);
            AlibreVendor =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_VENDOR);
            AlibreWebLink =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_WEBLINK);

            session.Close();
        }

        return this;
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
            {
                var alibreFileSystem = new AlibreFileSystem(x);
                // alibreFileSystem.PropertyChanged += DataBrowserForm.HandleAlibreFileSystemPropertyChangedEvent;
                children.Add(alibreFileSystem);
            }

        return children;
    }

    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        var handler = PropertyChanged;
        if (handler != null)
            handler(this, e);
    }


    protected void SetPropertyField<T>(string propertyName, ref T field, T newValue)
    {
        if (!EqualityComparer<T>.Default.Equals(field, newValue))
        {
            field = newValue;
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }
}