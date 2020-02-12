using System.Linq;
using Lib.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lib.Data.Repositories {
    public class IPDetailsSQLDataProvider : IIPDetailsDataProvider {
        private readonly IPStackDB _context;

        public IPDetailsSQLDataProvider (IPStackDB context) {
            _context = context;

        }

        public IPDetails GetIPDetails (string ip) {
            return _context.IPDetails.Where (x => x.Ip == ip).AsNoTracking ().FirstOrDefault ();

        }

        public void InsertIPDetails (IPDetails details) {
            _context.IPDetails.Add (details);
            _context.SaveChanges ();

        }

        public void SaveIPDetails (IPDetails details) {
            if (GetIPDetails (details.Ip) != null)
                UpdateIPDetails (details);
            else
                InsertIPDetails (details);

        }

        public void UpdateIPDetails (IPDetails details) {
            _context.IPDetails.Update (details);
            _context.SaveChanges ();

        }

    }
}