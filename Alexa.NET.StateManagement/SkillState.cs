namespace Alexa.NET.StateManagement
{
    using Alexa.NET.Request;
    using System.Collections.Generic;
    public class SkillState : ISkillState
    {
        private Dictionary<string,object> _requestAttributes;
        private Session _session;

        public SkillState() : this((Session)null) { }

        public SkillState(SkillRequest request) : this(request.Session)
        {

        }

        public SkillState(Session session)
        {
            _session = session;
            _requestAttributes = new Dictionary<string,object>();
        }
    }
}