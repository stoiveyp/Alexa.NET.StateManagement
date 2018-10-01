using System.Threading.Tasks;
using Alexa.NET.Request;

namespace Alexa.NET.StateManagement
{
    public interface IPersistenceStore
    {
        Task<T> Get<T>(SkillRequest request, string key);
        Task Set<T>(SkillRequest request, string key, T value);
    }
}
