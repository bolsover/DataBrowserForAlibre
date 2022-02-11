using System;
using System.Windows.Forms;

namespace Bolsover.DataBrowser;

public partial class PartNoConfiguration : UserControl
{
    public PartNoConfiguration()
    {
        InitializeComponent();
    }

    public class PartNoSetting
    {
        private string prefix;
        private string puffix;
        private int partNo;
        private int skipNo;
        private string example;

        public string Prefix
        {
            get => prefix;
            set => prefix = value;
        }

        public string Puffix
        {
            get => puffix;
            set => puffix = value;
        }

        public int PartNo
        {
            get => partNo;
            set => partNo = value;
        }

        public int SkipNo
        {
            get => skipNo;
            set => skipNo = value;
        }

        public string Example
        {
            get => example;
            set => example = value;
        }
    }
}