using System;
using NSubstitute;
using Xunit;

namespace Alexa.NET.StateManagement.Tests
{
    public class PersistentAttributeTests
    {
        private const string SimpleKey = "testKey";

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
            throw new NotImplementedException();
        }

        [Fact]
        public void PersistentCalledWhenNoRequestOrSession()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void PersistentHiddenWhenRequestAvailable()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void PersistentHiddenWhenSessionAvailable()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void PersistentCalledWhenExplicit()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void PersistentThrowsExceptionWhenNoStoreSet()
        {

        }

        [Fact]
        public void PersistentReturnsNullWhenGetAgainstNoStore()
        {

        }
    }
}
