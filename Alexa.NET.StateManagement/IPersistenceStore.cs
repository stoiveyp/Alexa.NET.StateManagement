using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alexa.NET.StateManagement
{
    public interface IPersistenceStore
    {
        Task<T> Get<T>(string simpleKey);
        Task Set<T>(string simpleKey, T simpleValue);
    }
}
