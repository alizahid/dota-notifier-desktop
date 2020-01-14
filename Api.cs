using EasyHttp.Http;
using System.Windows;

namespace Dota_Notifier
{
    class Api
    {
        private static readonly HttpClient client = new HttpClient("https://us-central1-dota-5233a.cloudfunctions.net");

        public static void Notify(string id)
        {
            client.Post("/notify", new { id }, HttpContentTypes.ApplicationJson);
        }
    }
}
