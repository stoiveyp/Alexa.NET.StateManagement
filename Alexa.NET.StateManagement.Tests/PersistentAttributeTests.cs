using System;
using System.Threading.Tasks;
using Alexa.NET.Request;
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
        public async Task GetCallsPersistentStore()
        {
            var request = new SkillRequest();
            var persistence = Substitute.For<IPersistenceStore>();
            var state = new SkillState(request,persistence);

            await state.Get<string>(SimpleKey);

            await persistence.Received(1).Get<string>(request, SimpleKey);
        }

        [Fact]
        public void SetCallsPersistentStore()
        {
            var request = new SkillRequest();
            var persistence = Substitute.For<IPersistenceStore>();
            var state = new SkillState(request,persistence);

            state.SetPersistent(SimpleKey, SimpleValue);

            persistence.Received(1).Set(request,SimpleKey, SimpleValue);
        }

        [Fact]
        public async Task PersistentHiddenWhenRequestAvailable()
        {
            var persistence = Substitute.For<IPersistenceStore>();
            persistence.Get<string>(Arg.Any<SkillRequest>(),SimpleKey).Returns(ReplacementValue);

            var skillState = new SkillState();
            skillState.SetRequest(SimpleKey, SimpleValue);

            var result = await skillState.Get<string>(SimpleKey);
            Assert.Equal(SimpleValue, result);

        }

        [Fact]
        public async Task PersistentHiddenWhenSessionAvailable()
        {
            var persistence = Substitute.For<IPersistenceStore>();
            persistence.Get<string>(Arg.Any<SkillRequest>(),SimpleKey).Returns(ReplacementValue);

            var skillState = new SkillState();
            skillState.SetSession(SimpleKey, SimpleValue);

            var result = await skillState.Get<string>(SimpleKey);
            Assert.Equal(SimpleValue, result);
        }

        [Fact]
        public async Task PersistentCalledWhenGetCalledExplicit()
        {
            var persistence = Substitute.For<IPersistenceStore>();
            persistence.Get<string>(Arg.Any<SkillRequest>(),SimpleKey).Returns(ReplacementValue);

            var skillState = new SkillState(new SkillRequest(),persistence);
            skillState.SetSession(SimpleKey, SimpleValue);

            var result = await skillState.GetPersistent<string>(SimpleKey);
            Assert.Equal(ReplacementValue, result);
        }

        [Fact]
        public async Task PersistentThrowsExceptionWhenNoStoreSet()
        {
            var state = new SkillState();
            await Assert.ThrowsAsync<InvalidOperationException>(() => state.SetPersistent(SimpleKey, SimpleValue));
        }

        [Fact]
        public async Task PersistentThrowsExceptionOnExplicitCallWhenNoStoreSet()
        {
            var state = new SkillState();
            await Assert.ThrowsAsync<InvalidOperationException>(() => state.SetPersistent(SimpleKey, SimpleValue));
        }

        [Fact]
        public async Task PersistentReturnsNullWhenGetAgainstNoStore()
        {
            var state = new SkillState();
            var result = await state.Get<string>(SimpleKey);
            Assert.Null(result);
        }
    }
}
