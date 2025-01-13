using System.Security.Cryptography;
using System.Text;

public static class Def
{
    public static readonly string SettingsPath = Path.Combine(AppContext.BaseDirectory,"Settings.json");
    public static readonly string TableListName = "TableList.xlsx";

    public static readonly byte[] EncryptKey = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes("YouHyunsang"));
    public static readonly byte[] EncryptIV = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes("JamHyunsang"));
}

public class SettingInfo
{
    public string Name = "NewProject";              // 프로젝트 

    public string ExcelPath = string.Empty;         // 엑셀 위치

    public string ResourcesPath = string.Empty;     // 리소스 폴더 위치
    public string AddressablePath = string.Empty;   // 어드레서블 폴더 위치
    public bool IsResourcesData = true;             // 데이터 저장 위치 설정`

    public string TablePath = string.Empty;         // 테이블 폴더 위치
    public string DBPath = string.Empty;            // 디비 주소
}

public class Settings
{
    public string CurrentSetting = string.Empty;
    public Dictionary<string, SettingInfo> MySettings = new Dictionary<string, SettingInfo>();
}

public class TableInfo
{
    public string ExcelName = string.Empty;
    public string SheetName = string.Empty;
    public bool IsLocalTable = false;
}