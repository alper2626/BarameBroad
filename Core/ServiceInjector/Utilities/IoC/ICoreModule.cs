using Microsoft.Extensions.DependencyInjection;

namespace Core.ServiceInjector.Utilities.IoC
{
    public interface ICoreModule
    {
        void Load(IServiceCollection collection);
    }
}
