using BaseEntities.Abstract;
using Core.LogModule.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Core.Initializer
{
    public class DbSetup : IDbSetup
    {
        public void Setup(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>().ToTable("Logs");
        }
    }
}