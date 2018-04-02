using System;
using System.Net.Sockets;

namespace Alexa.NET.StateManagement
{
    using Request;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SkillState : ISkillState
    {
        private const string NoPersistenceMessage = "No persistence store set";
        public Dictionary<string, object> RequestAttributes { get; }
        public Session Session { get; private set; }

        public IPersistenceStore Persistence { get; }

        public SkillState() : this((Session)null, null) { }

        public SkillState(SkillRequest request) : this(request, null)
        {

        }

        public SkillState(Session session) : this(session, null)
        {
        }

        public SkillState(IPersistenceStore persistence) : this((Session)null, persistence)
        {

        }
        public SkillState(SkillRequest request, IPersistenceStore persistence) : this(request.Session, persistence)
        {
        }

        public SkillState(Session session, IPersistenceStore persistence)
        {
            Session = session;
            Persistence = persistence;
            RequestAttributes = new Dictionary<string, object>();
        }

        public async Task<T> Get<T>(string key)
        {
            if (TryGetRequest<T>(key, out T localRequest))
            {
                return localRequest;
            }

            if (TryGetSession<T>(key, out T localSession))
            {
                return localSession;
            }

            return await GetPersistentOrDefault<T>(key);
        }

        public Task<T> GetPersistent<T>(string key)
        {
            if (Persistence == null)
            {
                throw new InvalidOperationException(NoPersistenceMessage);
            }
            return GetPersistentOrDefault<T>(key);
        }

        private Task<T> GetPersistentOrDefault<T>(string key)
        {
            return Persistence?.Get<T>(key) ?? Task.FromResult(default(T));
        }

        public T GetRequest<T>(string key)
        {
            if (TryGetRequest<T>(key, out T localRequest))
            {
                return localRequest;
            }
            return default(T);
        }
        private bool TryGetRequest<T>(string key, out T value)
        {
            if (RequestAttributes.TryGetValue(key, out object tempValue))
            {
                value = (T)tempValue;
                return true;
            }

            value = default(T);
            return false;
        }


        public T GetSession<T>(string key)
        {
            if (TryGetSession<T>(key, out T localSession))
            {
                return localSession;
            }
            return default(T);
        }

        private bool TryGetSession<T>(string key, out T value)
        {
            object tempValue = null;
            if (Session?.Attributes?.TryGetValue(key, out tempValue) ?? false)
            {
                value = (T)tempValue;
                return true;
            }

            value = default(T);
            return false;
        }

        public Task SetPersistent<T>(string key, T value)
        {
            if (Persistence == null)
            {
                throw new InvalidOperationException(NoPersistenceMessage);
            }
            return Persistence.Set(key, value);
        }

        public void SetSession<T>(string key, T value)
        {
            if (Session == null)
            {
                Session = new Session();
            }

            if (Session.Attributes == null)
            {
                Session.Attributes = new Dictionary<string, object>();
            }

            if (!Session.Attributes.ContainsKey(key))
            {
                Session.Attributes[key] = value;
            }
            else
            {
                Session.Attributes.Add(key,value);
            }
        }

        public void SetRequest<T>(string key, T value)
        {
            if (RequestAttributes.ContainsKey(key))
            {
                RequestAttributes[key] = value;
            }
            else
            {
                RequestAttributes.Add(key,value);
            }
        }

        public void Remove(string key)
        {
            SetRequest(key, (object)null);
            SetSession(key,(object)null);
            SetPersistent(key, (object) null);
        }
    }
}