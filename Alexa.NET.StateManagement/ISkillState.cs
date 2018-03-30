namespace Alexa.NET.StateManagement{
    public interface ISkillState
    {
        object GetAttribute(string testKey);
        void SetAttribute(string testKey, string testValue);
        void SetAttribute(string testKey, string testValue, AttributeLevel level);
    }
}