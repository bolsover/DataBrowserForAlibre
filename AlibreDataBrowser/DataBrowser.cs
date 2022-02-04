using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AlibreX;
using BrightIdeasSoftware;

namespace Bolsover.DataBrowser;

public partial class DataBrowserForm : Form
{
    private AlibreFileSystem editingRow;

    public DataBrowserForm()
    {
        InitializeComponent();
        setupColumns();
        setupTree();
        RegisterCustomEditors();
        FormClosed += (sender, args) => Environment.Exit(0);
    }


    private void setupColumns()
    {
        ConfigureAspectGetters();
        ConfigureAspectPutters();
    }

    private void RegisterCustomEditors()
    {
        // Register DateTime picker
        ObjectListView.EditorRegistry.Register(typeof(DateTime), delegate
        {
            var c = new DateTimePicker();
            c.Format = DateTimePickerFormat.Short;
            return c;
        });

        // Register MaterialPicker for use exclusively with olvColumnAlibreMaterial
        ObjectListView.EditorRegistry.Register(typeof(string), (model, column, value) =>
        {
            if (column == olvColumnAlibreMaterial)
            {
                var mc = new MaterialPicker();
                mc.ItemHasBeenSelected += McOnItemHasBeenSelected;
                return mc;
            }

            return null;
        });
    }


    /*
     * Retrieves the MaterialNode selected in the MaterialPicker.
     * Obtains the IADDesignSession and IADDesignProperties for the row being edited.
     * Updates the IADDesignProperties.Material with the Guid from the Material.
     * Saves the IADDesignSession.
     * Resets the AlibreMaterial property of the row being edited with the Name of the Material.
     */
    private void McOnItemHasBeenSelected(object sender, MaterialPicker.SelectedItemEventArgs e)
    {
        try
        {
            var materialNode = e.SelectedChoice;
            var designSession = AlibreConnector.RetrieveSessionForFile(editingRow);
            var designProperties = designSession.DesignProperties;

            //designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_MATERIAL, materialNode.Guid);
            designProperties.Material = materialNode.Guid;
            designSession.Close(true);
            editingRow.AlibreMaterial = materialNode.NodeName;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    /*
     * Configures AspectPutter methods for individual columns with the exception of Materials
     */
    private void ConfigureAspectPutters()
    {
        ConfigreAlibreDescriptionAspectPutter();
        ConfigreAlibrePartNoAspectPutter();
        ConfigureAlibreModifiedAspectPutter();
        // ConfigureAlibreMaterialAspectPutter(); // materials are handled by McOnItemHasBeenSelected method
        ConfigureColumnAspectPutter(olvColumnAlibreComment, ADExtendedDesignProperty.AD_COMMENT, typeof(string));
        ConfigureColumnAspectPutter(olvColumnAlibreCreatedDate, ADExtendedDesignProperty.AD_CREATED_DATE,
            typeof(DateTime));
        // ConfigureColumnAspectPutter(olvColumnAlibreMaterial, ADExtendedDesignProperty.AD_MATERIAL, typeof(string));
        ConfigureColumnAspectPutter(olvColumnAlibreCostCenter, ADExtendedDesignProperty.AD_COST_CENTER, typeof(string));
        ConfigureColumnAspectPutter(olvColumnAlibreCreatedBy, ADExtendedDesignProperty.AD_CREATED_BY, typeof(string));
        ConfigureColumnAspectPutter(olvColumnAlibreCreatingApplication,
            ADExtendedDesignProperty.AD_CREATING_APPLICATION, typeof(string));
        ConfigureColumnAspectPutter(olvColumnAlibreDocumentNumber, ADExtendedDesignProperty.AD_DOCUMENT_NUMBER,
            typeof(string));
        ConfigureColumnAspectPutter(olvColumnAlibreEngApprovalDate, ADExtendedDesignProperty.AD_ENG_APPROVAL_DATE,
            typeof(DateTime));
        ConfigureColumnAspectPutter(olvColumnAlibreEngApprovedBy, ADExtendedDesignProperty.AD_ENG_APPROVED_BY,
            typeof(string));
        ConfigureColumnAspectPutter(olvColumnAlibreEstimatedCost, ADExtendedDesignProperty.AD_ESTIMATED_COST,
            typeof(string));
        ConfigureColumnAspectPutter(olvColumnAlibreKeywords, ADExtendedDesignProperty.AD_KEYWORDS, typeof(string));
        ConfigureColumnAspectPutter(olvColumnAlibreLastAuthor, ADExtendedDesignProperty.AD_LAST_AUTHOR, typeof(string));
        ConfigureColumnAspectPutter(olvColumnAlibreLastUpdateDate, ADExtendedDesignProperty.AD_LAST_UPDATE_DATE,
            typeof(DateTime));
        ConfigureColumnAspectPutter(olvColumnAlibreMfgApprovedBy, ADExtendedDesignProperty.AD_MFG_APPROVED_BY,
            typeof(string));
        ConfigureColumnAspectPutter(olvColumnAlibreMfgApprovedDate, ADExtendedDesignProperty.AD_MFG_APPROVED_DATE,
            typeof(DateTime));
        ConfigureColumnAspectPutter(olvColumnAlibreProduct, ADExtendedDesignProperty.AD_PRODUCT, typeof(string));
        ConfigureColumnAspectPutter(olvColumnAlibreReceivedFrom, ADExtendedDesignProperty.AD_RECEIVED_FROM,
            typeof(string));
        ConfigureColumnAspectPutter(olvColumnAlibreRevision, ADExtendedDesignProperty.AD_REVISION, typeof(string));
        ConfigureColumnAspectPutter(olvColumnAlibreStockSize, ADExtendedDesignProperty.AD_STOCK_SIZE, typeof(string));
        ConfigureColumnAspectPutter(olvColumnAlibreSupplier, ADExtendedDesignProperty.AD_SUPPLIER, typeof(string));
        ConfigureColumnAspectPutter(olvColumnAlibreTitle, ADExtendedDesignProperty.AD_TITLE, typeof(string));
        ConfigureColumnAspectPutter(olvColumnAlibreVendor, ADExtendedDesignProperty.AD_VENDOR, typeof(string));
        ConfigureColumnAspectPutter(olvColumnAlibreWebLink, ADExtendedDesignProperty.AD_WEBLINK, typeof(string));
    }

    /*
     * Helper method to cast object Types
     */
    public static dynamic Cast(dynamic obj, Type castTo)
    {
        return Convert.ChangeType(obj, castTo);
    }

    /*
     * Configures AspectPutters based on the column, extendedDesignProperty and Type
     * OLVColumn column: The column to be configured
     * ADExtendedDesignProperty extendedDesignProperty: the property against which a value is put.
     * Type type: The Type of property being put.
     */
    private void ConfigureColumnAspectPutter(OLVColumn column, ADExtendedDesignProperty extendedDesignProperty,
        Type type)
    {
        column.AspectPutter = (rowObject, value) =>
        {
            var session = AlibreConnector.RetrieveSessionForFile((AlibreFileSystem) rowObject);
            var designProperties = session.DesignProperties;
            switch (extendedDesignProperty)
            {
                case ADExtendedDesignProperty.AD_WEBLINK:
                    ((AlibreFileSystem) rowObject).AlibreWebLink = Cast(value, type);
                    break;
                case ADExtendedDesignProperty.AD_VENDOR:
                    ((AlibreFileSystem) rowObject).AlibreVendor = Cast(value, type);
                    break;
                case ADExtendedDesignProperty.AD_MFG_APPROVED_DATE:
                    ((AlibreFileSystem) rowObject).AlibreMfgApprovedDate = Cast(value, type);
                    break;
                case ADExtendedDesignProperty.AD_COMMENT:
                    ((AlibreFileSystem) rowObject).AlibreComment = Cast(value, type);
                    break;
                case ADExtendedDesignProperty.AD_CREATED_DATE:
                    ((AlibreFileSystem) rowObject).AlibreCreatedDate = Cast(value, type);
                    break;

                case ADExtendedDesignProperty.AD_MATERIAL:
                    ((AlibreFileSystem) rowObject).AlibreExtMaterial = Cast(value, type);
                    break;
                case ADExtendedDesignProperty.AD_COST_CENTER:
                    ((AlibreFileSystem) rowObject).AlibreCostCenter = Cast(value, type);
                    break;
                case ADExtendedDesignProperty.AD_CREATED_BY:
                    ((AlibreFileSystem) rowObject).AlibreCreatedBy = Cast(value, type);
                    break;
                case ADExtendedDesignProperty.AD_CREATING_APPLICATION:
                    ((AlibreFileSystem) rowObject).AlibreCreatingApplication = Cast(value, type);
                    break;
                case ADExtendedDesignProperty.AD_DOCUMENT_NUMBER:
                    ((AlibreFileSystem) rowObject).AlibreDocumentNumber = Cast(value, type);
                    break;
                case ADExtendedDesignProperty.AD_ENG_APPROVAL_DATE:
                    ((AlibreFileSystem) rowObject).AlibreEngApprovalDate = Cast(value, type);
                    break;
                case ADExtendedDesignProperty.AD_ENG_APPROVED_BY:
                    ((AlibreFileSystem) rowObject).AlibreEngApprovedBy = Cast(value, type);
                    break;
                case ADExtendedDesignProperty.AD_ESTIMATED_COST:
                    ((AlibreFileSystem) rowObject).AlibreEstimatedCost = Cast(value, type);
                    break;
                case ADExtendedDesignProperty.AD_KEYWORDS:
                    ((AlibreFileSystem) rowObject).AlibreKeywords = Cast(value, type);
                    break;
                case ADExtendedDesignProperty.AD_LAST_AUTHOR:
                    ((AlibreFileSystem) rowObject).AlibreLastAuthor = Cast(value, type);
                    break;
                case ADExtendedDesignProperty.AD_MFG_APPROVED_BY:
                    ((AlibreFileSystem) rowObject).AlibreMfgApprovedBy = Cast(value, type);
                    break;
                case ADExtendedDesignProperty.AD_PRODUCT:
                    ((AlibreFileSystem) rowObject).AlibreProduct = Cast(value, type);
                    break;
                case ADExtendedDesignProperty.AD_RECEIVED_FROM:
                    ((AlibreFileSystem) rowObject).AlibreReceivedFrom = Cast(value, type);
                    break;
                case ADExtendedDesignProperty.AD_REVISION:
                    ((AlibreFileSystem) rowObject).AlibreRevision = Cast(value, type);
                    break;
                case ADExtendedDesignProperty.AD_STOCK_SIZE:
                    ((AlibreFileSystem) rowObject).AlibreStockSize = Cast(value, type);
                    break;
                case ADExtendedDesignProperty.AD_SUPPLIER:
                    ((AlibreFileSystem) rowObject).AlibreSupplier = Cast(value, type);
                    break;
                case ADExtendedDesignProperty.AD_TITLE:
                    ((AlibreFileSystem) rowObject).AlibreTitle = Cast(value, type);
                    break;
                case ADExtendedDesignProperty.AD_LAST_UPDATE_DATE:
                    ((AlibreFileSystem) rowObject).AlibreLastUpdateDate = Cast(value, type);
                    break;
            }

            if (type == typeof(DateTime))
            {
                value = ((DateTime) value).Date.ToShortDateString();
                designProperties.ExtendedDesignProperty(extendedDesignProperty, value);
            }
            else
            {
                designProperties.ExtendedDesignProperty(extendedDesignProperty, Cast(value, type));
            }

            session.Close(true);
        };
    }


    private void ConfigureAlibreModifiedAspectPutter()
    {
        olvColumnAlibreModified.AspectPutter = (rowObject, value) =>
        {
            ((AlibreFileSystem) rowObject).AlibreModified = (DateTime) value;
            var session = AlibreConnector.RetrieveSessionForFile((AlibreFileSystem) rowObject);

            var designProperties = session.DesignProperties;
            designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_MODIFIED,
                ((DateTime) value).Date.ToShortDateString());
            try
            {
                session.Close(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        };
    }


    private void ConfigreAlibrePartNoAspectPutter()
    {
        olvColumnAlibrePartNo.AspectPutter = (rowObject, value) =>
        {
            ((AlibreFileSystem) rowObject).AlibrePartNo = (string) value;
            var session = AlibreConnector.RetrieveSessionForFile((AlibreFileSystem) rowObject);
            var designProperties = session.DesignProperties;
            designProperties.Number = (string) value;

            try
            {
                session.Close(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        };
    }


    private void ConfigreAlibreDescriptionAspectPutter()
    {
        olvColumnAlibreDescription.AspectPutter = (rowObject, value) =>
        {
            ((AlibreFileSystem) rowObject).AlibreDescription = (string) value;
            var session = AlibreConnector.RetrieveSessionForFile((AlibreFileSystem) rowObject);
            var designProperties = session.DesignProperties;
            designProperties.Description = (string) value;

            try
            {
                session.Close(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        };
    }

    /*
     * Configures all the AspectGetter methods for individual columns
     */
    private void ConfigureAspectGetters()
    {
        var helper = new SysImageListHelper(treeListView);
        olvColumnName.ImageGetter = rowObject => helper.GetImageIndex(((AlibreFileSystem) rowObject).FullName);
        olvColumnType.AspectGetter = rowObject => ShellUtilities.GetFileType(((AlibreFileSystem) rowObject).FullName);
        olvColumnModified.AspectGetter = rowObject => ((AlibreFileSystem) rowObject).Info.LastWriteTime;
        olvColumnAlibreDescription.AspectGetter = rowObject => ((AlibreFileSystem) rowObject).AlibreDescription;

        olvColumnAlibrePartNo.AspectGetter = rowObject => ((AlibreFileSystem) rowObject).AlibrePartNo;
        olvColumnAlibreMaterial.AspectGetter = rowObject => ((AlibreFileSystem) rowObject).AlibreMaterial;
        //  olvColumnAlibreExtMaterial.AspectGetter = rowObject => ((AlibreFileSystem) rowObject).AlibreExtMaterial;
        olvColumnAlibreComment.AspectGetter = rowObject => ((AlibreFileSystem) rowObject).AlibreComment;
        olvColumnAlibreCostCenter.AspectGetter = rowObject => ((AlibreFileSystem) rowObject).AlibreCostCenter;
        olvColumnAlibreCreatedBy.AspectGetter = rowObject => ((AlibreFileSystem) rowObject).AlibreCreatedBy;
        olvColumnAlibreCreatedDate.AspectGetter = rowObject => ((AlibreFileSystem) rowObject).AlibreCreatedDate;
        olvColumnAlibreCreatingApplication.AspectGetter =
            rowObject => ((AlibreFileSystem) rowObject).AlibreCreatingApplication;
        olvColumnAlibreDocumentNumber.AspectGetter = rowObject => ((AlibreFileSystem) rowObject).AlibreDocumentNumber;
        olvColumnAlibreEngApprovalDate.AspectGetter = rowObject => ((AlibreFileSystem) rowObject).AlibreEngApprovalDate;
        olvColumnAlibreEngApprovedBy.AspectGetter = rowObject => ((AlibreFileSystem) rowObject).AlibreEngApprovedBy;
        olvColumnAlibreEstimatedCost.AspectGetter = rowObject => ((AlibreFileSystem) rowObject).AlibreEstimatedCost;
        olvColumnAlibreKeywords.AspectGetter = rowObject => ((AlibreFileSystem) rowObject).AlibreKeywords;
        olvColumnAlibreLastAuthor.AspectGetter = rowObject => ((AlibreFileSystem) rowObject).AlibreLastAuthor;
        olvColumnAlibreLastUpdateDate.AspectGetter = rowObject => ((AlibreFileSystem) rowObject).AlibreLastUpdateDate;
        olvColumnAlibreMfgApprovedBy.AspectGetter = rowObject => ((AlibreFileSystem) rowObject).AlibreMfgApprovedBy;
        olvColumnAlibreMfgApprovedDate.AspectGetter = rowObject => ((AlibreFileSystem) rowObject).AlibreMfgApprovedDate;
        olvColumnAlibreModified.AspectGetter = rowObject => ((AlibreFileSystem) rowObject).AlibreModified;
        olvColumnAlibreProduct.AspectGetter = rowObject => ((AlibreFileSystem) rowObject).AlibreProduct;
        olvColumnAlibreReceivedFrom.AspectGetter = rowObject => ((AlibreFileSystem) rowObject).AlibreReceivedFrom;
        olvColumnAlibreRevision.AspectGetter = rowObject => ((AlibreFileSystem) rowObject).AlibreRevision;
        olvColumnAlibreStockSize.AspectGetter = rowObject => ((AlibreFileSystem) rowObject).AlibreStockSize;
        olvColumnAlibreSupplier.AspectGetter = rowObject => ((AlibreFileSystem) rowObject).AlibreSupplier;
        olvColumnAlibreTitle.AspectGetter = rowObject => ((AlibreFileSystem) rowObject).AlibreTitle;
        olvColumnAlibreVendor.AspectGetter = rowObject => ((AlibreFileSystem) rowObject).AlibreVendor;
        olvColumnAlibreWebLink.AspectGetter = rowObject => ((AlibreFileSystem) rowObject).AlibreWebLink;
    }

    private void setupTree()
    {
        // TreeListView require two delegates:
        // 1. CanExpandGetter - Can a particular model be expanded?
        // 2. ChildrenGetter - Once the CanExpandGetter returns true, ChildrenGetter should return the list of children
        // CanExpandGetter is called very often! It must be very fast.
        // treeListView.CanExpandGetter = rowObject => (((AlibreFileSystem) rowObject).IsDirectory | ((AlibreFileSystem) rowObject).FullName.EndsWith("AD_ASM")) ;
        treeListView.CanExpandGetter = rowObject => ((AlibreFileSystem) rowObject).IsDirectory;
        treeListView.ChildrenGetter = rowObject =>
        {
            try
            {
                return ((AlibreFileSystem) rowObject).GetFileSystemInfos();
            }
            catch (UnauthorizedAccessException ex)
            {
                BeginInvoke((MethodInvoker) delegate { treeListView.Collapse(rowObject); });
                return new ArrayList();
            }
        };

        var roots = new ArrayList();
        foreach (var di in DriveInfo.GetDrives())
            if (di.IsReady)
                roots.Add(new AlibreFileSystem(new DirectoryInfo(di.Name)));
        treeListView.Roots = roots;
        treeListView.HierarchicalCheckboxes = true;
        treeListView.BooleanCheckStateGetter = rowObject => ((AlibreFileSystem) rowObject).IsChecked;
        treeListView.BooleanCheckStatePutter = (rowObject, value) =>
        {
            ((AlibreFileSystem) rowObject).IsChecked = value;
            if (value)
                AlibreConnector.RetrieveAlibreData((AlibreFileSystem) rowObject);
            else
                AlibreConnector.ResetAlibreData((AlibreFileSystem) rowObject);
            return value;
        };

        // add handler for CellEditStarting
        treeListView.CellEditStarting += HandleCellEditStarting;
    }


    /*
     * Handle CellEditStarting
     * Set editingRow field to correspond to the row being edited.
     * Cancel edit and return if the row is a directory.
     * Cancel edit and return if the row is not checked.
     * Cancel edit and return if row is not a Part, Assembly or Sheet Metal design
     * Fix up the cell edit control bounds - special attention to MaterialPicker
     * Cancel edit and return if row is locked - probably open in Alibre
     */
    private void HandleCellEditStarting(object sender, CellEditEventArgs e)
    {
        var rowObject = (AlibreFileSystem) e.RowObject;
        editingRow = rowObject;

        // directory items are not editable
        if (rowObject.IsDirectory)
        {
            e.Cancel = true;
            return;
        }

        // only checked items should be editable
        if (!rowObject.IsChecked)
        {
            e.Cancel = true;
            return;
        }

        // prevent edits to anything other than sheet metal, part and assembly types
        var extension = rowObject.Info.Extension.ToUpper();
        if (!(rowObject.Info.Extension.StartsWith(".AD_P") | rowObject.Info.Extension.StartsWith(".AD_A") |
              rowObject.Info.Extension.StartsWith(".AD_S")))
        {
            e.Cancel = true;
            return;
        }

        // olvColumnAlibreMaterial uses MaterialPicker other string based columns use default editor
        if (e.Column != olvColumnAlibreMaterial)
        {
            // fix up size of cell editor
            e.Control.Bounds = e.CellBounds;
        }
        else
        {
            if (!(rowObject.Info.Extension.StartsWith(".AD_P") | rowObject.Info.Extension.StartsWith(".AD_S")))
            {
                e.Cancel = true;
                return;
            }

            e.Control.Bounds = new Rectangle(e.CellBounds.X, e.CellBounds.Y, 250, 300);
        }

        // prevent editing locked files
        if (IsFileLocked(rowObject.AsFile))
        {
            e.Cancel = true;
            var message = "File Locked - Probably Open in Alibre.";
            var title = "File Locked";
            MessageBox.Show(message, title);
        }

        Console.WriteLine(sender);
    }

    /*
     * Utility to check if file is locked elsewhere
     */
    protected virtual bool IsFileLocked(FileInfo file)
    {
        try
        {
            using (var stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
            {
                stream.Close();
            }
        }
        catch (IOException)
        {
            //the file is unavailable because it is:
            //still being written to
            //or being processed by another thread
            //or does not exist (has already been processed)
            return true;
        }

        //file is not locked
        return false;
    }


    private void checkBoxFilter_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBoxFilter.Checked)
            treeListView.ModelFilter = new ModelFilter(rowObject =>
            {
                if (((AlibreFileSystem) rowObject).IsDirectory) return true;
                return ((AlibreFileSystem) rowObject).Info.Extension.StartsWith(".AD_");
            });
        else
            treeListView.ModelFilter = new ModelFilter(rowObject => { return true; });
    }

    private void checkBoxCopy_CheckedChanged(object sender, EventArgs e)
    {
        MessageBox.Show("Not Implemented Yet", "Not Implemented");
        //throw new NotImplementedException();
    }
}