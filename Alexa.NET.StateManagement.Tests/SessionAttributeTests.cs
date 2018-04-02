using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
        public void SetSessionAttributeAgainstSessionObject()
        {
            var state = new SkillState(new Session());

            state.SetSession(simpleKey, simpleValue);
            var result = state.Session.Attributes[simpleKey];

            Assert.Equal(simpleValue, result);
        }

        [Fact]
        public void SetSessionAttributeAgainstEmptySesson()
        {
            var state = new SkillState();

            Assert.Null(state.Session);
            state.SetSession(simpleKey, simpleValue);
            Assert.NotNull(state.Session);
        }

        [Fact]
        public void GetAttributeRetrievesSessionWhenRequestIsEmpty()
        {
            var state = new SkillState(new Session { Attributes = new Dictionary<string, object>() });
            state.Session.Attributes.Add(simpleKey, simpleValue);

            var value = state.GetSession<string>(simpleKey);

            Assert.Equal(simpleValue, value);
        }

        [Fact]
        public async Task GetAttributeRetrievesRequestWhenAvailable()
        {
            var state = new SkillState(new Session { Attributes = new Dictionary<string, object>() });
            state.SetRequest(simpleKey, simpleValue);
            state.SetSession(simpleKey, replacementValue);

            var value = await state.Get<string>(simpleKey);

            Assert.Equal(simpleValue, value);
        }
    }
}