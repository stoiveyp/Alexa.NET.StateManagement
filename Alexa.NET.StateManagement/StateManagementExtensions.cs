namespace Alexa.NET.StateManagement
{
    using Alexa.NET.Request;
    public static class StateManagementExtensions
    {
        public static ISkillState StateManagement(this SkillRequest request)
        {
            return new SkillState(request);
        }

        public static ISkillState StateManagement(this Session session)
        {
            return new SkillState(session);
        }
    }
}