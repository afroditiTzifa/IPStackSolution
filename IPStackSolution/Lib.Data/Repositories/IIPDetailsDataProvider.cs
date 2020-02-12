using Lib.Data.Entities;

namespace Lib.Data.Repositories {
    public interface IIPDetailsDataProvider {
        IPDetails GetIPDetails (string ip);

        void InsertIPDetails (IPDetails details);

        void SaveIPDetails (IPDetails details);

        void UpdateIPDetails (IPDetails details);

    }
}