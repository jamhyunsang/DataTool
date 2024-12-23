namespace DataExportTool
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            Size size = new Size(1366, 768);
            this.Size = size;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Form_Setting.FormClosed += Form_Setting_FormClosed;
        }

        #region Member Property
        private ProjectData? project = null;
        Setting Form_Setting = new Setting();
        #endregion

        #region Member Method
        private void SetProjectInfo()
        {
            projectKey.Text = project!.projectKey;
            excelPath.Text = project.excelPath;
            resourcesPath.Text = project.resourcesPath;
            addressablePath.Text = project.addressablePath;
            isResourcesData.Text = $"{project.isResourcesData}";
            tablePath.Text = project.tablePath;
            dbPath.Text = project.dbPath;
        }
        #endregion

        #region C# Method
        #region Form
        private void Main_Load(object sender, EventArgs e)
        {

            if (User.Instance.IsEmptyProject())
            {
                Form_Setting.ShowDialog();
            }
            else
            {
                project = User.Instance.GetCurrentProjectData();
                SetProjectInfo();
            }
        }
        #endregion

        #region Button Event
        private void Btn_Setting_Click(object sender, EventArgs e)
        {
            Form_Setting.ShowDialog();
        }

        private void Form_Setting_FormClosed(object? sender, FormClosedEventArgs e)
        {
            SetProjectInfo();
        }

        private void ListBox_Table_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ListBox = sender as ListBox;
            if (ListBox != null)
            {

            }
        }

        private void Btn_ExcelLoad_Click(object sender, EventArgs e)
        {
            var Data = Core.Load_TableList();

            if (Data != null)
            {

            }
        }
        #endregion
        #endregion
    }
}