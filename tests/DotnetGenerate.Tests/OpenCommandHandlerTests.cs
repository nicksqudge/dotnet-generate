using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace DotnetGenerate.Tests
{
    public class OpenCommandHandlerTests
    {
        [Fact]
        public void HandleNoValue()
        {
            var handler = new OpenCommandHandler("")
                .SetPath(new PathBuilderResult()
                {
                    Directory = "/Project/"
                });

            handler.HasCommand.Should().BeFalse();
            handler.GetCommand("something.cs")
                .Should().Be("");
        }

        [Fact]
        public void HandleValueNoPath()
        {
            var handler = new OpenCommandHandler("code")
                .SetPath(new PathBuilderResult()
                {
                    Directory = "/Project/"
                });

            handler.HasCommand.Should().BeTrue();
            handler.GetCommand("FilePath.cs")
                .Should()
                .Be("code \"/Project/FilePath.cs\"");
        }

        [Fact]
        public void HandleValueWithPath()
        {
            var handler = new OpenCommandHandler("open-this {path} something")
                .SetPath(new PathBuilderResult()
                {
                    Directory = "/Project/"
                });

            handler.HasCommand.Should().BeTrue();
            handler.GetCommand("FilePath.cs")
                .Should()
                .Be("open-this \"/Project/FilePath.cs\" something");
        }
    }  
}