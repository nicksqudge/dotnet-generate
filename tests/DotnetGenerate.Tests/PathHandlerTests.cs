using System;
using FluentAssertions;
using Xunit;

namespace DotnetGenerate.Tests
{
    public class PathHandlerTests
    {
        [Fact]
        public void GetPathForFileInProjectRoot()
        {
            var result = new PathHandler()
                .SetProjectPath("/Test/TestProject/TestProject.csproj")
                .Run("ExampleClass");

            result.RelativePath.Should().Be("./ExampleClass.cs");
            result.FullPath.Should().Be(@"/Test/TestProject/ExampleClass.cs");
            result.Namespace.Should().Be("TestProject");
        }

        [Fact]
        public void GetPathForInputWithinChildFolderOfRoot()
        {
            var result = new PathHandler()
                .SetProjectPath("/Test/TestProject/TestProject.csproj")
                .Run("something/ExampleClass");

            result.RelativePath.Should().Be("./something/ExampleClass.cs");
            result.FullPath.Should().Be("/Test/TestProject/something/ExampleClass.cs");
            result.Namespace.Should().Be("TestProject.something");
        }

        [Fact]
        public void CreateFileFromCWDChildOfProject()
        {
            var result = new PathHandler()
                .SetProjectPath("/Test/TestProject/TestProject.csproj")
                .SetCurrentWorkingDir("/Test/TestProject/Child")
                .Run("AnotherExample");

            result.RelativePath.Should().Be("./Child/AnotherExample.cs");
            result.FullPath.Should().Be("/Test/TestProject/Child/AnotherExample.cs");
            result.NameSpace.Should().Be("TestProject.Child");
        }
        
        // Create a file in a root of cwd which is not the root of the project

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
