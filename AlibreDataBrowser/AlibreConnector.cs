using System;
using AlibreX;

namespace Bolsover.DataBrowser;

public class AlibreConnector
{
    private static readonly IAutomationHook hook;

    static AlibreConnector()
    {
        try
        {
            hook = new AutomationHook();
            hook.Initialize(null, null, null, false, 0);
            Console.WriteLine("Connected to Alibre.");
        }
        catch
        {
            Console.WriteLine("Failed to connect to Alibre.");
        }
    }

    public static IADRoot RetrieveIADRoot()
    {
        return hook.Root as IADRoot;
    }


    public static IADMaterialLibraries RetrieveMaterialLibrariesForRoot()
    {
        return RetrieveIADRoot().MaterialLibraries;
    }


    public static IADDesignSession RetrieveSessionForFile(AlibreFileSystem alibreFileSystem)
    {
        var session = (IADDesignSession) RetrieveIADRoot().OpenFile(alibreFileSystem.FullName);
        return session;
    }


    /*
     * Removes all Alibre data from the AlibreFileSystem rowObject. Does not update Alibre
     */
    public static void ResetAlibreData(AlibreFileSystem rowObject)
    {
        rowObject.AlibreDescription = null;
        rowObject.AlibrePartNo = null;
        rowObject.AlibreMaterial = null;
        rowObject.AlibreExtMaterial = null;
        // rowObject.AlibreAngleDisplayUnits = "";
        // rowObject.AlibreDensity = designProperties.Density;
        // rowObject.AlibreLengthDisplayUnits = designProperties.LengthDisplayUnits;
        // rowObject.AlibreMassUnits = designProperties.MassUnits;
        // rowObject.AlibreModelUnits = designProperties.ModelUnits;
        rowObject.AlibreComment = "";

        rowObject.AlibreCostCenter = null;
        rowObject.AlibreCreatedBy = null;
        rowObject.AlibreCreatedDate = DateTime.MinValue;
        rowObject.AlibreCreatingApplication = null;
        rowObject.AlibreDocumentNumber = null;
        rowObject.AlibreEngApprovalDate = null;
        rowObject.AlibreEngApprovedBy = null;
        rowObject.AlibreEstimatedCost = null;
        rowObject.AlibreKeywords = null;
        rowObject.AlibreLastAuthor = null;
        rowObject.AlibreLastUpdateDate = null;
        rowObject.AlibreMfgApprovedBy = null;
        rowObject.AlibreMfgApprovedDate = null;
        rowObject.AlibreModified = null;
        rowObject.AlibreProduct = null;
        rowObject.AlibreReceivedFrom = null;
        rowObject.AlibreRevision = null;
        rowObject.AlibreStockSize = null;
        rowObject.AlibreSupplier = null;
        rowObject.AlibreTitle = null;
        rowObject.AlibreVendor = null;
        rowObject.AlibreWebLink = null;
    }

    /*
     * Retrieves data from the Alibre Session and populates the corresponding fields in the AlibreFileSystem rowObject
     */
    public static void RetrieveAlibreData(AlibreFileSystem rowObject)
    {
        if (rowObject != null && !rowObject.IsDirectory
                              && rowObject.AsFile is {IsReadOnly: false}
                              && rowObject.AsFile.Extension != null
                              && rowObject.AsFile.Extension.StartsWith(".AD_P") |
                              rowObject.AsFile.Extension.StartsWith(".AD_A") |
                              rowObject.AsFile.Extension.StartsWith(".AD_S"))
        {
            var session = RetrieveSessionForFile(rowObject);
            var designProperties = session.DesignProperties;
            rowObject.AlibreDescription = designProperties.Description;
            rowObject.AlibrePartNo = designProperties.Number;
            rowObject.AlibreMaterial = designProperties.Material;
            rowObject.AlibreAngleDisplayUnits = designProperties.AngleDisplayUnits;
            rowObject.AlibreDensity = designProperties.Density;
            rowObject.AlibreLengthDisplayUnits = designProperties.LengthDisplayUnits;
            rowObject.AlibreMassUnits = designProperties.MassUnits;
            rowObject.AlibreModelUnits = designProperties.ModelUnits;
            rowObject.AlibreExtMaterial =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_MATERIAL);
            rowObject.AlibreComment =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_COMMENT);

            rowObject.AlibreCostCenter =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_COST_CENTER);
            rowObject.AlibreCreatedBy =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_CREATED_BY);

            var s = designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_CREATED_DATE);
            if (s != null) rowObject.AlibreCreatedDate = DateTime.Parse((string) s);

            rowObject.AlibreCreatingApplication =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_CREATING_APPLICATION);
            rowObject.AlibreDocumentNumber =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_DOCUMENT_NUMBER);

            s = designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_ENG_APPROVAL_DATE);
            if (s != null) rowObject.AlibreEngApprovalDate = DateTime.Parse((string) s);

            rowObject.AlibreEngApprovedBy =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_ENG_APPROVED_BY);
            rowObject.AlibreEstimatedCost =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_ESTIMATED_COST);
            rowObject.AlibreKeywords =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_KEYWORDS);
            rowObject.AlibreLastAuthor =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_LAST_AUTHOR);
            s = designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_LAST_UPDATE_DATE);
            if (s != null) rowObject.AlibreLastUpdateDate = DateTime.Parse((string) s);

            rowObject.AlibreMfgApprovedBy =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_MFG_APPROVED_BY);
            s = designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_MFG_APPROVED_DATE);
            if (s != null) rowObject.AlibreMfgApprovedDate = DateTime.Parse((string) s);

            s = designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_MODIFIED);
            if (s != null) rowObject.AlibreModified = DateTime.Parse((string) s);

            rowObject.AlibreProduct =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_PRODUCT);
            rowObject.AlibreReceivedFrom =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_RECEIVED_FROM);
            rowObject.AlibreRevision =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_REVISION);
            rowObject.AlibreStockSize =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_STOCK_SIZE);
            rowObject.AlibreSupplier =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_SUPPLIER);
            rowObject.AlibreTitle = (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_TITLE);
            rowObject.AlibreVendor =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_VENDOR);
            rowObject.AlibreWebLink =
                (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_WEBLINK);
            session.Close();
        }
    }
}