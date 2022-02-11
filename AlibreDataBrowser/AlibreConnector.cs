using System.Diagnostics;
using System.Runtime.CompilerServices;
using AlibreX;

namespace Bolsover.DataBrowser;

public class AlibreConnector
{
    private static readonly IAutomationHook hook;
    private static readonly IADRoot root;

    static AlibreConnector()
    {
        try
        {
            hook = new AutomationHook();
            hook.Initialize(null, null, null, false, 0);
            root = hook.Root as IADRoot;
        }
        catch
        {
            Debug.WriteLine("Failed to connect to Alibre.");
        }
    }

    public static void TerminateAll()
    {
        root.TerminateAll();
    }

    public static IADMaterialLibraries RetrieveMaterialLibrariesForRoot()
    {
        return root.MaterialLibraries;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public static IADDesignSession RetrieveSessionForFile(AlibreFileSystem alibreFileSystem)
    {
        return (IADDesignSession) root.OpenFile(alibreFileSystem.FullName);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public static IADDesignSession OpenInAlibre(AlibreFileSystem alibreFileSystem)
    {
        return (IADDesignSession) root.OpenFileEx(alibreFileSystem.FullName, true);
    }
}