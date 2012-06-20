using System;

namespace SemeionModulesDesigner
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadXSDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveXMLAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.mainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(909, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadXSDToolStripMenuItem,
            this.loadXMLToolStripMenuItem,
            this.saveXMLAsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadXSDToolStripMenuItem
            // 
            this.loadXSDToolStripMenuItem.Name = "loadXSDToolStripMenuItem";
            this.loadXSDToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadXSDToolStripMenuItem.Text = "Load XSD";
            this.loadXSDToolStripMenuItem.Click += new System.EventHandler(this.LoadXSDToolStripMenuItemClick);
            // 
            // loadXMLToolStripMenuItem
            // 
            this.loadXMLToolStripMenuItem.Enabled = false;
            this.loadXMLToolStripMenuItem.Name = "loadXMLToolStripMenuItem";
            this.loadXMLToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadXMLToolStripMenuItem.Text = "Load XML";
            this.loadXMLToolStripMenuItem.Click += new System.EventHandler(this.LoadXmlToolStripMenuItemClick);
            // 
            // saveXMLAsToolStripMenuItem
            // 
            this.saveXMLAsToolStripMenuItem.Enabled = false;
            this.saveXMLAsToolStripMenuItem.Name = "saveXMLAsToolStripMenuItem";
            this.saveXMLAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveXMLAsToolStripMenuItem.Text = "Save XML as ...";
            this.saveXMLAsToolStripMenuItem.Click += new System.EventHandler(this.SaveXmlAsToolStripMenuItemClick);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // mainPanel
            // 
            this.mainPanel.AutoScroll = true;
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 24);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(909, 474);
            this.mainPanel.TabIndex = 1;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 498);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.mainMenuStrip);
            this.Name = "MainForm";
            this.Text = "Semeion Form Designer v1.0";
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadXSDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveXMLAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}

