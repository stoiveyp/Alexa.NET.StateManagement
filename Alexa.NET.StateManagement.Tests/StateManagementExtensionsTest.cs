using Alexa.NET.Request;
using Xunit;

namespace Alexa.NET.StateManagement.Tests
{
    public class StateManagementExtensionsTests{
        [Fact]
        public void ForRequestCreatesNewISkillState(){
            var state = new SkillRequest().StateManagement();
            Assert.NotNull(state);
        }

        [Fact]
        public void SkillStateWorksFromSession()
        {
            var state = new SkillRequest().StateManagement();
            Assert.NotNull(state);
        }

        [Fact]
        public void SkillStateWorksWithNoRequestObjects()
        {
            var state = new SkillState();
            Assert.NotNull(state);
        }
    }
}