using System.IO;
using HN.Bangumi.API.Models;
using HN.Bangumi.Configuration;
using Newtonsoft.Json;
using Windows.Storage;

namespace HN.Bangumi.Uwp.Configuration
{
    public class AppCache : IAppCache
    {
        public User User
        {
            get
            {
                var path = Path.Combine(ApplicationData.Current.TemporaryFolder.Path, "user.cache");
                if (!File.Exists(path))
                {
                    return null;
                }

                var json = File.ReadAllText(path);
                try
                {
                    return JsonConvert.DeserializeObject<User>(json);
                }
                catch (JsonException)
                {
                    return null;
                }
            }
            set
            {
                var path = Path.Combine(ApplicationData.Current.TemporaryFolder.Path, "user.cache");
                var json = JsonConvert.SerializeObject(value);
                File.WriteAllText(path, json);
            }
        }
    }
}
