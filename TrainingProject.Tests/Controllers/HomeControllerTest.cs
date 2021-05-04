using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TrainingProject;
using TrainingProject.Controllers;
using TrainingProject.Interfaces;
using TrainingProject.Models;

namespace TrainingProject.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexGetDrives()
        {
            Mock<IFileManager> mock = new Mock<IFileManager>();
            FileSystemItem fileSystemItem = new FileSystemItem("C:", "C:", DateTime.Now, 0, FileType.Drive, "C");
            List<FileSystemItem> fileSystemItems = new List<FileSystemItem>() { fileSystemItem };
            mock.Setup(m => m.GetDrives()).Returns(fileSystemItems);
            HomeController controller = new HomeController(mock.Object);

            ViewResult result = controller.Index(null) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(fileSystemItems, result.ViewData.Model);
        }

        [TestMethod]
        public void IndexGetFiles()
        {
            Mock<IFileManager> mock = new Mock<IFileManager>();
            FileSystemItem fileSystemItem = new FileSystemItem("C:", "C:", DateTime.Now, 0, FileType.Drive, "C");
            List<FileSystemItem> fileSystemItems = new List<FileSystemItem>() { fileSystemItem };
            mock.Setup(m => m.GetFiles("C")).Returns(fileSystemItems);
            HomeController controller = new HomeController(mock.Object);

            ViewResult result = controller.Index("C") as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            Mock<IFileManager> mock = new Mock<IFileManager>();
            HomeController controller = new HomeController(mock.Object);

            ViewResult result = controller.About() as ViewResult;

            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            Mock<IFileManager> mock = new Mock<IFileManager>();
            HomeController controller = new HomeController(mock.Object);

            ViewResult result = controller.Contact() as ViewResult;

            Assert.AreEqual("Your contact page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void CreateView()
        {
            Mock<IFileManager> mock = new Mock<IFileManager>();
            HomeController controller = new HomeController(mock.Object);

            ViewResult result = controller.Create() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateFolder()
        {
            Mock<IFileManager> mock = new Mock<IFileManager>();
            string name = "1";
            string path = "/C";
            string expectedRouteValue = "C";
            string expectedRouteName = "FileSystem";
            string expectedPath = "C:\\1";
            CreatedFile file = new CreatedFile() { Name = name, IsFolder = true, Path = path };
            mock.Setup(m => m.CreateFolder(path));
            HomeController controller = new HomeController(mock.Object);

            RedirectToRouteResult result = controller.Create(file) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedRouteValue, result.RouteValues["url"]);
            Assert.AreEqual(expectedRouteName, result.RouteName);
            mock.Verify(x => x.CreateFolder(expectedPath));
        }

        [TestMethod]
        public void CreateFile()
        {
            Mock<IFileManager> mock = new Mock<IFileManager>();
            string name = "1.txt";
            string path = "/C";
            string expectedRouteValue = "C";
            string expectedRouteName = "FileSystem";
            string expectedPath = "C:\\1.txt";
            CreatedFile file = new CreatedFile() { Name = name, IsFolder = false, Path = path };
            mock.Setup(m => m.CreateFile(path));
            HomeController controller = new HomeController(mock.Object);

            RedirectToRouteResult result = controller.Create(file) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedRouteValue, result.RouteValues["url"]);
            Assert.AreEqual(expectedRouteName, result.RouteName);
            mock.Verify(x => x.CreateFile(expectedPath));
        }

        [TestMethod]
        public void DeleteFolder()
        {
            Mock<IFileManager> mock = new Mock<IFileManager>();
            string path = "/C:\\1";
            string expectedRouteValue = "/";
            string expectedRouteName = "FileSystem";
            mock.Setup(m => m.DeleteFile(path));
            HomeController controller = new HomeController(mock.Object);

            RedirectToRouteResult result = controller.Delete(path) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedRouteValue, result.RouteValues["url"]);
            Assert.AreEqual(expectedRouteName, result.RouteName);
            mock.Verify(x => x.DeleteFile(path));
        }

        [TestMethod]
        public void DeleteFile()
        {
            Mock<IFileManager> mock = new Mock<IFileManager>();
            string path = "/C:\\1.txt";
            string expectedRouteValue = "/";
            string expectedRouteName = "FileSystem";
            mock.Setup(m => m.DeleteFile(path));
            HomeController controller = new HomeController(mock.Object);

            RedirectToRouteResult result = controller.Delete(path) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedRouteValue, result.RouteValues["url"]);
            Assert.AreEqual(expectedRouteName, result.RouteName);
            mock.Verify(x => x.DeleteFile(path));
        }
    }
}
