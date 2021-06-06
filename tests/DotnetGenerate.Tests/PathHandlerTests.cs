using System.Runtime.Intrinsics.X86;
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
            var result = new PathBuilder()
                .SetProjectPath("/Test/TestProject/TestProject.csproj")
                .Build("ExampleClass");

            result.RelativePath.Should().Be("./ExampleClass.cs");
            result.FullPath.Should().Be(@"/Test/TestProject/ExampleClass.cs");
            result.Namespace.Should().Be("TestProject");
        }

        [Fact]
        public void GetPathForInputWithinChildFolderOfRoot()
        {
            var result = new PathBuilder()
                .SetProjectPath("/Test/TestProject/TestProject.csproj")
                .Build("something/ExampleClass");

            result.RelativePath.Should().Be("./something/ExampleClass.cs");
            result.FullPath.Should().Be("/Test/TestProject/something/ExampleClass.cs");
            result.Namespace.Should().Be("TestProject.something");
        }

        [Fact]
        public void CreateFileFromCWDChildOfProject()
        {
            var result = new PathBuilder()
                .SetProjectPath("/Test/TestProject/TestProject.csproj")
                .SetCurrentWorkingDir("/Test/TestProject/Child")
                .Build("AnotherExample");

            result.RelativePath.Should().Be("./Child/AnotherExample.cs");
            result.FullPath.Should().Be("/Test/TestProject/Child/AnotherExample.cs");
            result.Namespace.Should().Be("TestProject.Child");
        }

        [Fact]
        public void CreateFileWithDoubleFolders()
        {
            var result = new PathBuilder()
                .SetProjectPath("/Test/TestProject/TestProject.csproj")
                .Build("Child1/Child2/Batman.cs");

            result.RelativePath.Should().Be("./Child1/Child2/Batman.cs");
            result.FullPath.Should().Be("/Test/TestProject/Child1/Child2/Batman.cs");
            result.Namespace.Should().Be("TestProject.Child1.Child2");
        }

        [Fact]
        public void ProvideNamespaceForPath()
        {
            var result = new PathBuilder()
                .SetProjectPath("/Test/TestProject/TestProject.csproj")
                .SetNamespace("DotnetGen")
                .Build("Example");

            result.RelativePath.Should().Be("./Example.cs");
            result.FullPath.Should().Be("/Test/TestProject/Example.cs");
            result.Namespace.Should().Be("DotnetGen");
        }

        [Fact]
        public void ProvideNamespaceForChildFolders()
        {
            var result = new PathBuilder()
                .SetProjectPath("/Test/TestProject/TestProject.csproj")
                .SetNamespace("DotnetGen")
                .Build("Shotgun/Something");  

            result.RelativePath.Should().Be("./Shotgun/Something.cs");
            result.FullPath.Should().Be("/Test/TestProject/Shotgun/Something.cs");
            result.Namespace.Should().Be("DotnetGen.Shotgun");
        }

        [Fact]
        public void AddInRelativePath()
        {
            var result = new PathBuilder()
                .SetProjectPath("/Test/TestProject/TestProject.csproj")
                .SetCurrentWorkingDir("/Test/TestProject/Something")
                .Build("../Team/Example");

            result.RelativePath.Should().Be("./Team/Example.cs");
            result.FullPath.Should().Be("/Test/TestProject/Team/Example.cs");
            result.Namespace.Should().Be("TestProject.Team");
        }

        [Fact]
        public void AddInExtraRelativePath()
        {
            var result = new PathBuilder()
                .SetProjectPath("/Test/TestProject/TestProject.csproj")
                .SetCurrentWorkingDir("/Test/TestProject/Dawn/Of/The/Dead")
                .Build("../../Team/Example");

            result.RelativePath.Should().Be("./Dawn/Of/Team/Example.cs");
            result.FullPath.Should().Be("/Test/TestProject/Dawn/Of/Team/Example.cs");
            result.Namespace.Should().Be("TestProject.Dawn.Of.Team");
        }

        [Fact]
        public void UseRelativePathWhenNotInProjectRoot()
        {
            var result = new PathBuilder()
                .SetProjectPath("/Test/TestProject/TestProject.csproj")
                .SetCurrentWorkingDir("/Test/TestProject/Love/Is/Love")
                .Build("./RootTest");

            result.RelativePath.Should().Be("./RootTest.cs");
            result.FullPath.Should().Be("/Test/TestProject/RootTest.cs");
            result.Namespace.Should().Be("TestProject");
        }

        [Fact]
        public void FileNameTransformer()
        {
            var result = new PathBuilder()
                .SetProjectPath("/Test/TestProject/TestProject.csproj")
                .SetFileNameTransform((string input) => {
                    return "NotTheOriginalFile.cs";
                })
                .Build("ExampleClass");

            result.RelativePath.Should().Be("./NotTheOriginalFile.cs");
            result.FullPath.Should().Be(@"/Test/TestProject/NotTheOriginalFile.cs");
            result.Namespace.Should().Be("TestProject");
        }

        [Fact]
        public void NamespaceNotCorrectBug()
        {
            var result = new PathBuilder()
                .SetProjectPath(@"E:\Repos\Worklog\Dependencies\Builders\Builders.csproj")
                .SetNamespace("OpenApiBuilder")
                .SetCurrentWorkingDir(@"E:\Repos\Worklog\Dependencies\Builders")
                .Build("ExampleClass");
                
            result.Namespace.Should().Be("OpenApiBuilder");
            result.RelativePath.Should().Be("./ExampleClass.cs");
        }
    }
}
