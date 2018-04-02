using System.Threading.Tasks;

namespace Alexa.NET.StateManagement
{
    public interface IPersistenceStore
    {
        Task<T> Get<T>(string key);
        Task Set<T>(string key, T value);
    }
}
