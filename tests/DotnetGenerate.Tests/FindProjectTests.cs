using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Xunit;
using Moq;
using System.Linq;

namespace DotnetGenerate.Tests
{
    public class FindProjectTests
    {
        [Fact]
        public void FindThisProjectRootNamespace()
        {
            var dir = Directory.GetCurrentDirectory();
            var projectFinder = new FindProject(
                new DirectoryHandler(), 
                new CsProjectFileReader()
            ).InDirectory(new DirectoryInfo(dir));

            projectFinder.RootNamespace.Should().Be("DotnetGenerate.Tests");
        }

        [Fact]
        public void FindUnityProject()
        {
            var files = new List<string>()
            {
                "Assembly-CSharp.csproj",
                "Unity.CollabProxy.Editor.csproj",
                "UnityEditor.TestRunner.csproj",
                "UnityEngine.UI.csproj"
            };

            var mockDirectoryHandler = new Mock<IDirectoryHandler>();
            mockDirectoryHandler
                .Setup(r => r.GetFiles(It.IsAny<DirectoryInfo>()))
                .Returns(files.Select(f => new FileInfo(f)).ToArray());

            var mockCsProjFileReader = new Mock<ICsProjectFileReader>();
            mockCsProjFileReader
                .Setup(r => r.CheckForRootNamespace(It.IsAny<FileInfo>()))
                .Returns("Somenamespace");

            var projectFinder = new FindProject(
                mockDirectoryHandler.Object,
                mockCsProjFileReader.Object
            ).InDirectory(new DirectoryInfo("something"));
            projectFinder.RootNamespace.Should().Be("Somenamespace");
        }
    }
}
