namespace Starfield.GUI.Forms
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
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.sceneViewportPanel = new System.Windows.Forms.Panel();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fieldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fieldToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objListTabControl = new System.Windows.Forms.TabControl();
            this.hitTriggersTab = new System.Windows.Forms.TabPage();
            this.hitTriggersListBox = new System.Windows.Forms.ListBox();
            this.entrancesTab = new System.Windows.Forms.TabPage();
            this.entrancesListBox = new System.Windows.Forms.ListBox();
            this.hitTableTab = new System.Windows.Forms.TabPage();
            this.hitDefinitionsListBox = new System.Windows.Forms.ListBox();
            this.type8EntriesTab = new System.Windows.Forms.TabPage();
            this.type8EntriesListBox = new System.Windows.Forms.ListBox();
            this.type9EntriesTab = new System.Windows.Forms.TabPage();
            this.type9EntriesListBox = new System.Windows.Forms.ListBox();
            this.type10EntriesTab = new System.Windows.Forms.TabPage();
            this.type10EntriesListBox = new System.Windows.Forms.ListBox();
            this.type11EntriesTab = new System.Windows.Forms.TabPage();
            this.type11EntriesListBox = new System.Windows.Forms.ListBox();
            this.msgTriggersTab = new System.Windows.Forms.TabPage();
            this.msgTriggersListBox = new System.Windows.Forms.ListBox();
            this.type18EntriesTab = new System.Windows.Forms.TabPage();
            this.type18EntriesListBox = new System.Windows.Forms.ListBox();
            this.type19EntriesTab = new System.Windows.Forms.TabPage();
            this.type19EntriesListBox = new System.Windows.Forms.ListBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.type22EntriesListBox = new System.Windows.Forms.ListBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.objPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.cameraPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.layerEditorPanel = new System.Windows.Forms.Panel();
            this.layerIndexInput = new System.Windows.Forms.NumericUpDown();
            this.layerEditorLabel = new System.Windows.Forms.Label();
            this.layerObjectAddButton = new System.Windows.Forms.Button();
            this.layerObjectDeleteButton = new System.Windows.Forms.Button();
            this.layerObjectCloneButton = new System.Windows.Forms.Button();
            this.mainMenuStrip.SuspendLayout();
            this.objListTabControl.SuspendLayout();
            this.hitTriggersTab.SuspendLayout();
            this.entrancesTab.SuspendLayout();
            this.hitTableTab.SuspendLayout();
            this.type8EntriesTab.SuspendLayout();
            this.type9EntriesTab.SuspendLayout();
            this.type10EntriesTab.SuspendLayout();
            this.type11EntriesTab.SuspendLayout();
            this.msgTriggersTab.SuspendLayout();
            this.type18EntriesTab.SuspendLayout();
            this.type19EntriesTab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.layerEditorPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layerIndexInput)).BeginInit();
            this.SuspendLayout();
            // 
            // sceneViewportPanel
            // 
            this.sceneViewportPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sceneViewportPanel.Location = new System.Drawing.Point(347, 27);
            this.sceneViewportPanel.Name = "sceneViewportPanel";
            this.sceneViewportPanel.Size = new System.Drawing.Size(567, 742);
            this.sceneViewportPanel.TabIndex = 0;
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(106, 24);
            this.mainMenuStrip.TabIndex = 1;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fieldToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // fieldToolStripMenuItem
            // 
            this.fieldToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fieldToolStripMenuItem1});
            this.fieldToolStripMenuItem.Name = "fieldToolStripMenuItem";
            this.fieldToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.fieldToolStripMenuItem.Text = "Open";
            this.fieldToolStripMenuItem.Click += new System.EventHandler(this.fieldToolStripMenuItem_Click);
            // 
            // fieldToolStripMenuItem1
            // 
            this.fieldToolStripMenuItem1.Name = "fieldToolStripMenuItem1";
            this.fieldToolStripMenuItem1.Size = new System.Drawing.Size(99, 22);
            this.fieldToolStripMenuItem1.Text = "Field";
            this.fieldToolStripMenuItem1.Click += new System.EventHandler(this.fieldToolStripMenuItem1_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // objListTabControl
            // 
            this.objListTabControl.Controls.Add(this.hitTriggersTab);
            this.objListTabControl.Controls.Add(this.entrancesTab);
            this.objListTabControl.Controls.Add(this.hitTableTab);
            this.objListTabControl.Controls.Add(this.type8EntriesTab);
            this.objListTabControl.Controls.Add(this.type9EntriesTab);
            this.objListTabControl.Controls.Add(this.type10EntriesTab);
            this.objListTabControl.Controls.Add(this.type11EntriesTab);
            this.objListTabControl.Controls.Add(this.msgTriggersTab);
            this.objListTabControl.Controls.Add(this.type18EntriesTab);
            this.objListTabControl.Controls.Add(this.type19EntriesTab);
            this.objListTabControl.Controls.Add(this.tabPage1);
            this.objListTabControl.Location = new System.Drawing.Point(3, 31);
            this.objListTabControl.Name = "objListTabControl";
            this.objListTabControl.SelectedIndex = 0;
            this.objListTabControl.Size = new System.Drawing.Size(323, 674);
            this.objListTabControl.TabIndex = 0;
            // 
            // hitTriggersTab
            // 
            this.hitTriggersTab.Controls.Add(this.hitTriggersListBox);
            this.hitTriggersTab.Location = new System.Drawing.Point(4, 22);
            this.hitTriggersTab.Name = "hitTriggersTab";
            this.hitTriggersTab.Padding = new System.Windows.Forms.Padding(3);
            this.hitTriggersTab.Size = new System.Drawing.Size(315, 648);
            this.hitTriggersTab.TabIndex = 0;
            this.hitTriggersTab.Text = "Hit triggers";
            this.hitTriggersTab.UseVisualStyleBackColor = true;
            // 
            // hitTriggersListBox
            // 
            this.hitTriggersListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hitTriggersListBox.FormattingEnabled = true;
            this.hitTriggersListBox.Location = new System.Drawing.Point(3, 3);
            this.hitTriggersListBox.Name = "hitTriggersListBox";
            this.hitTriggersListBox.Size = new System.Drawing.Size(309, 642);
            this.hitTriggersListBox.TabIndex = 0;
            // 
            // entrancesTab
            // 
            this.entrancesTab.Controls.Add(this.entrancesListBox);
            this.entrancesTab.Location = new System.Drawing.Point(4, 22);
            this.entrancesTab.Name = "entrancesTab";
            this.entrancesTab.Padding = new System.Windows.Forms.Padding(3);
            this.entrancesTab.Size = new System.Drawing.Size(315, 648);
            this.entrancesTab.TabIndex = 1;
            this.entrancesTab.Text = "Entrances";
            this.entrancesTab.UseVisualStyleBackColor = true;
            // 
            // entrancesListBox
            // 
            this.entrancesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.entrancesListBox.FormattingEnabled = true;
            this.entrancesListBox.Location = new System.Drawing.Point(3, 3);
            this.entrancesListBox.Name = "entrancesListBox";
            this.entrancesListBox.Size = new System.Drawing.Size(309, 642);
            this.entrancesListBox.TabIndex = 1;
            // 
            // hitTableTab
            // 
            this.hitTableTab.Controls.Add(this.hitDefinitionsListBox);
            this.hitTableTab.Location = new System.Drawing.Point(4, 22);
            this.hitTableTab.Name = "hitTableTab";
            this.hitTableTab.Size = new System.Drawing.Size(315, 648);
            this.hitTableTab.TabIndex = 3;
            this.hitTableTab.Text = "Hit table";
            this.hitTableTab.UseVisualStyleBackColor = true;
            // 
            // hitDefinitionsListBox
            // 
            this.hitDefinitionsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hitDefinitionsListBox.FormattingEnabled = true;
            this.hitDefinitionsListBox.Location = new System.Drawing.Point(0, 0);
            this.hitDefinitionsListBox.Name = "hitDefinitionsListBox";
            this.hitDefinitionsListBox.Size = new System.Drawing.Size(315, 648);
            this.hitDefinitionsListBox.TabIndex = 2;
            // 
            // type8EntriesTab
            // 
            this.type8EntriesTab.Controls.Add(this.type8EntriesListBox);
            this.type8EntriesTab.Location = new System.Drawing.Point(4, 22);
            this.type8EntriesTab.Name = "type8EntriesTab";
            this.type8EntriesTab.Size = new System.Drawing.Size(315, 648);
            this.type8EntriesTab.TabIndex = 4;
            this.type8EntriesTab.Text = "Type 8 entries";
            this.type8EntriesTab.UseVisualStyleBackColor = true;
            // 
            // type8EntriesListBox
            // 
            this.type8EntriesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.type8EntriesListBox.FormattingEnabled = true;
            this.type8EntriesListBox.Location = new System.Drawing.Point(0, 0);
            this.type8EntriesListBox.Name = "type8EntriesListBox";
            this.type8EntriesListBox.Size = new System.Drawing.Size(315, 648);
            this.type8EntriesListBox.TabIndex = 3;
            // 
            // type9EntriesTab
            // 
            this.type9EntriesTab.Controls.Add(this.type9EntriesListBox);
            this.type9EntriesTab.Location = new System.Drawing.Point(4, 22);
            this.type9EntriesTab.Name = "type9EntriesTab";
            this.type9EntriesTab.Size = new System.Drawing.Size(315, 648);
            this.type9EntriesTab.TabIndex = 5;
            this.type9EntriesTab.Text = "Type 9 entries";
            this.type9EntriesTab.UseVisualStyleBackColor = true;
            // 
            // type9EntriesListBox
            // 
            this.type9EntriesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.type9EntriesListBox.FormattingEnabled = true;
            this.type9EntriesListBox.Location = new System.Drawing.Point(0, 0);
            this.type9EntriesListBox.Name = "type9EntriesListBox";
            this.type9EntriesListBox.Size = new System.Drawing.Size(315, 648);
            this.type9EntriesListBox.TabIndex = 2;
            // 
            // type10EntriesTab
            // 
            this.type10EntriesTab.Controls.Add(this.type10EntriesListBox);
            this.type10EntriesTab.Location = new System.Drawing.Point(4, 22);
            this.type10EntriesTab.Name = "type10EntriesTab";
            this.type10EntriesTab.Size = new System.Drawing.Size(315, 648);
            this.type10EntriesTab.TabIndex = 6;
            this.type10EntriesTab.Text = "Type 10 entries";
            this.type10EntriesTab.UseVisualStyleBackColor = true;
            // 
            // type10EntriesListBox
            // 
            this.type10EntriesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.type10EntriesListBox.FormattingEnabled = true;
            this.type10EntriesListBox.Location = new System.Drawing.Point(0, 0);
            this.type10EntriesListBox.Name = "type10EntriesListBox";
            this.type10EntriesListBox.Size = new System.Drawing.Size(315, 648);
            this.type10EntriesListBox.TabIndex = 2;
            // 
            // type11EntriesTab
            // 
            this.type11EntriesTab.Controls.Add(this.type11EntriesListBox);
            this.type11EntriesTab.Location = new System.Drawing.Point(4, 22);
            this.type11EntriesTab.Name = "type11EntriesTab";
            this.type11EntriesTab.Size = new System.Drawing.Size(315, 648);
            this.type11EntriesTab.TabIndex = 7;
            this.type11EntriesTab.Text = "Type 11 entries";
            this.type11EntriesTab.UseVisualStyleBackColor = true;
            // 
            // type11EntriesListBox
            // 
            this.type11EntriesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.type11EntriesListBox.FormattingEnabled = true;
            this.type11EntriesListBox.Location = new System.Drawing.Point(0, 0);
            this.type11EntriesListBox.Name = "type11EntriesListBox";
            this.type11EntriesListBox.Size = new System.Drawing.Size(315, 648);
            this.type11EntriesListBox.TabIndex = 2;
            // 
            // msgTriggersTab
            // 
            this.msgTriggersTab.Controls.Add(this.msgTriggersListBox);
            this.msgTriggersTab.Location = new System.Drawing.Point(4, 22);
            this.msgTriggersTab.Name = "msgTriggersTab";
            this.msgTriggersTab.Size = new System.Drawing.Size(315, 648);
            this.msgTriggersTab.TabIndex = 2;
            this.msgTriggersTab.Text = "Message Triggers";
            this.msgTriggersTab.UseVisualStyleBackColor = true;
            // 
            // msgTriggersListBox
            // 
            this.msgTriggersListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.msgTriggersListBox.FormattingEnabled = true;
            this.msgTriggersListBox.Location = new System.Drawing.Point(0, 0);
            this.msgTriggersListBox.Name = "msgTriggersListBox";
            this.msgTriggersListBox.Size = new System.Drawing.Size(315, 648);
            this.msgTriggersListBox.TabIndex = 1;
            // 
            // type18EntriesTab
            // 
            this.type18EntriesTab.Controls.Add(this.type18EntriesListBox);
            this.type18EntriesTab.Location = new System.Drawing.Point(4, 22);
            this.type18EntriesTab.Name = "type18EntriesTab";
            this.type18EntriesTab.Size = new System.Drawing.Size(315, 648);
            this.type18EntriesTab.TabIndex = 8;
            this.type18EntriesTab.Text = "Type 18 entries";
            this.type18EntriesTab.UseVisualStyleBackColor = true;
            // 
            // type18EntriesListBox
            // 
            this.type18EntriesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.type18EntriesListBox.FormattingEnabled = true;
            this.type18EntriesListBox.Location = new System.Drawing.Point(0, 0);
            this.type18EntriesListBox.Name = "type18EntriesListBox";
            this.type18EntriesListBox.Size = new System.Drawing.Size(315, 648);
            this.type18EntriesListBox.TabIndex = 2;
            // 
            // type19EntriesTab
            // 
            this.type19EntriesTab.Controls.Add(this.type19EntriesListBox);
            this.type19EntriesTab.Location = new System.Drawing.Point(4, 22);
            this.type19EntriesTab.Name = "type19EntriesTab";
            this.type19EntriesTab.Size = new System.Drawing.Size(315, 648);
            this.type19EntriesTab.TabIndex = 9;
            this.type19EntriesTab.Text = "Type 19 entries";
            this.type19EntriesTab.UseVisualStyleBackColor = true;
            // 
            // type19EntriesListBox
            // 
            this.type19EntriesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.type19EntriesListBox.FormattingEnabled = true;
            this.type19EntriesListBox.Location = new System.Drawing.Point(0, 0);
            this.type19EntriesListBox.Name = "type19EntriesListBox";
            this.type19EntriesListBox.Size = new System.Drawing.Size(315, 648);
            this.type19EntriesListBox.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.type22EntriesListBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(315, 648);
            this.tabPage1.TabIndex = 10;
            this.tabPage1.Text = "Type 22 entries";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // type22EntriesListBox
            // 
            this.type22EntriesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.type22EntriesListBox.FormattingEnabled = true;
            this.type22EntriesListBox.Location = new System.Drawing.Point(0, 0);
            this.type22EntriesListBox.Name = "type22EntriesListBox";
            this.type22EntriesListBox.Size = new System.Drawing.Size(315, 648);
            this.type22EntriesListBox.TabIndex = 2;
            // 
            // tabControl2
            // 
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Location = new System.Drawing.Point(920, 27);
            this.tabControl2.Multiline = true;
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(278, 742);
            this.tabControl2.TabIndex = 2;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.objPropertyGrid);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(270, 716);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Object properties";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // objPropertyGrid
            // 
            this.objPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objPropertyGrid.Location = new System.Drawing.Point(3, 3);
            this.objPropertyGrid.Name = "objPropertyGrid";
            this.objPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.objPropertyGrid.Size = new System.Drawing.Size(264, 710);
            this.objPropertyGrid.TabIndex = 0;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.cameraPropertyGrid);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(270, 716);
            this.tabPage6.TabIndex = 2;
            this.tabPage6.Text = "Default camera";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // cameraPropertyGrid
            // 
            this.cameraPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cameraPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.cameraPropertyGrid.Name = "cameraPropertyGrid";
            this.cameraPropertyGrid.Size = new System.Drawing.Size(270, 716);
            this.cameraPropertyGrid.TabIndex = 0;
            // 
            // layerEditorPanel
            // 
            this.layerEditorPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.layerEditorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.layerEditorPanel.Controls.Add(this.layerObjectDeleteButton);
            this.layerEditorPanel.Controls.Add(this.layerObjectAddButton);
            this.layerEditorPanel.Controls.Add(this.layerIndexInput);
            this.layerEditorPanel.Controls.Add(this.layerObjectCloneButton);
            this.layerEditorPanel.Controls.Add(this.layerEditorLabel);
            this.layerEditorPanel.Controls.Add(this.objListTabControl);
            this.layerEditorPanel.Location = new System.Drawing.Point(12, 27);
            this.layerEditorPanel.Name = "layerEditorPanel";
            this.layerEditorPanel.Size = new System.Drawing.Size(329, 744);
            this.layerEditorPanel.TabIndex = 0;
            // 
            // layerIndexInput
            // 
            this.layerIndexInput.Location = new System.Drawing.Point(283, 5);
            this.layerIndexInput.Name = "layerIndexInput";
            this.layerIndexInput.Size = new System.Drawing.Size(36, 20);
            this.layerIndexInput.TabIndex = 1;
            // 
            // layerEditorLabel
            // 
            this.layerEditorLabel.AutoSize = true;
            this.layerEditorLabel.Location = new System.Drawing.Point(10, 8);
            this.layerEditorLabel.Name = "layerEditorLabel";
            this.layerEditorLabel.Size = new System.Drawing.Size(62, 13);
            this.layerEditorLabel.TabIndex = 1;
            this.layerEditorLabel.Text = "Layer editor";
            // 
            // layerObjectAddButton
            // 
            this.layerObjectAddButton.Location = new System.Drawing.Point(39, 711);
            this.layerObjectAddButton.Name = "layerObjectAddButton";
            this.layerObjectAddButton.Size = new System.Drawing.Size(75, 23);
            this.layerObjectAddButton.TabIndex = 1;
            this.layerObjectAddButton.Text = "Add";
            this.layerObjectAddButton.UseVisualStyleBackColor = true;
            // 
            // layerObjectDeleteButton
            // 
            this.layerObjectDeleteButton.Location = new System.Drawing.Point(201, 711);
            this.layerObjectDeleteButton.Name = "layerObjectDeleteButton";
            this.layerObjectDeleteButton.Size = new System.Drawing.Size(75, 23);
            this.layerObjectDeleteButton.TabIndex = 2;
            this.layerObjectDeleteButton.Text = "Delete";
            this.layerObjectDeleteButton.UseVisualStyleBackColor = true;
            this.layerObjectDeleteButton.Click += new System.EventHandler(this.layerObjectDeleteButton_Click);
            // 
            // layerObjectCloneButton
            // 
            this.layerObjectCloneButton.Location = new System.Drawing.Point(120, 711);
            this.layerObjectCloneButton.Name = "layerObjectCloneButton";
            this.layerObjectCloneButton.Size = new System.Drawing.Size(75, 23);
            this.layerObjectCloneButton.TabIndex = 3;
            this.layerObjectCloneButton.Text = "Clone";
            this.layerObjectCloneButton.UseVisualStyleBackColor = true;
            this.layerObjectCloneButton.Click += new System.EventHandler(this.layerObjectCloneButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1210, 783);
            this.Controls.Add(this.layerEditorPanel);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.sceneViewportPanel);
            this.Controls.Add(this.mainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Starfield";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.objListTabControl.ResumeLayout(false);
            this.hitTriggersTab.ResumeLayout(false);
            this.entrancesTab.ResumeLayout(false);
            this.hitTableTab.ResumeLayout(false);
            this.type8EntriesTab.ResumeLayout(false);
            this.type9EntriesTab.ResumeLayout(false);
            this.type10EntriesTab.ResumeLayout(false);
            this.type11EntriesTab.ResumeLayout(false);
            this.msgTriggersTab.ResumeLayout(false);
            this.type18EntriesTab.ResumeLayout(false);
            this.type19EntriesTab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.layerEditorPanel.ResumeLayout(false);
            this.layerEditorPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layerIndexInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel sceneViewportPanel;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.TabControl objListTabControl;
        private System.Windows.Forms.TabPage hitTriggersTab;
        private System.Windows.Forms.TabPage entrancesTab;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage msgTriggersTab;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.ListBox hitTriggersListBox;
        private System.Windows.Forms.ListBox entrancesListBox;
        private System.Windows.Forms.ListBox msgTriggersListBox;
        private System.Windows.Forms.PropertyGrid cameraPropertyGrid;
        private System.Windows.Forms.TabPage hitTableTab;
        private System.Windows.Forms.ListBox hitDefinitionsListBox;
        private System.Windows.Forms.PropertyGrid objPropertyGrid;
        private System.Windows.Forms.Panel layerEditorPanel;
        private System.Windows.Forms.Label layerEditorLabel;
        private System.Windows.Forms.NumericUpDown layerIndexInput;
        private System.Windows.Forms.TabPage type8EntriesTab;
        private System.Windows.Forms.TabPage type9EntriesTab;
        private System.Windows.Forms.TabPage type10EntriesTab;
        private System.Windows.Forms.TabPage type11EntriesTab;
        private System.Windows.Forms.TabPage type18EntriesTab;
        private System.Windows.Forms.TabPage type19EntriesTab;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ToolStripMenuItem fieldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fieldToolStripMenuItem1;
        private System.Windows.Forms.ListBox type8EntriesListBox;
        private System.Windows.Forms.ListBox type9EntriesListBox;
        private System.Windows.Forms.ListBox type10EntriesListBox;
        private System.Windows.Forms.ListBox type11EntriesListBox;
        private System.Windows.Forms.ListBox type18EntriesListBox;
        private System.Windows.Forms.ListBox type19EntriesListBox;
        private System.Windows.Forms.ListBox type22EntriesListBox;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.Button layerObjectCloneButton;
        private System.Windows.Forms.Button layerObjectDeleteButton;
        private System.Windows.Forms.Button layerObjectAddButton;
    }
}

