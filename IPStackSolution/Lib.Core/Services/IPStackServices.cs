using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lib.Data.Entities;
using Lib.Data.Repositories;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Lib.Core.Services {
    public class IPStackServices {

        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;
        private IIPDetailsDataProvider _dataProvider;
        private readonly AppSettings _appSettings;

        public IPStackServices (IMemoryCache memoryCache, IOptions<AppSettings> settings, IIPDetailsDataProvider dataProvider, IMapper mapper) {

            _cache = memoryCache;
            _dataProvider = dataProvider;
            _mapper = mapper;
            _appSettings = settings.Value;

        }

        public async Task<Models.IPDetails> GetData (string ip) {

            Models.IPDetails result = null;

            string uri = $"{_appSettings.ApiBaseUrl}/{ip}?access_key={_appSettings.ApiAccessKey}";
            _cache.TryGetValue (ip, out result);
            if (result == null) {
                IPDetails item = _dataProvider.GetIPDetails (ip);
                result = _mapper.Map<Models.IPDetails> (item);
                if (result != null) {
                    _cache.Set (ip, result, TimeSpan.FromSeconds (60));
                } else {
                    var consumerFactory = new Lib.Service.IIPInfoProviderFactory ();
                    var consumer = consumerFactory.Create ();
                    Lib.Service.IIPDetails details = await consumer.GetDetails (uri);
                    if (details != null) {
                        result = _mapper.Map<Models.IPDetails> (details);
                        IPDetails newrecord = _mapper.Map<IPDetails> (details);
                        newrecord.Ip = ip;
                        _dataProvider.InsertIPDetails (newrecord);
                        _cache.Set (ip, result, TimeSpan.FromSeconds (60));
                    }
                }
            }
            return result;

        }
        private IEnumerable<IEnumerable<IPDetails>> IntoBatches<IPDetails> (IEnumerable<IPDetails> list, int size) {
            var rest = list;
            while (rest.Any ()) {
                yield return rest.Take (size);
                rest = rest.Skip (size);
            }
        }

        public Task PostData (List<IPDetails> values, Guid guid) {
            return Task.Run (() => {
                var batches = IntoBatches (values, 10);
                foreach (IEnumerable<IPDetails> batch in batches) {
                    for (int i = 0; i < values.Count; i++) {
                        values[i].guid = guid.ToString ();
                        _dataProvider.SaveIPDetails (values[i]);
                        _cache.Set (values[i].Ip, values[i], TimeSpan.FromSeconds (60));

                    }

                }

            });

        }

    }
}