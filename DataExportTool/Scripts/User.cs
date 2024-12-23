using Microsoft.Win32;

public class User
{
    private static Lazy<User> instance = new Lazy<User>(() =>
    {
        var instance = new User();
        instance.Init();
        return instance;
    });

    public static User Instance
    {
        get { return instance.Value; }
    }

    private ProjectSaveData? projectSaveData = null;

    private void Init()
    {
        string RegistryPath = "Software\\DataExportTool";
        using (RegistryKey Key = Registry.CurrentUser.CreateSubKey(RegistryPath))
        {
            var RegistryData = Key.GetValue(Def.registryPath);
            if (RegistryData != null)
            {
                var Data = RegistryData.ToString();

                if (Data != null)
                {
                    projectSaveData = Util.ToObject<ProjectSaveData>(Data);
                }
                else
                {
                    projectSaveData = new ProjectSaveData();
                }
            }
            else
            {
                projectSaveData = new ProjectSaveData();
            }
        }
    }

    public void Save()
    {
        string RegistryPath = "Software\\DataExportTool";
        using (RegistryKey Key = Registry.CurrentUser.CreateSubKey(RegistryPath))
        {
            Key.SetValue(Def.registryPath, Util.ToJson(projectSaveData!));
        }
    }

    public void Save(ProjectData projectData)
    {
        if(IsExistProject(projectData.projectKey))
        {
            projectSaveData!.projects[projectData.projectKey] = projectData;
        }
        else
        {
            projectSaveData!.projects.Add(projectData.projectKey, projectData);
        }
        projectSaveData!.projectKey = projectData.projectKey;

        string RegistryPath = "Software\\DataExportTool";
        using (RegistryKey Key = Registry.CurrentUser.CreateSubKey(RegistryPath))
        {
            Key.SetValue(Def.registryPath, Util.ToJson(projectSaveData!));
        }
    }

    public void Save(string projectKey)
    {
        if (!IsExistProject(projectKey))
            return;

        projectSaveData!.projectKey = projectKey;

        string RegistryPath = "Software\\DataExportTool";
        using (RegistryKey Key = Registry.CurrentUser.CreateSubKey(RegistryPath))
        {
            Key.SetValue(Def.registryPath, Util.ToJson(projectSaveData!));
        }
    }

    public string GetProjectKey()
    {
        return projectSaveData!.projectKey;
    }

    public void SetProjectKey(string projectKey)
    {
        projectSaveData!.projectKey = projectKey;
    }

    public Dictionary<string, ProjectData> GetProjects()
    {
        return projectSaveData!.projects;
    }

    public ProjectData? GetCurrentProjectData()
    {
        if (string.IsNullOrEmpty(projectSaveData!.projectKey))
            return null;
        else
            return projectSaveData!.projects[projectSaveData!.projectKey];
    }

    public ProjectData? GetProjectData(string projectKey)
    {
        if (string.IsNullOrEmpty(projectKey))
            return null;
        else
        {
            if (IsExistProject(projectKey))
                return projectSaveData!.projects[projectKey];
            else
                return null;
        }
    }

    public bool IsExistProject(string projectKey)
    {
        return projectSaveData!.projects.ContainsKey(projectKey);
    }

    public bool IsEmptyProject()
    {
        return projectSaveData!.projects.Count == 0;
    }
}
