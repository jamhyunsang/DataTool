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
        private List<TableInfo> TableInfoList = null;

        private Setting Form_Setting = new Setting();
        #endregion

        #region Member Method
        private void SetProjectInfo()
        {
            var SettingInfo = User.Instance.GetCurrentSetting();

            projectKey.Text = SettingInfo.Name;
            excelPath.Text = SettingInfo.ExcelPath;
            resourcesPath.Text = SettingInfo.ResourcesPath;
            tablePath.Text = SettingInfo.TablePath;
            dbPath.Text = SettingInfo.DBPath;
        }
        #endregion

        #region C# Method
        #region Form
        private void Main_Load(object sender, EventArgs e)
        {
            if (User.Instance.IsSettingEmpty())
            {
                Form_Setting.ShowDialog();
            }
            else
            {
                SetProjectInfo();
            }
        }

        private void Form_Setting_FormClosed(object? sender, FormClosedEventArgs e)
        {
        }
        #endregion

        #region Button Event
        private void Btn_Setting_Click(object sender, EventArgs e)
        {
            Form_Setting.ShowDialog();
        }

        private void ListBox_Table_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ListBox = sender as ListBox;
            if (ListBox != null && !string.IsNullOrEmpty(ListBox.Text))
            {
                var selectTable = TableInfoList.Where(Data => Data.SheetName.Equals(ListBox.Text)).SingleOrDefault();

                var tableData = Core.GetTable(selectTable.ExcelName, selectTable.SheetName);

                tableDataView.DataSource = tableData;
            }
        }

        private void Btn_ExcelLoad_Click(object sender, EventArgs e)
        {
            TableInfoList = Core.GetTableList();

            ListBox_Table.Items.Clear();
            ListBox_Table.Items.AddRange(TableInfoList.Select(Data => Data.SheetName).ToArray());
        }

        private void Btn_TableCSExport_Click(object sender, EventArgs e)
        {
            Core.MakeTable();
        }

        private void Btn_DataExport_Click(object sender, EventArgs e)
        {
            Core.ExportTable();
        }
        #endregion
        #endregion
    }
}