﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            var state = new SkillState(new SkillRequest());

            state.SetSession(simpleKey, simpleValue);
            var result = state.Session.Attributes[simpleKey];

            Assert.Equal(simpleValue, result);
        }

        [Fact]
        public void SessionDeserializeComplexObject()
        {
            var session = new Session();
            
            var data = new Intent {Slots = new Dictionary<string, Slot> {{"test", new Slot {Name = "test"}}}};
            session.Attributes = new Dictionary<string, object> {{"data", data.Slots}};

            var serial = JsonConvert.SerializeObject(session);
            var returnresult = JsonConvert.DeserializeObject<Session>(serial);
            var state = new SkillState(new SkillRequest{Session=returnresult});
            var result = state.GetSession<Dictionary<string,Slot>>("data");
            
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("test",result["test"].Name);
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
        public void CheckIntSessionValues()
        {
            var sessionOne = new Session();
            var requestOne = new SkillRequest { Session = sessionOne };
            var state = new SkillState(requestOne);

            state.SetSession("apple", 2);

            var serial = JsonConvert.SerializeObject(state.Session);
            var sessionTwo = JsonConvert.DeserializeObject<Session>(serial);
            var stateTwo = new SkillState(new SkillRequest { Session = sessionTwo });
            var result = stateTwo.GetSession<int>("apple");

            Assert.Equal(2, result);
        }

        [Fact]
        public async Task CheckAttributeInJson()
        {
            var requestOne = JsonConvert.DeserializeObject<SkillRequest>(System.IO.File.ReadAllText("example.json"));
            var stateTwo = new SkillState(requestOne);
            var result = await stateTwo.Get<int>("apple");

            Assert.Equal(2, result);
        }

        [Fact]
        public async Task CheckArrayInJson()
        {
            var requestOne = JsonConvert.DeserializeObject<SkillRequest>(System.IO.File.ReadAllText("example.json"));
            var stateTwo = new SkillState(requestOne);
            var result = await stateTwo.Get<JObject[]>("products");

            Assert.Single(result);
        }

        [Fact]
        public void GetAttributeRetrievesSessionWhenRequestIsEmpty()
        {
            var state = new SkillState(new SkillRequest{Session=new Session { Attributes = new Dictionary<string, object>() }});
            state.Session.Attributes.Add(simpleKey, simpleValue);

            var value = state.GetSession<string>(simpleKey);

            Assert.Equal(simpleValue, value);
        }

        [Fact]
        public async Task GetAttributeRetrievesRequestWhenAvailable()
        {
            var state = new SkillState(new SkillRequest{Session=new Session { Attributes = new Dictionary<string, object>() }});
            state.SetRequest(simpleKey, simpleValue);
            state.SetSession(simpleKey, replacementValue);

            var value = await state.Get<string>(simpleKey);

            Assert.Equal(simpleValue, value);
        }
    }
}