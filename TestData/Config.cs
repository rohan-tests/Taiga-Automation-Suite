
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TestData
{
    public class Config
    {
        private static readonly JObject json = JObject.Parse(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, @"..\..\..\TestData\config.json")));
        public static string BaseUrl => json["baseUrl"]!.ToString();
        public static string Username => json["username"]!.ToString();
        public static string Password => json["password"]!.ToString();
        public static string BaseApiUrl => json["baseApiUrl"]!.ToString();
        public static bool BoardReset => (bool)json["boardReset"]!;
        public static int ProjectId => (int)json["projectId"]!;

    }
}