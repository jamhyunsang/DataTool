using ExcelDataReader;
using System.Data;

public static class Core
{
    public static List<TableInfo> GetTableList()
    {
        var SettingInfo = User.Instance.GetCurrentSetting();

        List<TableInfo> result = new List<TableInfo>();

        string TablePath = Path.Combine(SettingInfo.ExcelPath, Def.TableListName);

        using (var Stream = File.Open(TablePath, FileMode.Open, FileAccess.Read))
        {
            using (var Reader = ExcelReaderFactory.CreateReader(Stream))
            {
                var DataSet = Reader.AsDataSet();

                var TableList = OptimizeTable(DataSet.Tables["TableList"]);

                for (int count = 1; count < TableList.Rows.Count; count++)
                {
                    TableInfo TableInfo = new TableInfo();
                    TableInfo.ExcelName = TableList.Rows[count][0].ToString();
                    TableInfo.SheetName = TableList.Rows[count][1].ToString();
                    result.Add(TableInfo);
                }

                return result;
            }
        }
    }

    public static DataTable GetTable(string TableName, string TableSheetName)
    {
        var SettingInfo = User.Instance.GetCurrentSetting();

        string TablePath = Path.Combine(SettingInfo.ExcelPath, $"{TableName}.xlsx");

        using (var Stream = File.Open(TablePath, FileMode.Open, FileAccess.Read))
        {
            using (var Reader = ExcelReaderFactory.CreateReader(Stream))
            {
                var DataSet = Reader.AsDataSet();

                var Table = OptimizeTable(DataSet.Tables[$"{TableSheetName}"]);

                return Table;
            }
        }
    }

    private static DataTable OptimizeTable(DataTable Table)
    {
        int StartRow = 1;
        int EndRow = -1;
        int StartCol = 1;
        int EndCol = -1;

        for (int Row = 0; Row < Table.Rows.Count; Row++)
        {
            for (int Column = 0; Column < Table.Columns.Count; Column++)
            {
                var CellValue = Table.Rows[Row][Column]?.ToString()?.Trim();

                if (CellValue == "@")
                {
                    if (Row > EndRow)
                        EndRow = Row;

                    if (Column > EndCol)
                        EndCol = Column;
                }
            }
        }

        DataTable extractedData = new DataTable(Table.Rows[StartRow][StartCol].ToString());

        for (int i = StartCol; i < EndCol; i++)
        {
            extractedData.Columns.Add($"{Table.Rows[2][i].ToString()}");
        }

        for (int i = StartRow + 1; i < EndRow; i++)
        {
            DataRow newRow = extractedData.NewRow();
            for (int j = StartCol; j < EndCol; j++)
            {
                newRow[j - 1] = Table.Rows[i][j];
            }
            extractedData.Rows.Add(newRow);
        }

        return extractedData;
    }

    public static void ExportTable()
    {
        var SettingInfo = User.Instance.GetCurrentSetting();

        var TableList = GetTableList();

        foreach (var Table in TableList)
        {
            string TablePath = Path.Combine(SettingInfo.ExcelPath, $"{Table.ExcelName}.xlsx");

            using (var Stream = File.Open(TablePath, FileMode.Open, FileAccess.Read))
            {
                string DataPath = string.Empty;

                using (var Reader = ExcelReaderFactory.CreateReader(Stream))
                {
                    var DataSet = Reader.AsDataSet();

                    var TableData = OptimizeTable(DataSet.Tables[Table.SheetName]);

                    if (Table.IsLocalTable)
                        DataPath = Path.Combine(SettingInfo.ResourcesPath, $"{TableData.TableName}.txt");
                    else
                        DataPath = Path.Combine(SettingInfo.AddressablePath, $"{TableData.TableName}.txt");

                    List<List<string>> Rows = new List<List<string>>();

                    for (int Row = 2; Row < TableData.Rows.Count; Row++)
                    {
                        List<string> Columns = new List<string>();
                        for (int Col = 0; Col < TableData.Columns.Count; Col++)
                        {
                            Columns.Add(TableData.Rows[Row][Col].ToString());
                        }

                        Rows.Add(Columns);
                    }

                    var Compress = Util.Compress(Util.ToJson(Rows));
                    var Encrypt = Util.Encrypt(Compress);

                    File.WriteAllText(DataPath, Encrypt);
                }
            }
        }
    }

    public static void MakeTable()
    {

    }
}
