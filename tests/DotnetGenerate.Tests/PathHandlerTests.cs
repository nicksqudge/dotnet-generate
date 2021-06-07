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
                .SetInput("ExampleClass")
                .Build();

            result.RelativePath.Should().Be("./");
            result.Directory.Should().Be(@"/Test/TestProject/");
            result.Namespace.Should().Be("TestProject");
            result.FileName.Should().Be("ExampleClass");
        }

        [Fact]
        public void GetPathForInputWithinChildFolderOfRoot()
        {
            var result = new PathBuilder()
                .SetProjectPath("/Test/TestProject/TestProject.csproj")
                .SetInput("something/ExampleClass")
                .Build();

            result.RelativePath.Should().Be("./something/");
            result.Directory.Should().Be("/Test/TestProject/something/");
            result.Namespace.Should().Be("TestProject.something");
            result.FileName.Should().Be("ExampleClass");
        }

        [Fact]
        public void CreateFileFromCWDChildOfProject()
        {
            var result = new PathBuilder()
                .SetProjectPath("/Test/TestProject/TestProject.csproj")
                .SetCurrentWorkingDir("/Test/TestProject/Child")
                .SetInput("AnotherExample")
                .Build();

            result.RelativePath.Should().Be("./Child/");
            result.Directory.Should().Be("/Test/TestProject/Child/");
            result.Namespace.Should().Be("TestProject.Child");
            result.FileName.Should().Be("AnotherExample");
        }

        [Fact]
        public void CreateFileWithDoubleFolders()
        {
            var result = new PathBuilder()
                .SetProjectPath("/Test/TestProject/TestProject.csproj")
                .SetInput("Child1/Child2/Batman.cs")
                .Build();

            result.RelativePath.Should().Be("./Child1/Child2/");
            result.Directory.Should().Be("/Test/TestProject/Child1/Child2/");
            result.Namespace.Should().Be("TestProject.Child1.Child2");
            result.FileName.Should().Be("Batman");
        }

        [Fact]
        public void ProvideNamespaceForPath()
        {
            var result = new PathBuilder()
                .SetProjectPath("/Test/TestProject/TestProject.csproj")
                .SetNamespace("DotnetGen")
                .SetInput("Example")
                .Build();

            result.RelativePath.Should().Be("./");
            result.Directory.Should().Be("/Test/TestProject/");
            result.Namespace.Should().Be("DotnetGen");
            result.FileName.Should().Be("Example");
        }

        [Fact]
        public void ProvideNamespaceForChildFolders()
        {
            var result = new PathBuilder()
                .SetProjectPath("/Test/TestProject/TestProject.csproj")
                .SetNamespace("DotnetGen")
                .SetInput("Shotgun/Something")
                .Build();  

            result.RelativePath.Should().Be("./Shotgun/");
            result.Directory.Should().Be("/Test/TestProject/Shotgun/");
            result.Namespace.Should().Be("DotnetGen.Shotgun");
            result.FileName.Should().Be("Something");
        }

        [Fact]
        public void AddInRelativePath()
        {
            var result = new PathBuilder()
                .SetProjectPath("/Test/TestProject/TestProject.csproj")
                .SetCurrentWorkingDir("/Test/TestProject/Something")
                .SetInput("../Team/Example")
                .Build();

            result.RelativePath.Should().Be("./Team/");
            result.Directory.Should().Be("/Test/TestProject/Team/");
            result.Namespace.Should().Be("TestProject.Team");
            result.FileName.Should().Be("Example");
        }

        [Fact]
        public void AddInExtraRelativePath()
        {
            var result = new PathBuilder()
                .SetProjectPath("/Test/TestProject/TestProject.csproj")
                .SetCurrentWorkingDir("/Test/TestProject/Dawn/Of/The/Dead")
                .SetInput("../../Team/Example")
                .Build();

            result.RelativePath.Should().Be("./Dawn/Of/Team/");
            result.Directory.Should().Be("/Test/TestProject/Dawn/Of/Team/");
            result.Namespace.Should().Be("TestProject.Dawn.Of.Team");
            result.FileName.Should().Be("Example");
        }

        [Fact]
        public void UseRelativePathWhenNotInProjectRoot()
        {
            var result = new PathBuilder()
                .SetProjectPath("/Test/TestProject/TestProject.csproj")
                .SetCurrentWorkingDir("/Test/TestProject/Love/Is/Love")
                .SetInput("./RootTest")
                .Build();

            result.RelativePath.Should().Be("./");
            result.Directory.Should().Be("/Test/TestProject/");
            result.Namespace.Should().Be("TestProject");
            result.FileName.Should().Be("RootTest");
        }

        [Fact]
        public void NamespaceNotCorrectBug()
        {
            var result = new PathBuilder()
                .SetProjectPath(@"E:\Repos\Worklog\Dependencies\Builders\Builders.csproj")
                .SetNamespace("OpenApiBuilder")
                .SetCurrentWorkingDir(@"E:\Repos\Worklog\Dependencies\Builders")
                .SetInput("ExampleClass")
                .Build();
                
            result.Namespace.Should().Be("OpenApiBuilder");
            result.RelativePath.Should().Be("./");
            result.FileName.Should().Be("ExampleClass");
        }
    }
}
