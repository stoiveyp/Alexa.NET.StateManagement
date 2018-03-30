using Alexa.NET.Request;
using Alexa.NET.StateManagement;
using Xunit;
public class StateManagementExtensionsTests{
    [Fact]
    public void ForRequestCreatesNewISkillState(){
        var state = new SkillRequest().StateManagement();
        Assert.NotNull(state);
    }

    [Fact]
    public void SkillStateWorksFromSession()
    {
        var state = new SkillRequest().Session.StateManagement();
        Assert.NotNull(state);
    }

    [Fact]
    public void SkillStateWorksWithNoRequestObjects()
    {
        var state = new SkillState();
        Assert.NotNull(state);
    }
}