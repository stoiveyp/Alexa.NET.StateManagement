namespace Alexa.NET.StateManagement
{
    using Alexa.NET.Request;
    using Alexa.NET.StateManagement;
    public static class StateManagementExtensions
    {
        public static ISkillState StateManagement(this SkillRequest request)
        {
            return new SkillState(request);
        }
    }
}