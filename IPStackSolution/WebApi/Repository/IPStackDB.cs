using WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WebApi.Repository
{
    public class IPStackDB : DbContext
    {
        public DbSet<IPDetails> IPDetails { get; set; }
        public IPStackDB(DbContextOptions<IPStackDB> options) : base(options)
        { }
    }
}