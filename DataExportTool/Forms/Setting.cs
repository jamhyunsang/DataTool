namespace DataExportTool
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();

            Size size = new Size(1280, 768);
            this.Size = size;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        #region Member Property
        private ProjectData? projectData = null;
        #endregion

        #region Member Method
        private void SetProjectInfo()
        {
            projectKey.Text = projectData!.projectKey;
            excelPath.Text = projectData.excelPath;
            resourcesPath.Text = projectData.resourcesPath;
            addressablePath.Text = projectData.addressablePath;
            isResourceData.Checked = projectData.isResourcesData;
            tablePath.Text = projectData.tablePath;
            dbPath.Text = projectData.dbPath;
        }

        private void SetComboBoxList()
        {
            Combo_Project.Items.Clear();
            Combo_Project.Items.AddRange(User.Instance.GetProjects().Keys.ToArray());
        }
        #endregion

        #region C# Method
        #region Form
        private void Setting_Load(object sender, EventArgs e)
        {
            if(User.Instance.IsEmptyProject())
            {
                projectData = new ProjectData();
            }
            else
            {
                SetComboBoxList();
                projectData = User.Instance.GetCurrentProjectData();
            }
            
            SetProjectInfo();
        }

        private void Setting_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
        #endregion

        #region ComboBox
        private void Combo_Project_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox? comboBox = sender as ComboBox;
            if (comboBox == null)
                return;

            string? SelectItem = comboBox.SelectedItem as string;
            if (SelectItem == null)
                return;
            
            projectData = User.Instance.GetProjectData(SelectItem);
            SetProjectInfo();
        }
        #endregion

        #region Button Event
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            User.Instance.Save(projectData!);

            SetComboBoxList();

            Combo_Project.SelectedItem = projectData!.projectKey;
        }

        private void Btn_OK_Click(object sender, EventArgs e)
        {
            if (User.Instance.IsEmptyProject())
                return;
            
            User.Instance.Save(projectData!.projectKey);
            Close();
        }

        private void Btn_NewSetting_Click(object sender, EventArgs e)
        {
            projectData = new ProjectData();
            SetProjectInfo();
        }

        private void Btn_Remove_Click(object sender, EventArgs e)
        {
            SetProjectInfo();
        }

        private void Btn_TablePath_Click(object sender, EventArgs e)
        {
            var FolderBrowser = new FolderBrowserDialog();
            FolderBrowser.ShowDialog();
            excelPath.Text = FolderBrowser.SelectedPath;
        }

        private void Btn_ClientSTRPath_Click(object sender, EventArgs e)
        {
            var FolderBrowser = new FolderBrowserDialog();
            FolderBrowser.ShowDialog();
            resourcesPath.Text = FolderBrowser.SelectedPath;
        }

        private void Btn_BundleSTRPath_Click(object sender, EventArgs e)
        {
            var FolderBrowser = new FolderBrowserDialog();
            FolderBrowser.ShowDialog();
            addressablePath.Text = FolderBrowser.SelectedPath;
        }

        private void Btn_TableDataPath_Click(object sender, EventArgs e)
        {
            var FolderBrowser = new FolderBrowserDialog();
            FolderBrowser.ShowDialog();
            tablePath.Text = FolderBrowser.SelectedPath;
        }
        #endregion
         
        #region Text Event
        private void Text_ProjectName_TextChanged(object sender, EventArgs e)
        {
            TextBox? TextBox = sender as TextBox;

            if (TextBox != null && projectData != null)
            {
                projectData.projectKey = TextBox.Text;
            }
        }

        private void Text_TablePath_TextChanged(object sender, EventArgs e)
        {
            TextBox? TextBox = sender as TextBox;

            if (TextBox != null && projectData != null)
            {
                projectData.excelPath = TextBox.Text;
            }
        }

        private void Text_ClientSTRPath_TextChanged(object sender, EventArgs e)
        {
            TextBox? TextBox = sender as TextBox;

            if (TextBox != null && projectData != null)
            {
                projectData.resourcesPath = TextBox.Text;
            }
        }

        private void Text_BundleSTRPath_TextChanged(object sender, EventArgs e)
        {
            TextBox? TextBox = sender as TextBox;

            if (TextBox != null && projectData != null)
            {
                projectData.addressablePath = TextBox.Text;
            }
        }

        private void Text_TableCSPath_TextChanged(object sender, EventArgs e)
        {
            TextBox? TextBox = sender as TextBox;

            if (TextBox != null && projectData != null)
            {
                projectData.tablePath = TextBox.Text;
            }
        }

        private void Text_TableDataPath_TextChanged(object sender, EventArgs e)
        {
            TextBox? TextBox = sender as TextBox;

            if (TextBox != null && projectData != null)
            {
                projectData.tablePath = TextBox.Text;
            }
        }

        private void Text_DBPath_TextChanged(object sender, EventArgs e)
        {
            TextBox? TextBox = sender as TextBox;

            if (TextBox != null && projectData != null)
            {
                projectData.dbPath = TextBox.Text;
            }
        }
        #endregion
        #endregion
    }
}
