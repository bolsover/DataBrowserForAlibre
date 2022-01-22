using System;
using AlibreX;

namespace Bolsover.DataBrowser;

public class AlibreConnector
{
    static IADRoot root;

    static AlibreConnector()
    {
        try
        {
            IAutomationHook hook = new AutomationHook();
            hook.Initialize(null, null, null, false, 0);
            Console.WriteLine("Connected to Alibre.");
            root = hook.Root as IADRoot;
        }
        catch
        {
            Console.WriteLine("Failed to connect to Alibre.");
        }
    }


    public IADDesignSession RetrieveSessionForFile(AlibreFileSystem alibreFileSystem)
    {
        IADDesignSession session = (IADDesignSession) root.OpenFile(alibreFileSystem.FullName);
        return session;
    }

    private IADDesignProperties RetrieveDesignPropertiesForSession(IADDesignSession session)
    {
        IADDesignProperties designProperties = session.DesignProperties;
        return designProperties;
    }

    public IADDesignProperties RetrieveDesignPropertiesForFile(AlibreFileSystem alibreFileSystem)
    {
        IADDesignSession session = RetrieveSessionForFile(alibreFileSystem);
        return RetrieveDesignPropertiesForSession(session);
      

    }

    /*
     * Removes all Alibre data from the AlibreFileSystem rowObject. Does not update Alibre
     */
    public void ResetAlibreData(AlibreFileSystem rowObject)
    {
        rowObject.AlibreDescription = "";
            rowObject.AlibrePartNo = "";
            rowObject.AlibreMaterial = "";
            //rowObject.AlibreAngleDisplayUnits = "";
            //rowObject.AlibreDensity = designProperties.Density;
            //rowObject.AlibreLengthDisplayUnits = designProperties.LengthDisplayUnits;
            //rowObject.AlibreMassUnits = designProperties.MassUnits;
            //rowObject.AlibreModelUnits = designProperties.ModelUnits;
            rowObject.AlibreComment = "";
           
            rowObject.AlibreCostCenter  = "";
            rowObject.AlibreCreatedBy  = "";
            rowObject.AlibreCreatedDate  = "";
            rowObject.AlibreCreatingApplication  = "";
            rowObject.AlibreDocumentNumber  = "";
            rowObject.AlibreEngApprovalDate  = "";
            rowObject.AlibreEngApprovedBy = "";
            rowObject.AlibreEstimatedCost  = "";
            rowObject.AlibreKeywords  = "";
            rowObject.AlibreLastAuthor = "";
            rowObject.AlibreLastUpdateDate = "";
            rowObject.AlibreMfgApprovedBy = "";
            rowObject.AlibreMfgApprovedDate = "";
            rowObject.AlibreModified = "";
            rowObject.AlibreProduct = "";
            rowObject.AlibreReceivedFrom  = "";
            rowObject.AlibreRevision  = "";
            rowObject.AlibreStockSize = "";
            rowObject.AlibreSupplier  = "";
            rowObject.AlibreTitle  = "";
            rowObject.AlibreVendor = "";
            rowObject.AlibreWebLink  = "";
    }

    /*
     * Retrieves data from the Alibre Session and populates the corresponding fields in the AlibreFileSystem rowObject
     */
    public void RetrieveAlibreData(AlibreFileSystem rowObject)
    {
        if (rowObject != null && !rowObject.IsDirectory && rowObject.AsFile != null &&
            rowObject.AsFile.Extension != null && rowObject.AsFile.Extension.StartsWith(".AD_P") |
            rowObject.AsFile.Extension.StartsWith(".AD_A"))
        {
            var session = this.RetrieveSessionForFile(rowObject);
            var designProperties = session.DesignProperties;
            rowObject.AlibreDescription = designProperties.Description;
            rowObject.AlibrePartNo = designProperties.Number;
            rowObject.AlibreMaterial = designProperties.Material;
            rowObject.AlibreAngleDisplayUnits = designProperties.AngleDisplayUnits;
            rowObject.AlibreDensity = designProperties.Density;
            rowObject.AlibreLengthDisplayUnits = designProperties.LengthDisplayUnits;
            rowObject.AlibreMassUnits = designProperties.MassUnits;
            rowObject.AlibreModelUnits = designProperties.ModelUnits;
            rowObject.AlibreComment = (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_COMMENT);
           
            rowObject.AlibreCostCenter = (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_COST_CENTER);
            rowObject.AlibreCreatedBy = (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_CREATED_BY);
            rowObject.AlibreCreatedDate = (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_CREATED_DATE);
            rowObject.AlibreCreatingApplication = (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_CREATING_APPLICATION);
            rowObject.AlibreDocumentNumber = (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_DOCUMENT_NUMBER);
            rowObject.AlibreEngApprovalDate = (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_ENG_APPROVAL_DATE);
            rowObject.AlibreEngApprovedBy = (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_ENG_APPROVED_BY);
            rowObject.AlibreEstimatedCost = (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_ESTIMATED_COST);
            rowObject.AlibreKeywords = (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_KEYWORDS);
            rowObject.AlibreLastAuthor = (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_LAST_AUTHOR);
            rowObject.AlibreLastUpdateDate = (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_LAST_UPDATE_DATE);
            rowObject.AlibreMfgApprovedBy = (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_MFG_APPROVED_BY);
            rowObject.AlibreMfgApprovedDate = (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_MFG_APPROVED_DATE);
            rowObject.AlibreModified = (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_MODIFIED);
            rowObject.AlibreProduct = (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_PRODUCT);
            rowObject.AlibreReceivedFrom = (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_RECEIVED_FROM);
            rowObject.AlibreRevision = (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_REVISION);
            rowObject.AlibreStockSize = (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_STOCK_SIZE);
            rowObject.AlibreSupplier = (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_SUPPLIER);
            rowObject.AlibreTitle = (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_TITLE);
            rowObject.AlibreVendor = (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_VENDOR);
            rowObject.AlibreWebLink = (string) designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_WEBLINK);
            session.Close(false);
            
        }
    }
}