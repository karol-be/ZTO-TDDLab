using FluentAssertions;
using Xunit;

namespace Tests.BasicUtils
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            true.Should().BeTrue();
        }
    }
}