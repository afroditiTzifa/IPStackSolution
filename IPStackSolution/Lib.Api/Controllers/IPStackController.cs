using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Lib.Core.Services;
using Lib.Data.Entities;
using Lib.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Lib.Api.Controllers {
    [Route ("api/ipstack")]
    public class IPStackController : ControllerBase {

        private readonly IPStackServices _services;

        public IPStackController (IMemoryCache memoryCache, IOptions<AppSettings> settings, IIPDetailsDataProvider dataProvider, IMapper mapper) {

            _services = new IPStackServices (memoryCache, settings, dataProvider, mapper);
        }

        [HttpGet ("{ip}")]
        public async Task<IActionResult> Get (string ip) {

            Lib.Core.Models.IPDetails result = await _services.GetData (ip);
            if (result != null)
                return Ok (result);
            else
                return StatusCode (500);

        }

        [HttpPost]
        public async Task<IActionResult> Post ([FromBody] List<Lib.Data.Entities.IPDetails> values) {
            Guid guid = Guid.NewGuid ();
            if (values != null && values.Count > 0)
                _services.PostData (values, guid);
            return Ok (guid);
        }

    }

}