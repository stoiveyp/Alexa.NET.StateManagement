using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Request;
using Xunit;

namespace Alexa.NET.StateManagement.Tests
{
    public class SessionAttributeTests
    {
        private string simpleKey = "key";
        private string simpleValue = "value";
        private string replacementValue = "replacement";

        [Fact]
        public void SetSessionAttributeAgainstRequest()
        {
            var state = new SkillState(new Session());

            state.SetAttribute(simpleKey, simpleValue, AttributeLevel.Session);
            var result = state.Session.Attributes[simpleKey];

            Assert.Equal(simpleValue,result);
        }

        [Fact]
        public void SetSessionAttributeAgainstEmptySesson()
        {
            var state = new SkillState();

            Assert.Throws<InvalidOperationException>(() => state.SetAttribute(simpleKey,simpleValue, AttributeLevel.Session));

        }
    }
}
