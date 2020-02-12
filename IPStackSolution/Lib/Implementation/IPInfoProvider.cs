using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Lib.Service;
using Newtonsoft.Json;

namespace Lib.Implementaion {
    class IPInfoProvider : IIPInfoProvider, IDisposable

    {
        static HttpClient client = new HttpClient ();

        public async Task<IIPDetails> GetDetails (string uri) {
            IPDetails details = null;
            try {
                HttpResponseMessage response = await client.GetAsync (uri);
                if (response.IsSuccessStatusCode) {
                    string result = await response.Content.ReadAsStringAsync ();
                    details = JsonConvert.DeserializeObject<IPDetails> (result);
                }

            } catch (Exception ex) {
                //2do “IPServiceNotAvailableException
            }
            return details;
        }

        public void Dispose () {
            client.Dispose ();
        }

    }
}