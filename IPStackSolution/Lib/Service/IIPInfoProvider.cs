using System.Threading.Tasks;

namespace Lib.Service {
    public interface IIPInfoProvider {
        Task<IIPDetails> GetDetails (string ip);
    }

}