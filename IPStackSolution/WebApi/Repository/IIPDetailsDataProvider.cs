using WebApi.Models;
using System.Threading.Tasks;

namespace WebApi.Repository
{
    public interface IIPDetailsDataProvider
    {
        IPDetails GetIPDetails(string ip);
       
        void InsertIPDetails(IPDetails details);
        
        void SaveIPDetails(IPDetails details);

        void UpdateIPDetails(IPDetails details);
   
      
    }
}