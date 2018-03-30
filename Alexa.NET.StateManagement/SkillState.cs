namespace Alexa.NET.StateManagement
{
    using Request;
    using System.Collections.Generic;

    public class SkillState : ISkillState
    {
        public Dictionary<string, object> RequestAttributes { get; }
        public Session Session { get; }

        public SkillState() : this((Session)null) { }

        public SkillState(SkillRequest request) : this(request.Session)
        {

        }

        public SkillState(Session session)
        {
            Session = session;
            RequestAttributes = new Dictionary<string, object>();
        }

        public void SetAttribute(string key, string value)
        {
            if (!RequestAttributes.TryAdd(key, value))
            {
                RequestAttributes[key] = value;
            }
        }

        public object GetAttribute(string testKey)
        {
            return RequestAttributes[testKey];
        }
    }
}