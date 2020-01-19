using EasyHttp.Http;

namespace Dota_Notifier
{
    class Api
    {
        public const string READY_CHECK = "READY_CHECK";
        public const string GAME_IS_READY = "GAME_IS_READY";

        private static readonly HttpClient client = new HttpClient("https://us-central1-dota-5233a.cloudfunctions.net");

        public static void Notify(string type)
        {
            string id = Storage.Get("id");

            client.Post("/notify", new { id, type }, HttpContentTypes.ApplicationJson);
        }
    }
}
