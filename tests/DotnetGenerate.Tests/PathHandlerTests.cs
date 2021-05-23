using System;
using FluentAssertions;
using Xunit;

namespace DotnetGenerate.Tests
{
    public class PathHandlerTests
    {
        [Fact]
        public void ProjectLivesInWorkingDirectoryAddSingleFile()
        {
            var path = new PathGenerator()
                .SetCurrentWorkingDirectory("C:/Repos/TestProject/")
                .SetProjectPath("C:/Repos/TestProject/TestProject.csproj")
                .SetNamespace("TestProject")
                .SetUserInput("SomeClass")
                .Generate();

            path.Namespace.Should().Be("TestProject");
            path.FullFilePath.Should().Be("C:/Repos/TestProject/SomeClass.cs");
            path.FileName.Should().Be("SomeClass.cs");
            path.DisplayPath.Should().Be("./SomeClass.cs");
        }

        // Child folder with namespace defined

        // Double child folder with namespace defined

        // Child folder with no namespace defined

        // Double child folder with no namespace defined

        // Use ../

        // Use ./ when in a child directory

        // No project path could be found

        // Test for interfaces
    }
}
