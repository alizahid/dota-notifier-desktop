using Hanssens.Net;

namespace Dota_Notifier
{
    public class Storage
    {
        private static readonly LocalStorage storage = new LocalStorage();

        static Storage()
        {
            storage.Load();
        }

        public static string Get(string key)
        {
            if (storage.Exists(key))
            {
                return storage.Get(key).ToString();
            }

            return null;
        }

        public static void Put(string key, string value)
        {
            storage.Store(key, value);
            storage.Persist();
        }

        public static void Clear()
        {
            storage.Clear();
            storage.Destroy();
        }
    }
}
