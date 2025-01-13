namespace DataExportTool
{
    partial class Setting
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
            components = new System.ComponentModel.Container();
            projectKey = new TextBox();
            excelPath = new TextBox();
            resourcesPath = new TextBox();
            addressablePath = new TextBox();
            dbPath = new TextBox();
            tablePath = new TextBox();
            Group_Info = new GroupBox();
            isResourceData = new CheckBox();
            dbPathTitle = new Label();
            tablePathTItle = new Label();
            isResourcesDataTitle = new Label();
            addressablePathTitle = new Label();
            resourcesPathTitle = new Label();
            excelPathTitle = new Label();
            projectKeyTitle = new Label();
            ProjectSelect = new Label();
            Combo_Project = new ComboBox();
            tablePathButton = new Button();
            addressablePathButton = new Button();
            resourcesPathButton = new Button();
            excelPathButton = new Button();
            Btn_NewSetting = new Button();
            Btn_Save = new Button();
            Btn_Remove = new Button();
            Btn_OK = new Button();
            imageList1 = new ImageList(components);
            Group_Info.SuspendLayout();
            SuspendLayout();
            // 
            // projectKey
            // 
            projectKey.Location = new Point(6, 81);
            projectKey.Name = "projectKey";
            projectKey.Size = new Size(1029, 23);
            projectKey.TabIndex = 0;
            projectKey.TextChanged += Text_ProjectName_TextChanged;
            // 
            // excelPath
            // 
            excelPath.Location = new Point(6, 125);
            excelPath.Name = "excelPath";
            excelPath.Size = new Size(897, 23);
            excelPath.TabIndex = 1;
            excelPath.TextChanged += Text_TablePath_TextChanged;
            // 
            // resourcesPath
            // 
            resourcesPath.Location = new Point(6, 169);
            resourcesPath.Name = "resourcesPath";
            resourcesPath.Size = new Size(897, 23);
            resourcesPath.TabIndex = 3;
            resourcesPath.TextChanged += Text_ClientSTRPath_TextChanged;
            // 
            // addressablePath
            // 
            addressablePath.Location = new Point(6, 213);
            addressablePath.Name = "addressablePath";
            addressablePath.Size = new Size(897, 23);
            addressablePath.TabIndex = 4;
            addressablePath.TextChanged += Text_BundleSTRPath_TextChanged;
            // 
            // dbPath
            // 
            dbPath.Location = new Point(6, 345);
            dbPath.Name = "dbPath";
            dbPath.Size = new Size(1029, 23);
            dbPath.TabIndex = 5;
            dbPath.TextChanged += Text_DBPath_TextChanged;
            // 
            // tablePath
            // 
            tablePath.Location = new Point(6, 301);
            tablePath.Name = "tablePath";
            tablePath.Size = new Size(897, 23);
            tablePath.TabIndex = 6;
            tablePath.TextChanged += Text_TableDataPath_TextChanged;
            // 
            // Group_Info
            // 
            Group_Info.Controls.Add(isResourceData);
            Group_Info.Controls.Add(dbPathTitle);
            Group_Info.Controls.Add(tablePathTItle);
            Group_Info.Controls.Add(isResourcesDataTitle);
            Group_Info.Controls.Add(addressablePathTitle);
            Group_Info.Controls.Add(resourcesPathTitle);
            Group_Info.Controls.Add(excelPathTitle);
            Group_Info.Controls.Add(projectKeyTitle);
            Group_Info.Controls.Add(ProjectSelect);
            Group_Info.Controls.Add(Combo_Project);
            Group_Info.Controls.Add(tablePathButton);
            Group_Info.Controls.Add(addressablePathButton);
            Group_Info.Controls.Add(resourcesPathButton);
            Group_Info.Controls.Add(excelPathButton);
            Group_Info.Controls.Add(projectKey);
            Group_Info.Controls.Add(dbPath);
            Group_Info.Controls.Add(tablePath);
            Group_Info.Controls.Add(excelPath);
            Group_Info.Controls.Add(addressablePath);
            Group_Info.Controls.Add(resourcesPath);
            Group_Info.Location = new Point(12, 12);
            Group_Info.Name = "Group_Info";
            Group_Info.Size = new Size(1041, 561);
            Group_Info.TabIndex = 7;
            Group_Info.TabStop = false;
            Group_Info.Text = "ProjectInfo";
            // 
            // isResourceData
            // 
            isResourceData.AutoSize = true;
            isResourceData.Location = new Point(6, 261);
            isResourceData.Name = "isResourceData";
            isResourceData.Size = new Size(15, 14);
            isResourceData.TabIndex = 25;
            isResourceData.UseVisualStyleBackColor = true;
            // 
            // dbPathTitle
            // 
            dbPathTitle.AutoSize = true;
            dbPathTitle.Location = new Point(6, 327);
            dbPathTitle.Name = "dbPathTitle";
            dbPathTitle.Size = new Size(47, 15);
            dbPathTitle.TabIndex = 24;
            dbPathTitle.Text = "DBPath";
            // 
            // tablePathTItle
            // 
            tablePathTItle.AutoSize = true;
            tablePathTItle.Location = new Point(6, 283);
            tablePathTItle.Name = "tablePathTItle";
            tablePathTItle.Size = new Size(59, 15);
            tablePathTItle.TabIndex = 23;
            tablePathTItle.Text = "TablePath";
            // 
            // isResourcesDataTitle
            // 
            isResourcesDataTitle.AutoSize = true;
            isResourcesDataTitle.Location = new Point(6, 239);
            isResourcesDataTitle.Name = "isResourcesDataTitle";
            isResourcesDataTitle.Size = new Size(93, 15);
            isResourcesDataTitle.TabIndex = 22;
            isResourcesDataTitle.Text = "isResourcesData";
            // 
            // addressablePathTitle
            // 
            addressablePathTitle.AutoSize = true;
            addressablePathTitle.Location = new Point(6, 195);
            addressablePathTitle.Name = "addressablePathTitle";
            addressablePathTitle.Size = new Size(95, 15);
            addressablePathTitle.TabIndex = 21;
            addressablePathTitle.Text = "AddressablePath";
            // 
            // resourcesPathTitle
            // 
            resourcesPathTitle.AutoSize = true;
            resourcesPathTitle.Location = new Point(6, 151);
            resourcesPathTitle.Name = "resourcesPathTitle";
            resourcesPathTitle.Size = new Size(84, 15);
            resourcesPathTitle.TabIndex = 20;
            resourcesPathTitle.Text = "ResourcesPath";
            // 
            // excelPathTitle
            // 
            excelPathTitle.AutoSize = true;
            excelPathTitle.Location = new Point(6, 107);
            excelPathTitle.Name = "excelPathTitle";
            excelPathTitle.Size = new Size(58, 15);
            excelPathTitle.TabIndex = 19;
            excelPathTitle.Text = "ExcelPath";
            // 
            // projectKeyTitle
            // 
            projectKeyTitle.AutoSize = true;
            projectKeyTitle.Location = new Point(6, 63);
            projectKeyTitle.Name = "projectKeyTitle";
            projectKeyTitle.Size = new Size(64, 15);
            projectKeyTitle.TabIndex = 18;
            projectKeyTitle.Text = "ProJectKey";
            // 
            // ProjectSelect
            // 
            ProjectSelect.AutoSize = true;
            ProjectSelect.Location = new Point(6, 19);
            ProjectSelect.Name = "ProjectSelect";
            ProjectSelect.Size = new Size(76, 15);
            ProjectSelect.TabIndex = 17;
            ProjectSelect.Text = "ProjectSelect";
            // 
            // Combo_Project
            // 
            Combo_Project.DropDownStyle = ComboBoxStyle.DropDownList;
            Combo_Project.FormattingEnabled = true;
            Combo_Project.Location = new Point(6, 37);
            Combo_Project.Name = "Combo_Project";
            Combo_Project.Size = new Size(1029, 23);
            Combo_Project.TabIndex = 12;
            Combo_Project.SelectedIndexChanged += Combo_Project_SelectedIndexChanged;
            // 
            // tablePathButton
            // 
            tablePathButton.Location = new Point(909, 301);
            tablePathButton.Name = "tablePathButton";
            tablePathButton.Size = new Size(126, 23);
            tablePathButton.TabIndex = 16;
            tablePathButton.Text = "TablePath";
            tablePathButton.UseVisualStyleBackColor = true;
            tablePathButton.Click += Btn_TableDataPath_Click;
            // 
            // addressablePathButton
            // 
            addressablePathButton.Location = new Point(909, 213);
            addressablePathButton.Name = "addressablePathButton";
            addressablePathButton.Size = new Size(126, 23);
            addressablePathButton.TabIndex = 14;
            addressablePathButton.Text = "AddressablePath";
            addressablePathButton.UseVisualStyleBackColor = true;
            addressablePathButton.Click += Btn_BundleSTRPath_Click;
            // 
            // resourcesPathButton
            // 
            resourcesPathButton.Location = new Point(909, 169);
            resourcesPathButton.Name = "resourcesPathButton";
            resourcesPathButton.Size = new Size(126, 23);
            resourcesPathButton.TabIndex = 13;
            resourcesPathButton.Text = "ResourcesPath";
            resourcesPathButton.UseVisualStyleBackColor = true;
            resourcesPathButton.Click += Btn_ClientSTRPath_Click;
            // 
            // excelPathButton
            // 
            excelPathButton.Location = new Point(909, 125);
            excelPathButton.Name = "excelPathButton";
            excelPathButton.Size = new Size(126, 23);
            excelPathButton.TabIndex = 7;
            excelPathButton.Text = "ExcelPath";
            excelPathButton.UseVisualStyleBackColor = true;
            excelPathButton.Click += Btn_TablePath_Click;
            // 
            // Btn_NewSetting
            // 
            Btn_NewSetting.Location = new Point(1059, 16);
            Btn_NewSetting.Name = "Btn_NewSetting";
            Btn_NewSetting.Size = new Size(193, 100);
            Btn_NewSetting.TabIndex = 8;
            Btn_NewSetting.Text = "NewSetting";
            Btn_NewSetting.UseVisualStyleBackColor = true;
            Btn_NewSetting.Click += Btn_NewSetting_Click;
            // 
            // Btn_Save
            // 
            Btn_Save.Location = new Point(1059, 122);
            Btn_Save.Name = "Btn_Save";
            Btn_Save.Size = new Size(193, 100);
            Btn_Save.TabIndex = 9;
            Btn_Save.Text = "Save";
            Btn_Save.UseVisualStyleBackColor = true;
            Btn_Save.Click += Btn_Save_Click;
            // 
            // Btn_Remove
            // 
            Btn_Remove.Location = new Point(1059, 334);
            Btn_Remove.Name = "Btn_Remove";
            Btn_Remove.Size = new Size(193, 100);
            Btn_Remove.TabIndex = 10;
            Btn_Remove.Text = "Remove";
            Btn_Remove.UseVisualStyleBackColor = true;
            Btn_Remove.Click += Btn_Remove_Click;
            // 
            // Btn_OK
            // 
            Btn_OK.Location = new Point(1059, 228);
            Btn_OK.Name = "Btn_OK";
            Btn_OK.Size = new Size(193, 100);
            Btn_OK.TabIndex = 11;
            Btn_OK.Text = "OK";
            Btn_OK.UseVisualStyleBackColor = true;
            Btn_OK.Click += Btn_OK_Click;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // Setting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 729);
            ControlBox = false;
            Controls.Add(Btn_OK);
            Controls.Add(Btn_Remove);
            Controls.Add(Btn_Save);
            Controls.Add(Btn_NewSetting);
            Controls.Add(Group_Info);
            Name = "Setting";
            Text = "Setting";
            Load += Setting_Load;
            Group_Info.ResumeLayout(false);
            Group_Info.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private TextBox projectKey;
        private TextBox excelPath;
        private TextBox resourcesPath;
        private TextBox addressablePath;
        private TextBox dbPath;
        private TextBox tablePath;
        private GroupBox Group_Info;
        private Button Btn_NewSetting;
        private Button Btn_Save;
        private Button Btn_Remove;
        private Button Btn_OK;
        private Button excelPathButton;
        private Button tablePathButton;
        private Button addressablePathButton;
        private Button resourcesPathButton;
        private Button Btn_TableCSPath;
        private ImageList imageList1;
        private ComboBox Combo_Project;
        private Label ProjectSelect;
        private Label projectKeyTitle;
        private Label dbPathTitle;
        private Label tablePathTItle;
        private Label isResourcesDataTitle;
        private Label addressablePathTitle;
        private Label resourcesPathTitle;
        private Label excelPathTitle;
        private CheckBox isResourceData;
    }
}