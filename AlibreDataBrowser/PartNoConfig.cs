using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace Bolsover.DataBrowser;

public partial class PartNoConfig : UserControl
{
    private readonly PartNoManager _partNoManager = new();
    private IList selectedItems;
    private readonly ToolTip saveTooltip = new();
    private readonly ToolTip applyTooltip = new();
    private readonly ToolTip cancelTooltip = new();

    public PartNoConfig()
    {
        InitializeComponent();
        bindings();
        SetupToolTips();

    }

    public IList SelectedItems
    {
        get => selectedItems;
        set
        {
            selectedItems = value;
            this.labelInfo.Text = "Info " + selectedItems.Count + " files to renumber.";
        }
    }
    
    private void SetupToolTips()
    {
        saveTooltip.SetToolTip(this.button2,
            "Saves the settings for the next part numbering session.");
        applyTooltip.SetToolTip(this.button1,
            "Updates the selected files with the new part numbers.");
        cancelTooltip.SetToolTip(this.buttonCancel,
            "Cancels this dialog without updating files.");
    }

    private void bindings()
    {
        textBoxPrefix.DataBindings.Add("Text", _partNoManager.PartNoSetting, "Prefix");
        textBoxSuffix.DataBindings.Add("Text", _partNoManager.PartNoSetting, "Suffix");
        nextNumberSpinner.DataBindings.Add("Value", _partNoManager.PartNoSetting, "PartNo");
        stepSpinner.DataBindings.Add("Value", _partNoManager.PartNoSetting, "SkipNo");
        textBoxExample.DataBindings.Add("Text", _partNoManager.PartNoSetting, "Example");
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
        Hide();

    }
    
    private void buttonSave_Click(object sender, EventArgs e)
    {
        PartNoManager.SaveConfig();

    }
    
    private void buttonApply_Click(object sender, EventArgs e)
    {
        foreach (var oItem in selectedItems)
        {
            var fileSystem = (AlibreFileSystem) oItem;

            if (fileSystem.Info.Extension.ToUpper().StartsWith(".AD_P") |
                fileSystem.Info.Extension.StartsWith(".AD_A") | fileSystem.Info.Extension.StartsWith(".AD_S"))
            {
                this.labelInfo.Text = "Info updating " + fileSystem.Name;
                var session = AlibreConnector.RetrieveSessionForFile(fileSystem);
                fileSystem.AlibrePartNo = _partNoManager.PartNoSetting.Prefix + _partNoManager.PartNoSetting.PartNo +
                                          _partNoManager.PartNoSetting.Suffix;
                _partNoManager.PartNoSetting.PartNo += _partNoManager.PartNoSetting.SkipNo;
                this.nextNumberSpinner.Value = _partNoManager.PartNoSetting.PartNo;
                var designProperties = session.DesignProperties;
                designProperties.Number = (string) fileSystem.AlibrePartNo;
                session.Close(true);
            }
            PartNoManager.SaveConfig();
        }
        this.labelInfo.Text = "Info done updating";
    }
    
    [Serializable()]
    public class PartNoSetting
    {
        private string prefix;
        private string suffix;
        private int partNo;
        private int skipNo;
        private string example;

        public string Prefix
        {
            get => prefix;
            set
            {
                prefix = value;
               
                InvokePropertyChanged(new PropertyChangedEventArgs("prefix"));
                Example = prefix + partNo + suffix;
                
            }
        }

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void InvokePropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = PropertyChanged;
            Example = prefix + partNo + suffix;
            if (handler != null)
            {
                handler(this, e);
                
            }
          
        }

        #endregion

        public string Suffix
        {
            get => suffix;
            set
            {
                suffix = value;
                
                InvokePropertyChanged(new PropertyChangedEventArgs("suffix"));
                // Example = prefix + partNo + suffix;
            }
        }

        public int PartNo
        {
            get => partNo;
            set
            {
                partNo = value;
                
                InvokePropertyChanged(new PropertyChangedEventArgs("partNo"));
                // Example = prefix + partNo + suffix;
            }
        }

        public int SkipNo
        {
            get => skipNo;
            set
            {
                skipNo = value;
                
                InvokePropertyChanged(new PropertyChangedEventArgs("skipNo"));
                // Example = prefix + partNo + suffix;
            }
        }

        public string Example
        {
            get => prefix + partNo + suffix;
            set
            {
                example = value; 
              //  InvokePropertyChanged(new PropertyChangedEventArgs("example"));
            }
        }
    }

 private class PartNoManager
    {
        private static PartNoSetting partNoSetting = new();

        public PartNoManager()
        {
            Initialize();
        }

        public PartNoSetting PartNoSetting
        {
            get => partNoSetting;
            set => partNoSetting = value;
        }

        private void Initialize()
        {
            LoadConfig();
        }

        public void LoadConfig()
        {
            var directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                "\\DataBrowser";
            var directoryInfo = new DirectoryInfo(directoryPath);
            if (!directoryInfo.Exists) Directory.CreateDirectory(directoryPath);
            var filepath = directoryPath + "\\partnumber.settings";
            var fileInfo = new FileInfo(filepath);
            if (fileInfo.Exists)
            {
                var srReader = File.OpenText(filepath);
                var tType = partNoSetting.GetType();
                var xsSerializer = new System.Xml.Serialization.XmlSerializer(tType);
                var oData = xsSerializer.Deserialize(srReader);
                partNoSetting = (PartNoSetting) oData;
                srReader.Close();
            }
        }

        // Save configuration file
        public static void SaveConfig()
        {
            var directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                "\\DataBrowser";
            var directoryInfo = new DirectoryInfo(directoryPath);
            if (!directoryInfo.Exists) Directory.CreateDirectory(directoryPath);
            var filepath = directoryPath + "\\partnumber.settings";
            var swWriter = File.CreateText(filepath);
            var tType = partNoSetting.GetType();
            if (tType.IsSerializable)
            {
                var xsSerializer = new System.Xml.Serialization.XmlSerializer(tType);
                xsSerializer.Serialize(swWriter, partNoSetting);
                swWriter.Close();
            }
        }
    }
  
}