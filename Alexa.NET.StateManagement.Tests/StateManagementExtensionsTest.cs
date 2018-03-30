using Alexa.NET.Request;
using Alexa.NET.StateManagement;
using Xunit;
public class StateManagementExtensionsTests{
    public void ForRequestCreatesNewISkillState(){
        var state = new SkillRequest().StateManagement();
        Assert.NotNull(state);
    }
}