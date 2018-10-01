namespace Alexa.NET.StateManagement
{
    using Alexa.NET.Request;
    public static class StateManagementExtensions
    {
        public static ISkillState StateManagement(this SkillRequest request, IPersistenceStore store = null)
        {
            return new SkillState(request, null);
        }
    }
}