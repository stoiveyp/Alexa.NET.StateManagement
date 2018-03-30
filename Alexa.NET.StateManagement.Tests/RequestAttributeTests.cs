using System;
using Alexa.NET.StateManagement;
using Xunit;

namespace Alexa.NET.Statemanagement.Tests
{
    public class RequestAttributeTests
    {
        private string simpleKey = "key";
        private string simpleValue = "value";
        private string replacementValue = "replacement";


        [Fact]
        public void SetAttributeAssumesRequestPersistence()
        {
            var state = new SkillState();

            state.SetAttribute(simpleKey, simpleValue);

            Assert.True(state.RequestAttributes.ContainsKey(simpleKey));
            Assert.Equal(simpleValue, state.RequestAttributes[simpleKey]);
        }

        [Fact]
        public void GetAttributeAssumesRequestPersistence()
        {
            var state = new SkillState();

            state.RequestAttributes.Add(simpleKey, simpleValue);
            var result = (string)state.GetAttribute(simpleKey);

            Assert.Equal(simpleValue, result);
        }

        [Fact]
        public void SetAttributeReplacesWithNewValue()
        {
            var state = new SkillState();

            state.SetAttribute(simpleKey,simpleValue);
            state.SetAttribute(simpleKey,replacementValue);
            var result = state.GetAttribute(simpleKey);

            Assert.Equal(replacementValue,result);
        }
    }
}
