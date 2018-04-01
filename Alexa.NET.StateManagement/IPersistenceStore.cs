using System;
using System.Collections.Generic;
using System.Text;

namespace Alexa.NET.StateManagement
{
    public interface IPersistenceStore
    {
        object Get(string simpleKey);
        void Set(string simpleKey, object simpleValue);
    }
}
