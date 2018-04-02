using System;
using System.Threading.Tasks;
using Alexa.NET.StateManagement;
using Xunit;

namespace Alexa.NET.StateManagement.Tests
{
    public class RequestAttributeTests
    {
        private string simpleKey = "key";
        private string simpleValue = "value";
        private string replacementValue = "replacement";

        [Fact]
        public async Task GetAttributeAssumesRequestPersistence()
        {
            var state = new SkillState();

            state.RequestAttributes.Add(simpleKey, simpleValue);
            var result = await state.Get<string>(simpleKey);

            Assert.Equal(simpleValue, result);
        }

        [Fact]
        public void SetAttributeReplacesWithNewValue()
        {
            var state = new SkillState();

            state.SetRequest(simpleKey,simpleValue);
            state.SetRequest(simpleKey,replacementValue);
            var result = state.GetRequest<string>(simpleKey);

            Assert.Equal(replacementValue,result);
        }
    }
}
