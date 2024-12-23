using Newtonsoft.Json;
public static class Util
{
    public static string ToJson(object Obj)
    {
        return JsonConvert.SerializeObject(Obj);
    }

    public static T ToObject<T>(string Str)
    {
        return JsonConvert.DeserializeObject<T>(Str);
    }
}