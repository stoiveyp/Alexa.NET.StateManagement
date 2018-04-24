using System.Threading.Tasks;
using Alexa.NET.Request;

namespace Alexa.NET.StateManagement{
    public interface ISkillState
    {
        Task<T> Get<T>(string key);
        T GetRequest<T>(string key);
        T GetSession<T>(string key);
        Task<T> GetPersistent<T>(string key);

        void SetRequest<T>(string key, T value);
        void SetSession<T>(string key, T value);

        Task SetPersistent<T>(string key, T value);

        Session Session { get; }
    }
}