namespace DataExportTool
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Btn_ExcelLoad = new Button();
            Btn_Setting = new Button();
            Group_Info = new GroupBox();
            Btn_STRExport = new Button();
            Btn_DataExport = new Button();
            Btn_TableCSExport = new Button();
            excelPath = new Label();
            excelPathTitle = new Label();
            dbPath = new Label();
            projectKey = new Label();
            dbPathTitle = new Label();
            projectKeyTitle = new Label();
            resourcesPathTitle = new Label();
            tablePath = new Label();
            tablePathTatle = new Label();
            isResourcesData = new Label();
            isResourcesDataPathTitle = new Label();
            addressablePath = new Label();
            addressablePathTitle = new Label();
            resourcesPath = new Label();
            ListBox_Table = new ListBox();
            tableDataView = new DataGridView();
            Group_Info.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tableDataView).BeginInit();
            SuspendLayout();
            // 
            // Btn_ExcelLoad
            // 
            Btn_ExcelLoad.Location = new Point(1172, 47);
            Btn_ExcelLoad.Name = "Btn_ExcelLoad";
            Btn_ExcelLoad.Size = new Size(148, 29);
            Btn_ExcelLoad.TabIndex = 0;
            Btn_ExcelLoad.Text = "엑셀 로드";
            Btn_ExcelLoad.UseVisualStyleBackColor = true;
            Btn_ExcelLoad.Click += Btn_ExcelLoad_Click;
            // 
            // Btn_Setting
            // 
            Btn_Setting.Location = new Point(1172, 12);
            Btn_Setting.Name = "Btn_Setting";
            Btn_Setting.Size = new Size(148, 29);
            Btn_Setting.TabIndex = 2;
            Btn_Setting.Text = "설정";
            Btn_Setting.UseVisualStyleBackColor = true;
            Btn_Setting.Click += Btn_Setting_Click;
            // 
            // Group_Info
            // 
            Group_Info.Controls.Add(Btn_STRExport);
            Group_Info.Controls.Add(Btn_DataExport);
            Group_Info.Controls.Add(Btn_TableCSExport);
            Group_Info.Controls.Add(excelPath);
            Group_Info.Controls.Add(excelPathTitle);
            Group_Info.Controls.Add(dbPath);
            Group_Info.Controls.Add(projectKey);
            Group_Info.Controls.Add(dbPathTitle);
            Group_Info.Controls.Add(projectKeyTitle);
            Group_Info.Controls.Add(resourcesPathTitle);
            Group_Info.Controls.Add(tablePath);
            Group_Info.Controls.Add(tablePathTatle);
            Group_Info.Controls.Add(isResourcesData);
            Group_Info.Controls.Add(isResourcesDataPathTitle);
            Group_Info.Controls.Add(addressablePath);
            Group_Info.Controls.Add(addressablePathTitle);
            Group_Info.Controls.Add(resourcesPath);
            Group_Info.Controls.Add(Btn_Setting);
            Group_Info.Controls.Add(Btn_ExcelLoad);
            Group_Info.Location = new Point(12, 12);
            Group_Info.Name = "Group_Info";
            Group_Info.Size = new Size(1326, 236);
            Group_Info.TabIndex = 3;
            Group_Info.TabStop = false;
            Group_Info.Text = "ProjectInfo";
            // 
            // Btn_STRExport
            // 
            Btn_STRExport.Location = new Point(1172, 152);
            Btn_STRExport.Name = "Btn_STRExport";
            Btn_STRExport.Size = new Size(148, 29);
            Btn_STRExport.TabIndex = 19;
            Btn_STRExport.Text = "스트링 추출";
            Btn_STRExport.UseVisualStyleBackColor = true;
            // 
            // Btn_DataExport
            // 
            Btn_DataExport.Location = new Point(1172, 117);
            Btn_DataExport.Name = "Btn_DataExport";
            Btn_DataExport.Size = new Size(148, 29);
            Btn_DataExport.TabIndex = 18;
            Btn_DataExport.Text = "테입블 데이터 추출";
            Btn_DataExport.UseVisualStyleBackColor = true;
            Btn_DataExport.Click += Btn_DataExport_Click;
            // 
            // Btn_TableCSExport
            // 
            Btn_TableCSExport.Location = new Point(1172, 82);
            Btn_TableCSExport.Name = "Btn_TableCSExport";
            Btn_TableCSExport.Size = new Size(148, 29);
            Btn_TableCSExport.TabIndex = 17;
            Btn_TableCSExport.Text = "테이블 CS 추출";
            Btn_TableCSExport.UseVisualStyleBackColor = true;
            Btn_TableCSExport.Click += Btn_TableCSExport_Click;
            // 
            // excelPath
            // 
            excelPath.AutoSize = true;
            excelPath.ForeColor = SystemColors.ControlText;
            excelPath.Location = new Point(6, 64);
            excelPath.Name = "excelPath";
            excelPath.Size = new Size(41, 15);
            excelPath.TabIndex = 16;
            excelPath.Text = "empty";
            // 
            // excelPathTitle
            // 
            excelPathTitle.AutoSize = true;
            excelPathTitle.ForeColor = SystemColors.ControlText;
            excelPathTitle.Location = new Point(6, 49);
            excelPathTitle.Name = "excelPathTitle";
            excelPathTitle.Size = new Size(58, 15);
            excelPathTitle.TabIndex = 15;
            excelPathTitle.Text = "ExcelPath";
            // 
            // dbPath
            // 
            dbPath.AutoSize = true;
            dbPath.Location = new Point(6, 214);
            dbPath.Name = "dbPath";
            dbPath.Size = new Size(41, 15);
            dbPath.TabIndex = 14;
            dbPath.Text = "empty";
            // 
            // projectKey
            // 
            projectKey.AutoSize = true;
            projectKey.Location = new Point(6, 34);
            projectKey.Name = "projectKey";
            projectKey.Size = new Size(41, 15);
            projectKey.TabIndex = 4;
            projectKey.Text = "empty";
            // 
            // dbPathTitle
            // 
            dbPathTitle.AutoSize = true;
            dbPathTitle.Location = new Point(6, 199);
            dbPathTitle.Name = "dbPathTitle";
            dbPathTitle.Size = new Size(47, 15);
            dbPathTitle.TabIndex = 13;
            dbPathTitle.Text = "DBPath";
            // 
            // projectKeyTitle
            // 
            projectKeyTitle.AutoSize = true;
            projectKeyTitle.Location = new Point(6, 19);
            projectKeyTitle.Name = "projectKeyTitle";
            projectKeyTitle.Size = new Size(63, 15);
            projectKeyTitle.TabIndex = 3;
            projectKeyTitle.Text = "ProjectKey";
            // 
            // resourcesPathTitle
            // 
            resourcesPathTitle.AutoSize = true;
            resourcesPathTitle.Location = new Point(6, 79);
            resourcesPathTitle.Name = "resourcesPathTitle";
            resourcesPathTitle.Size = new Size(84, 15);
            resourcesPathTitle.TabIndex = 5;
            resourcesPathTitle.Text = "ResourcesPath";
            // 
            // tablePath
            // 
            tablePath.AutoSize = true;
            tablePath.Location = new Point(6, 184);
            tablePath.Name = "tablePath";
            tablePath.Size = new Size(41, 15);
            tablePath.TabIndex = 12;
            tablePath.Text = "empty";
            // 
            // tablePathTatle
            // 
            tablePathTatle.AutoSize = true;
            tablePathTatle.Location = new Point(6, 169);
            tablePathTatle.Name = "tablePathTatle";
            tablePathTatle.Size = new Size(59, 15);
            tablePathTatle.TabIndex = 11;
            tablePathTatle.Text = "TablePath";
            // 
            // isResourcesData
            // 
            isResourcesData.AutoSize = true;
            isResourcesData.Location = new Point(6, 154);
            isResourcesData.Name = "isResourcesData";
            isResourcesData.Size = new Size(31, 15);
            isResourcesData.TabIndex = 10;
            isResourcesData.Text = "false";
            // 
            // isResourcesDataPathTitle
            // 
            isResourcesDataPathTitle.AutoSize = true;
            isResourcesDataPathTitle.Location = new Point(6, 139);
            isResourcesDataPathTitle.Name = "isResourcesDataPathTitle";
            isResourcesDataPathTitle.Size = new Size(93, 15);
            isResourcesDataPathTitle.TabIndex = 9;
            isResourcesDataPathTitle.Text = "isResourcesData";
            // 
            // addressablePath
            // 
            addressablePath.AutoSize = true;
            addressablePath.Location = new Point(6, 124);
            addressablePath.Name = "addressablePath";
            addressablePath.Size = new Size(41, 15);
            addressablePath.TabIndex = 8;
            addressablePath.Text = "empty";
            // 
            // addressablePathTitle
            // 
            addressablePathTitle.AutoSize = true;
            addressablePathTitle.Location = new Point(6, 109);
            addressablePathTitle.Name = "addressablePathTitle";
            addressablePathTitle.Size = new Size(95, 15);
            addressablePathTitle.TabIndex = 7;
            addressablePathTitle.Text = "AddressablePath";
            // 
            // resourcesPath
            // 
            resourcesPath.AutoSize = true;
            resourcesPath.Location = new Point(6, 94);
            resourcesPath.Name = "resourcesPath";
            resourcesPath.Size = new Size(41, 15);
            resourcesPath.TabIndex = 6;
            resourcesPath.Text = "empty";
            // 
            // ListBox_Table
            // 
            ListBox_Table.FormattingEnabled = true;
            ListBox_Table.ItemHeight = 15;
            ListBox_Table.Location = new Point(12, 254);
            ListBox_Table.Name = "ListBox_Table";
            ListBox_Table.Size = new Size(261, 469);
            ListBox_Table.TabIndex = 5;
            ListBox_Table.SelectedIndexChanged += ListBox_Table_SelectedIndexChanged;
            // 
            // tableDataView
            // 
            tableDataView.AllowUserToAddRows = false;
            tableDataView.AllowUserToDeleteRows = false;
            tableDataView.AllowUserToResizeColumns = false;
            tableDataView.AllowUserToResizeRows = false;
            tableDataView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tableDataView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tableDataView.ColumnHeadersVisible = false;
            tableDataView.Location = new Point(279, 254);
            tableDataView.Name = "tableDataView";
            tableDataView.ReadOnly = true;
            tableDataView.Size = new Size(1059, 469);
            tableDataView.TabIndex = 6;
            // 
            // Main
            // 
            ClientSize = new Size(1350, 729);
            Controls.Add(tableDataView);
            Controls.Add(ListBox_Table);
            Controls.Add(Group_Info);
            Name = "Main";
            Text = "DataExportTool";
            Load += Main_Load;
            Group_Info.ResumeLayout(false);
            Group_Info.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tableDataView).EndInit();
            ResumeLayout(false);
        }
        #endregion

        private Button Btn_ExcelLoad;
        private Button Btn_Setting;
        private GroupBox Group_Info;
        private Label projectKeyTitle;
        private Label isResourcesData;
        private Label isResourcesDataPathTitle;
        private Label addressablePath;
        private Label addressablePathTitle;
        private Label resourcesPath;
        private Label resourcesPathTitle;
        private Label projectKey;
        private Label dbPath;
        private Label dbPathTitle;
        private Label tablePath;
        private Label tablePathTatle;
        private Label excelPath;
        private Label excelPathTitle;
        private ListBox ListBox_Table;
        private DataGridView tableDataView;
        private Button Btn_STRExport;
        private Button Btn_DataExport;
        private Button Btn_TableCSExport;
        private TreeView treeView1;
    }
}
