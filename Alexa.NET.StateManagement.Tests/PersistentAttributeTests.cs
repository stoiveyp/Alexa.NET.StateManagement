using System;
using NSubstitute;
using Xunit;

namespace Alexa.NET.StateManagement.Tests
{
    public class PersistentAttributeTests
    {
        private const string SimpleKey = "testKey";
        private const string SimpleValue = "testValue";
        private const string ReplacementValue = "replacement";

        [Fact]
        public void GetCallsPersistentStore()
        {
            var persistence = Substitute.For<IPersistenceStore>();
            var state = new SkillState(persistence);

            state.GetAttribute(SimpleKey);

            persistence.Received(1).Get(SimpleKey);
        }

        [Fact]
        public void SetCallsPersistentStore()
        {
            var persistence = Substitute.For<IPersistenceStore>();
            var state = new SkillState(persistence);

            state.SetAttribute(SimpleKey, SimpleValue, AttributeLevel.Persistent);

            persistence.Received(1).Set(SimpleKey, SimpleValue);
        }

        [Fact]
        public void PersistentHiddenWhenRequestAvailable()
        {
            var persistence = Substitute.For<IPersistenceStore>();
            persistence.Get(SimpleKey).Returns(ReplacementValue);

            var skillState = new SkillState();
            skillState.SetAttribute(SimpleKey, SimpleValue);

            var result = skillState.GetAttribute(SimpleKey);
            Assert.Equal(SimpleValue, result);

        }

        [Fact]
        public void PersistentHiddenWhenSessionAvailable()
        {
            var persistence = Substitute.For<IPersistenceStore>();
            persistence.Get(SimpleKey).Returns(ReplacementValue);

            var skillState = new SkillState();
            skillState.SetAttribute(SimpleKey, SimpleValue, AttributeLevel.Session);

            var result = skillState.GetAttribute(SimpleKey);
            Assert.Equal(SimpleValue, result);
        }

        [Fact]
        public void PersistentCalledWhenGetCalledExplicit()
        {
            var persistence = Substitute.For<IPersistenceStore>();
            persistence.Get(SimpleKey).Returns(ReplacementValue);

            var skillState = new SkillState(persistence);
            skillState.SetAttribute(SimpleKey, SimpleValue, AttributeLevel.Session);

            var result = skillState.GetAttribute(SimpleKey, AttributeLevel.Persistent);
            Assert.Equal(ReplacementValue, result);
        }

        [Fact]
        public void PersistentThrowsExceptionWhenNoStoreSet()
        {
            var state = new SkillState();
            Assert.Throws<InvalidOperationException>(() => state.SetAttribute(SimpleKey, SimpleValue, AttributeLevel.Persistent));
        }

        [Fact]
        public void PersistentThrowsExceptionOnExplicitCallWhenNoStoreSet()
        {
            var state = new SkillState();
            Assert.Throws<InvalidOperationException>(() => state.SetAttribute(SimpleKey, SimpleValue, AttributeLevel.Persistent));
        }

        [Fact]
        public void PersistentReturnsNullWhenGetAgainstNoStore()
        {
            var state = new SkillState();
            var result = state.GetAttribute(SimpleKey);
            Assert.Null(result);
        }
    }
}
