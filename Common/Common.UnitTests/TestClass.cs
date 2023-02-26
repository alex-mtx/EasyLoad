using AutoFixture.AutoMoq;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.UnitTests
{
    [TestClass]
    public abstract class TestClass
    {
        protected IFixture _fixture;
        protected CancellationToken _ct = new CancellationToken();

        public TestClass()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

    }
}