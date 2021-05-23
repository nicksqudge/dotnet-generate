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
            result.Namespace.Should().Be("TestProject.Child");
        }

        [Fact]
        public void CreateFileWithDoubleFolders()
        {
            var result = new PathHandler()
                .SetProjectPath("/Test/TestProject/TestProject.csproj")
                .Run("Child1/Child2/Batman.cs");

            result.RelativePath.Should().Be("./Child1/Child2/Batman.cs");
            result.FullPath.Should().Be("/Test/TestProject/Child1/Child2/Batman.cs");
            result.Namespace.Should().Be("TestProject.Child1.Child2");
        }

        [Fact]
        public void ProvideNamespaceForPath()
        {
            var result = new PathHandler()
                .SetProjectPath("/Test/TestProject/TestProject.csproj")
                .SetNamespace("DotnetGen")
                .Run("Example");

            result.RelativePath.Should().Be("./Example.cs");
            result.FullPath.Should().Be("/Test/TestProject/Example.cs");
            result.Namespace.Should().Be("DotnetGen");
        }

        [Fact]
        public void ProvideNamespaceForChildFolders()
        {
            var result = new PathHandler()
                .SetProjectPath("/Test/TestProject/TestProject.csproj")
                .SetNamespace("DotnetGen")
                .Run("Shotgun/Something");  

            result.RelativePath.Should().Be("./Shotgun/Something.cs");
            result.FullPath.Should().Be("/Test/TestProject/Shotgun/Something.cs");
            result.Namespace.Should().Be("DotnetGen.Shotgun");
        }

        [Fact]
        public void AddInRelativePath()
        {
            var result = new PathHandler()
                .SetProjectPath("/Test/TestProject/TestProject.csproj")
                .SetCurrentWorkingDir("/Test/TestProject/Something")
                .Run("../Team/Example");

            result.RelativePath.Should().Be("./Team/Example.cs");
            result.FullPath.Should().Be("/Test/TestProject/Team/Example.cs");
            result.Namespace.Should().Be("TestProject.Team");
        }

        [Fact]
        public void AddInExtraRelativePath()
        {
            var result = new PathHandler()
                .SetProjectPath("/Test/TestProject/TestProject.csproj")
                .SetCurrentWorkingDir("/Test/TestProject/Dawn/Of/The/Dead")
                .Run("../../Team/Example");

            result.RelativePath.Should().Be("./Dawn/Of/Team/Example.cs");
            result.FullPath.Should().Be("/Test/TestProject/Dawn/Of/Team/Example.cs");
            result.Namespace.Should().Be("TestProject.Dawn.Of.Team");
        }

        [Fact]
        public void UseRelativePathWhenNotInProjectRoot()
        {
            var result = new PathHandler()
                .SetProjectPath("/Test/TestProject/TestProject.csproj")
                .SetCurrentWorkingDir("/Test/TestProject/Love/Is/Love")
                .Run("./RootTest");

            result.RelativePath.Should().Be("./RootTest.cs");
            result.FullPath.Should().Be("/Test/TestProject/RootTest.cs");
            result.Namespace.Should().Be("TestProject");
        }
    }
}
