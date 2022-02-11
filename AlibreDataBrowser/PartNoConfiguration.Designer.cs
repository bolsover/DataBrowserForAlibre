using System.ComponentModel;

namespace Bolsover.DataBrowser;

partial class PartNoConfiguration
{
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.label1 = new System.Windows.Forms.Label();
        this.textBoxPrefix = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.textBoxSeedNumber = new System.Windows.Forms.TextBox();
        this.label3 = new System.Windows.Forms.Label();
        this.textBoxSkipCount = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.textBoxSuffix = new System.Windows.Forms.TextBox();
        this.buttonCancel = new System.Windows.Forms.Button();
        this.buttonApply = new System.Windows.Forms.Button();
        this.textBoxExample = new System.Windows.Forms.TextBox();
        this.label5 = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // label1
        // 
        this.label1.Location = new System.Drawing.Point(22, 33);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(100, 21);
        this.label1.TabIndex = 0;
        this.label1.Text = "Prefix";
        // 
        // textBoxPrefix
        // 
        this.textBoxPrefix.Location = new System.Drawing.Point(245, 30);
        this.textBoxPrefix.Name = "textBoxPrefix";
        this.textBoxPrefix.Size = new System.Drawing.Size(203, 20);
        this.textBoxPrefix.TabIndex = 1;
        // 
        // label2
        // 
        this.label2.Location = new System.Drawing.Point(21, 82);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(112, 18);
        this.label2.TabIndex = 2;
        this.label2.Text = "Seed Number";
        // 
        // textBoxSeedNumber
        // 
        this.textBoxSeedNumber.Location = new System.Drawing.Point(245, 80);
        this.textBoxSeedNumber.Name = "textBoxSeedNumber";
        this.textBoxSeedNumber.Size = new System.Drawing.Size(203, 20);
        this.textBoxSeedNumber.TabIndex = 3;
        // 
        // label3
        // 
        this.label3.Location = new System.Drawing.Point(21, 141);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(100, 17);
        this.label3.TabIndex = 4;
        this.label3.Text = "Skip Count";
        // 
        // textBoxSkipCount
        // 
        this.textBoxSkipCount.Location = new System.Drawing.Point(245, 138);
        this.textBoxSkipCount.Name = "textBoxSkipCount";
        this.textBoxSkipCount.Size = new System.Drawing.Size(201, 20);
        this.textBoxSkipCount.TabIndex = 5;
        // 
        // label4
        // 
        this.label4.Location = new System.Drawing.Point(22, 201);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(100, 23);
        this.label4.TabIndex = 6;
        this.label4.Text = "Suffix";
        // 
        // textBoxSuffix
        // 
        this.textBoxSuffix.Location = new System.Drawing.Point(245, 198);
        this.textBoxSuffix.Name = "textBoxSuffix";
        this.textBoxSuffix.Size = new System.Drawing.Size(203, 20);
        this.textBoxSuffix.TabIndex = 7;
        // 
        // buttonCancel
        // 
        this.buttonCancel.Location = new System.Drawing.Point(22, 299);
        this.buttonCancel.Name = "buttonCancel";
        this.buttonCancel.Size = new System.Drawing.Size(75, 23);
        this.buttonCancel.TabIndex = 8;
        this.buttonCancel.Text = "Cancel";
        this.buttonCancel.UseVisualStyleBackColor = true;
        // 
        // buttonApply
        // 
        this.buttonApply.Location = new System.Drawing.Point(245, 299);
        this.buttonApply.Name = "buttonApply";
        this.buttonApply.Size = new System.Drawing.Size(75, 23);
        this.buttonApply.TabIndex = 9;
        this.buttonApply.Text = "Apply";
        this.buttonApply.UseVisualStyleBackColor = true;
        // 
        // textBoxExample
        // 
        this.textBoxExample.Location = new System.Drawing.Point(245, 253);
        this.textBoxExample.Name = "textBoxExample";
        this.textBoxExample.Size = new System.Drawing.Size(196, 20);
        this.textBoxExample.TabIndex = 10;
        // 
        // label5
        // 
        this.label5.Location = new System.Drawing.Point(22, 256);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(100, 23);
        this.label5.TabIndex = 11;
        this.label5.Text = "Example";
        // 
        // PartNoConfiguration
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.Controls.Add(this.label5);
        this.Controls.Add(this.textBoxExample);
        this.Controls.Add(this.buttonApply);
        this.Controls.Add(this.buttonCancel);
        this.Controls.Add(this.textBoxSuffix);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.textBoxSkipCount);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.textBoxSeedNumber);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.textBoxPrefix);
        this.Controls.Add(this.label1);
        this.Name = "PartNoConfiguration";
        this.Size = new System.Drawing.Size(504, 382);
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.TextBox textBoxPrefix;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox textBoxSeedNumber;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox textBoxSkipCount;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox textBoxSuffix;
    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.Button buttonApply;
    private System.Windows.Forms.TextBox textBoxExample;
    private System.Windows.Forms.Label label5;

    private System.Windows.Forms.Label label1;

    #endregion
}