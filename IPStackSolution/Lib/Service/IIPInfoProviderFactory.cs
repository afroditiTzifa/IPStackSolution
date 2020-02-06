using Lib.Implementaion;

namespace Lib.Service
{
    public class IIPInfoProviderFactory
    {
        public IIPInfoProvider Create()
        {
            return new IPInfoProvider();

        }
    }
}