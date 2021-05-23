using System;
using FluentAssertions;
using Xunit;

namespace DotnetGenerate.Tests
{
    public class PathHandlerTests
    {
        [Theory]
        [InlineData("/Test/", "TestName", "/Test/", "TestName.cs")]
        // [InlineData("/Test/", "testDir/TestName", "/Test/testDir/", "TestName.cs")]
        // [InlineData("/Test/", "../something", "/", "Something.cs")]
        // [InlineData("\\Test\\", "testDir/TestName", "\\Test\\testDir\\", "TestName.cs")]
        // [InlineData("/Test/", "testDir\\something", "/Test/testDir/", "Something.cs")]
        public void CheckGeneration(string startingDir, string userInput, string expectedDir, string expectedName)
        {
            var pathHandler = new PathHandler(startingDir, userInput);
            pathHandler.Generate();

            pathHandler.Directory.Should().Be(expectedDir);
            pathHandler.FileName.Should().Be(expectedName);
        }
    }
}
