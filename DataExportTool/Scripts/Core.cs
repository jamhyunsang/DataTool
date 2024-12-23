using ExcelDataReader;
using MongoDB.Bson.Serialization.Conventions;
using System.Data;

public static class Core
{
    public static DataTable? Load_TableList()
    {
        var project = User.Instance.GetCurrentProjectData();
        if (project != null)
        {
            string TablePath = $"{project.excelPath}\\TableList.xlsx";
            using (var stream = File.Open(TablePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var DataSet = reader.AsDataSet();
                    return DataSet.Tables[0];
                }
            }
        }

        return null;
    }

    public static DataTable? Load_TableData(string TableName, string TableSheetName)
    {
        var project = User.Instance.GetCurrentProjectData();

        if (project != null)
        {
            string TablePath = $"{project.excelPath}\\TableList.xlsx";
            using (var stream = File.Open(TablePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var DataSet = reader.AsDataSet();
                    return DataSet.Tables[0];
                }
            }
        }

        return null;
    }
}
