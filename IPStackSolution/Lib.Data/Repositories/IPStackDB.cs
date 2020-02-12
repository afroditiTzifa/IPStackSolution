using Lib.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lib.Data.Repositories {
    public class IPStackDB : DbContext {
        public DbSet<IPDetails> IPDetails { get; set; }
        public IPStackDB (DbContextOptions<IPStackDB> options) : base (options) { }
    }
}