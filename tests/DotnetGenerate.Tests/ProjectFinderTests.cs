using System;
using System.IO;
using FluentAssertions;
using Xunit;

namespace DotnetGenerate.Tests
{
    public class ProjectFinderTests
    {
        [Fact]
        public void FindThisProjectRootNamespace()
        {
            var dir = Directory.GetCurrentDirectory();
            var projectFinder = new ProjectFinder().Find(new DirectoryInfo(dir));

            projectFinder.RootNamespace.Should().Be("DotnetGenerate.Tests");
        }
    }
}
