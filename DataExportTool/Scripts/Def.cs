public static class Def
{
    public static string registryPath = "PROJECTS_SETTING";
}

public class ProjectData
{
    public string projectKey = "NewProject";        // 프로젝트 키

    public string excelPath = string.Empty;         // 엑셀 위치

    public string resourcesPath = string.Empty;     // 리소스 폴더 위치
    public string addressablePath = string.Empty;   // 어드레서블 폴더 위치
    public bool isResourcesData = true;             // 데이터 저장 위치 설정

    public string tablePath = string.Empty;         // 테이블 폴더 위치
    public string dbPath = string.Empty;            // 디비 주소
}

public class ProjectSaveData
{
    public string projectKey = string.Empty;
    public Dictionary<string, ProjectData> projects = new Dictionary<string, ProjectData>();
}