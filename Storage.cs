using Hanssens.Net;

namespace Dota_Notifier
{
    public class Storage
    {
        private readonly LocalStorage storage = new LocalStorage();

        public Storage()
        {
            storage.Load();
        }

        public string Get(string key)
        {
            if (storage.Exists(key))
            {
                return storage.Get(key).ToString();
            }

            return null;
        }

        public void Put(string key, string value)
        {
            storage.Store(key, value);
            storage.Persist();
        }

        public void Clear()
        {
            storage.Clear();
            storage.Destroy();
        }
    }
}
