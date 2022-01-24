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
    private readonly AlibreConnector alibreConnector = new();

    public DataBrowserForm()
    {
        InitializeComponent();
        setupColumns();
        setupTree();
        FormClosed += (sender, args) => System.Environment.Exit(0); ;
    }

    private void setupColumns()
    {
        ConfigureAspectGetters();
        ConfigureAspectPutters();
       
    }

    /*
     * Configures all the AspectPutter methods for individual columns
     */
    private void ConfigureAspectPutters()
    {
        olvColumnAlibreDescription.AspectPutter = (rowObject, value) =>
        {
            ((AlibreFileSystem) rowObject).AlibreDescription = (string) value;
            var session = alibreConnector.RetrieveSessionForFile((AlibreFileSystem) rowObject);
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

        olvColumnAlibrePartNo.AspectPutter = (rowObject, value) =>
        {
            ((AlibreFileSystem) rowObject).AlibrePartNo = (string) value;
            var session = alibreConnector.RetrieveSessionForFile((AlibreFileSystem) rowObject);
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

        olvColumnAlibreComment.AspectPutter = (rowObject, value) =>
        {
            ((AlibreFileSystem) rowObject).AlibreComment = (string) value;
            var session = alibreConnector.RetrieveSessionForFile((AlibreFileSystem) rowObject);
            var designProperties = session.DesignProperties;
            designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_COMMENT, (string) value);

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
        olvColumnAlibreCreatedDate.AspectPutter = (rowObject, value) =>
        {
            ((AlibreFileSystem) rowObject).AlibreCreatedDate = (DateTime) value;
            var session = alibreConnector.RetrieveSessionForFile((AlibreFileSystem) rowObject);

            var designProperties = session.DesignProperties;
            designProperties.ExtendedDesignProperty(ADExtendedDesignProperty.AD_CREATED_DATE,
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
        treeListView.CanExpandGetter = rowObject => ((AlibreFileSystem) rowObject).IsDirectory;
        treeListView.ChildrenGetter = rowObject =>
        {
            try
            {
                return ((AlibreFileSystem) rowObject).GetFileSystemInfos();
            }
            catch (UnauthorizedAccessException ex)
            {
                BeginInvoke((MethodInvoker) delegate
                {
                    treeListView.Collapse(rowObject);
                    MessageBox.Show(this, ex.Message, "ObjectListViewDemo", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                });
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
                alibreConnector.RetrieveAlibreData((AlibreFileSystem) rowObject);
            else
                alibreConnector.ResetAlibreData((AlibreFileSystem) rowObject);

            return value;
        };
        
        treeListView.CellEditStarting += new CellEditEventHandler(this.HandleCellEditStarting);
    }


    private void buttonFilter_Click(object sender, EventArgs e)
    {
        treeListView.ModelFilter = new ModelFilter(rowObject =>
        {
            if (((AlibreFileSystem) rowObject).IsDirectory) return true;
            return ((AlibreFileSystem) rowObject).Info.Extension.StartsWith(".AD_");
        });
    }
    
    private void HandleCellEditStarting(object sender, CellEditEventArgs e) {
       // only checked items should be editable
        if (!((AlibreFileSystem) e.RowObject).IsChecked)
        {
            e.Cancel = true;
        }
// fix up size of cell editor
        Rectangle r = e.CellBounds;
        e.Control.Bounds = r;
        
     

    }


 
}