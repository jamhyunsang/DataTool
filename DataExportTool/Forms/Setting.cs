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
        private SettingInfo SettingInfo = null;
        #endregion

        #region Member Method
        private void SetProjectInfo()
        {
            projectKey.Text = SettingInfo.Name;
            excelPath.Text = SettingInfo.ExcelPath;
            resourcesPath.Text = SettingInfo.ResourcesPath;
            addressablePath.Text = SettingInfo.AddressablePath;
            isResourceData.Checked = SettingInfo.IsResourcesData;
            tablePath.Text = SettingInfo.TablePath;
            dbPath.Text = SettingInfo.DBPath;
        }

        private void SetComboBoxList()
        {
            Combo_Project.Items.Clear();
            Combo_Project.Items.AddRange(User.Instance.GetMySettingsKeys());
        }
        #endregion

        #region C# Method
        #region Form
        private void Setting_Load(object sender, EventArgs e)
        {
            if (User.Instance.IsSettingEmpty())
            {
                SettingInfo = new SettingInfo();
            }
            else
            {
                SetComboBoxList();
                SettingInfo = User.Instance.GetCurrentSetting();
            }

            SetProjectInfo();
        }
        #endregion

        #region ComboBox
        private void Combo_Project_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox == null)
                return;

            string? SelectItem = comboBox.SelectedItem as string;

            if (SelectItem == null)
                return;

            SettingInfo = User.Instance.GetSetting(SelectItem);

            SetProjectInfo();
        }
        #endregion

        #region Button Event
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            User.Instance.SaveSettingInfo(SettingInfo);
        }

        private void Btn_OK_Click(object sender, EventArgs e)
        {
            if (User.Instance.IsSettingEmpty())
            {
                MessageBox.Show("세팅이 없습니다.", "알림", MessageBoxButtons.OK);
                return;
            }

            User.Instance.ChangeCurrentSetting(SettingInfo.Name);

            Close();
        }

        private void Btn_NewSetting_Click(object sender, EventArgs e)
        {
            SettingInfo = new SettingInfo();
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
            var TextBox = sender as TextBox;

            if (TextBox != null && SettingInfo != null)
            {
                SettingInfo.Name = TextBox.Text;
            }
        }

        private void Text_TablePath_TextChanged(object sender, EventArgs e)
        {
            var TextBox = sender as TextBox;

            if (TextBox != null && SettingInfo != null)
            {
                SettingInfo.ExcelPath = Path.Combine(TextBox.Text);
            }
        }

        private void Text_ClientSTRPath_TextChanged(object sender, EventArgs e)
        {
            var TextBox = sender as TextBox;

            if (TextBox != null && SettingInfo != null)
            {
                SettingInfo.ResourcesPath = Path.Combine(TextBox.Text);
            }
        }

        private void Text_BundleSTRPath_TextChanged(object sender, EventArgs e)
        {
            var TextBox = sender as TextBox;

            if (TextBox != null && SettingInfo != null)
            {
                SettingInfo.AddressablePath = Path.Combine(TextBox.Text);
            }
        }

        private void Text_TableCSPath_TextChanged(object sender, EventArgs e)
        {
            var TextBox = sender as TextBox;

            if (TextBox != null && SettingInfo != null)
            {
                SettingInfo.TablePath = Path.Combine(TextBox.Text);
            }
        }

        private void Text_TableDataPath_TextChanged(object sender, EventArgs e)
        {
            var TextBox = sender as TextBox;

            if (TextBox != null && SettingInfo != null)
            {
                SettingInfo.TablePath = Path.Combine(TextBox.Text);
            }
        }

        private void Text_DBPath_TextChanged(object sender, EventArgs e)
        {
            var TextBox = sender as TextBox;

            if (TextBox != null && SettingInfo != null)
            {
                SettingInfo.DBPath = Path.Combine(TextBox.Text);
            }
        }
        #endregion
        #endregion
    }
}
