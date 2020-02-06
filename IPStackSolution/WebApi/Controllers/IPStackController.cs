using System;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WebApi.Helpers;
using System.Threading;

namespace WebApi.Controllers
{
    [Route("api/ipstack")]
    public class IPStackController: ControllerBase
    {      
       private readonly IMemoryCache _cache;
       private readonly IMapper _mapper;
       private readonly AppSettings _appSettings ;
       private IIPDetailsDataProvider _dataProvider ;
       
       public IPStackController(IMemoryCache memoryCache, IOptions<AppSettings> settings, IIPDetailsDataProvider dataProvider)
       {
           _cache = memoryCache;
           var config = new MapperConfiguration(cfg => 
           {
                cfg.CreateMap<Lib.Service.IIPDetails, IPDetailsVM>();
                cfg.CreateMap<IPDetails, IPDetailsVM>();
                cfg.CreateMap<Lib.Service.IIPDetails, IPDetails>();
               
           });
           _mapper = config.CreateMapper();
           _appSettings =settings.Value;
           _dataProvider= dataProvider;
       }


        [HttpGet("{ip}")]
        public async Task<IActionResult> Get(string ip)
        {
            string uri = $"{_appSettings.ApiBaseUrl}/{ip}?access_key={_appSettings.ApiAccessKey}";
            IPDetailsVM result = null;
            _cache.TryGetValue(ip, out result);
            if (result == null )
            {
                IPDetails item = _dataProvider.GetIPDetails(ip);
                result  = _mapper.Map<IPDetailsVM>(item);
                if (result != null)
                {

                    _cache.Set(ip, result, TimeSpan.FromSeconds(60));
                    return Ok(result);
                }
                else
                {
                    var consumerFactory = new Lib.Service.IIPInfoProviderFactory();
                    var consumer = consumerFactory.Create();
                    Lib.Service.IIPDetails details = await consumer.GetDetails(uri);
                    if (details != null) 
                    {
                        result  = _mapper.Map<IPDetailsVM>(details);
                        IPDetails newrecord = _mapper.Map<IPDetails>(details);
                        newrecord.Ip = ip;
                        _dataProvider.InsertIPDetails(newrecord);
                        _cache.Set(ip, result, TimeSpan.FromSeconds(60));
                        return Ok(result);
                   }
                   else return StatusCode(500);
                }
            } 
            else return Ok(result);   
           
        }

  
        [HttpPost]
        public IActionResult Post([FromBody] List<IPDetails> values)
        {
            Guid guid = Guid.NewGuid();
            if (values != null && values.Count > 0 )
            {
               Task.Run(()=> DoWork(values, guid));
            
            }            
            return Ok(guid); 
        } 
        
        private void DoWork(List<IPDetails> values, Guid guid)
        {
              var batches = values.IntoBatches(10);
                foreach (IEnumerable<IPDetails> batch in batches)
                    UpdateData(batch.ToList(), guid);
        }

        private void  UpdateData(List<IPDetails> values, Guid guid)
        {
            for (int i=0; i < values.Count; i++)
            {
                values[i].guid= guid.ToString();
                _dataProvider.SaveIPDetails(values[i]);
                _cache.Set(values[i].Ip, values[i], TimeSpan.FromSeconds(60));

            }
        }


    }
  
}