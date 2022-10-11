using Microsoft.EntityFrameworkCore;

namespace BaseEntities.Abstract
{
    public interface IDbSetup
    {
        void Setup(ModelBuilder modelBuilder);
    }
}