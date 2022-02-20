using System;
using System.Collections;
using System.Reflection;
using System.Windows.Forms;
using AlibreX;

namespace Bolsover.DataBrowser;

public partial class MaterialsBrowser : Form
{
    private MaterialNode Root;

    public MaterialsBrowser()
    {
        InitializeComponent();
        Root = PrepareMaterialsTree();
        setupColumns();
        setupTree();
    }

    private MaterialNode PrepareMaterialsTree()
    {
        var libraries = AlibreConnector.RetrieveMaterialLibrariesForRoot();
        Root = new MaterialNode("Material Library");
        foreach (IADMaterialLibrary library in libraries)
        {
            var child = new MaterialNode(library.Name);
            Root.AddChild(child);

            foreach (IADMaterial material in library.Materials)
            {
                var materialNode = new MaterialNode(material.Name);
                materialNode.Material = material;
                materialNode.Guid = GetAlibreMaterialGuid(material);
                Console.WriteLine(material.Name + " : " + materialNode.Guid);
                child.AddChild(materialNode);
            }

            WalkMaterials(library, child, child);
        }

        return Root;
    }

    public string GetAlibreMaterialGuid(object obj)
    {
        var t = obj.GetType();
        var fieldInfo = t.GetField("alibreMaterial",
            BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
            BindingFlags.Static);
        var alibreMaterial = fieldInfo.GetValue(obj);
        var t2 = alibreMaterial.GetType();
        var propertyInfo2 = t2.GetProperty("Guid",
            BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
            BindingFlags.Static);
        var guid = propertyInfo2.GetValue(alibreMaterial);
        return (string) guid;
    }


    /*
     * Need to make this recursive
     */
    private void WalkMaterials(IADMaterialLibrary library, MaterialNode parent, MaterialNode toplevel)
    {
        foreach (IADMaterialLibraryFolder folder in library.Folders)
        {
            var f = new MaterialNode(folder.Name);
            parent.AddChild(f);

            foreach (IADMaterial material in folder.Materials)
            {
                var materialNode = new MaterialNode(material.Name);
                f.AddChild(materialNode);
                materialNode.Material = material;
                materialNode.Guid = GetAlibreMaterialGuid(material);
                Console.WriteLine(material.Name + " : " + materialNode.Guid);
                // if this subMaterial is also in the toplevel materials remove from top level
                toplevel.RemoveChild(materialNode);
            }

            foreach (IADMaterialLibrary subLibrary in folder.SubFolders) WalkMaterials(subLibrary, f, toplevel);
        }
    }


    private void setupColumns()
    {
        ConfigureAspectGetters();
    }


    private void ConfigureAspectGetters()
    {
        olvColumnName.AspectGetter = rowObject => ((MaterialNode) rowObject).NodeName;
    }


    private void setupTree()
    {
        treeListView1.CanExpandGetter = rowObject => ((MaterialNode) rowObject).CanExpand;
        treeListView1.ChildrenGetter = rowObject =>
        {
            try
            {
                return ((MaterialNode) rowObject).ChList;
            }
            catch (UnauthorizedAccessException ex)
            {
                BeginInvoke((MethodInvoker) delegate { treeListView1.Collapse(rowObject); });
                return new ArrayList();
            }
        };
        var roots = new ArrayList();
        roots.Add(PrepareMaterialsTree());
        treeListView1.Roots = roots;
    }
}