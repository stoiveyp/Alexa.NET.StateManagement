using System;
using System.Net.Sockets;

namespace Alexa.NET.StateManagement
{
    using Request;
    using System.Collections.Generic;

    public class SkillState : ISkillState
    {
        public Dictionary<string, object> RequestAttributes { get; }
        public Session Session { get; }

        public SkillState() : this((Session)null,null) { }

        public SkillState(SkillRequest request) : this(request,null)
        {

        }

        public SkillState(SkillRequest request, IPersistenceStore persistence) : this(request.Session, persistence)
        {
        }

        public SkillState(IPersistenceStore persistence) : this((Session)null, persistence)
        {

        }

        public SkillState(Session session):this(session,null) {
        }

        public SkillState(Session session, IPersistenceStore persistence)
        {
            Session = session;
            RequestAttributes = new Dictionary<string, object>();
        }

        public object GetAttribute(string key)
        {
            return GetRequest(key) ?? GetSession(key);
        }

        public object GetAttribute(string key, AttributeLevel level)
        {
            switch (level)
            {
                case AttributeLevel.Request:
                    return GetRequest(key);
                case AttributeLevel.Session:
                    return GetSession(key);
            }

            return null;
        }

        private object GetRequest(string key)
        {
            return RequestAttributes.TryGetValue(key, out object value) ? value : null;
        }

        private object GetSession(string key)
        {
            object value = null;
            return Session?.Attributes.TryGetValue(key, out value) ?? false ? value : null;
        }

        public void SetAttribute(string key, string value)
        {
            SetAttribute(key, value, AttributeLevel.Request);
        }

        public void SetAttribute(string key, string value, AttributeLevel level)
        {
            switch (level)
            {
                case AttributeLevel.Request:
                    AddRequest(key, value);
                    break;
                case AttributeLevel.Session:
                    AddSession(key, value);
                    break;
            }
        }

        private void AddSession(string key, string value)
        {
            if (Session == null)
            {
                throw new InvalidOperationException("No session set");
            }

            if (Session.Attributes == null)
            {
                Session.Attributes = new Dictionary<string, object>();
            }

            if (!Session.Attributes.TryAdd(key, value))
            {
                Session.Attributes[key] = value;
            }
        }

        private void AddRequest(string key, string value)
        {
            if (!RequestAttributes.TryAdd(key, value))
            {
                RequestAttributes[key] = value;
            }
        }
    }
}