namespace Alexa.NET.StateManagement{
    public interface ISkillState
    {
        object GetAttribute(string key);
        object GetAttribute(string key, AttributeLevel level);
        void SetAttribute(string key, string value);
        void SetAttribute(string key, string value, AttributeLevel level);
    }
}