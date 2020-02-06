using System.IO;
using WebApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace WebApi.Repository
{
    
    public class IPDetailsJsonDataProvider : IIPDetailsDataProvider
    {
        private readonly AppSettings _appSettings ;
        public IPDetailsJsonDataProvider(IOptions<AppSettings> settings)
        {
            _appSettings =settings.Value;
        }
        public  IPDetails GetIPDetails(string ip)
        {
            IPDetails result =null;
            var ipDetails = JsonConvert.DeserializeObject<Dictionary<string, IPDetails>> (System.IO.File.ReadAllText(_appSettings.FileDbName));     
            if (ipDetails != null && ipDetails.ContainsKey(ip))
                result = ipDetails[ip];
            return result;    
               
        }

        public void InsertIPDetails(IPDetails details)
        {  
            var jsonObj = JsonConvert.DeserializeObject<Dictionary<string, IPDetails>> (System.IO.File.ReadAllText(_appSettings.FileDbName));     
            if (jsonObj == null) 
               jsonObj=new Dictionary<string, IPDetails>();
            jsonObj.Add(details.Ip, details);  
            string newJsonResult = JsonConvert.SerializeObject(jsonObj,  Newtonsoft.Json.Formatting.Indented);              
            System.IO.File.WriteAllText(_appSettings.FileDbName, newJsonResult);

        }

        public  void SaveIPDetails(IPDetails details)
        {
            if (GetIPDetails(details.Ip) != null) 
               UpdateIPDetails(details);
            else
               InsertIPDetails(details);

        }

        
        public void UpdateIPDetails(IPDetails details)
        {
            var jsonObj = JsonConvert.DeserializeObject<Dictionary<string, IPDetails>> (System.IO.File.ReadAllText(_appSettings.FileDbName));     
            jsonObj.Remove(details.Ip);
            jsonObj.Add(details.Ip, details);
            string newJsonResult = JsonConvert.SerializeObject(jsonObj,  Newtonsoft.Json.Formatting.Indented);              
            System.IO.File.WriteAllText(_appSettings.FileDbName, newJsonResult);

        }
    }
}