using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
using TrainingProject.Controllers;
using TrainingProject.Interfaces;
using TrainingProject.Models;

namespace TrainingProject.Tests.Controllers
{
    [TestClass]
    public class FilesControllerTests
    {
        [TestMethod]
        public void Index()
        {
            Mock<IFileManager> mock = new Mock<IFileManager>();
            FilesController controller = new FilesController(mock.Object);

            JsonResult result = controller.Index(null);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateFile()
        {
            Mock<IFileManager> mock = new Mock<IFileManager>();
            string name = "1.txt";
            string path = "/D:\\1.txt";
            CreatedFile file = new CreatedFile() { Name = name, IsFolder = false, Path = path };
            mock.Setup(m => m.CreateFile(path));
            FilesController controller = new FilesController(mock.Object);

            JsonResult result = controller.Create(file) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(JsonConvert.SerializeObject(file), JsonConvert.SerializeObject(result.Data));
            mock.Verify(x => x.CreateFile(path));
        }

        [TestMethod]
        public void CreateFileException()
        {
            Mock<IFileManager> mock = new Mock<IFileManager>();
            string name = "1.txt";
            string path = "/D:\\1.txt";
            int expectedStatusCode = 403;
            CreatedFile file = new CreatedFile() { Name = name, IsFolder = false, Path = path };
            mock.Setup(m => m.CreateFile(path)).Throws(new UnauthorizedAccessException());
            FilesController controller = new FilesController(mock.Object);

            HttpStatusCodeResult result = controller.Create(file) as HttpStatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedStatusCode, result.StatusCode);
        }

        [TestMethod]
        public void CreateFolder()
        {
            Mock<IFileManager> mock = new Mock<IFileManager>();
            string name = "1";
            string path = "/D:\\1";
            CreatedFile file = new CreatedFile() { Name = name, IsFolder = true, Path = path };
            mock.Setup(m => m.CreateFolder(path));
            FilesController controller = new FilesController(mock.Object);

            JsonResult result = controller.Create(file) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(JsonConvert.SerializeObject(file), JsonConvert.SerializeObject(result.Data));
            mock.Verify(x => x.CreateFolder(path));
        }

        [TestMethod]
        public void CreateFolderException()
        {
            Mock<IFileManager> mock = new Mock<IFileManager>();
            string name = "1";
            string path = "/D:\\1";
            int expectedStatusCode = 403;
            CreatedFile file = new CreatedFile() { Name = name, IsFolder = true, Path = path };
            mock.Setup(m => m.CreateFolder(path)).Throws(new UnauthorizedAccessException());
            FilesController controller = new FilesController(mock.Object);

            HttpStatusCodeResult result = controller.Create(file) as HttpStatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedStatusCode, result.StatusCode);
        }

        [TestMethod]
        public void DeleteFile()
        {
            Mock<IFileManager> mock = new Mock<IFileManager>();
            string path = "/D:\\1.txt";
            mock.Setup(m => m.DeleteFile(path));
            FilesController controller = new FilesController(mock.Object);

            JsonResult result = controller.Delete(path) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(JsonConvert.SerializeObject(path), JsonConvert.SerializeObject(result.Data));
            mock.Verify(x => x.DeleteFile(path));
        }

        [TestMethod]
        public void DeleteFileException()
        {
            Mock<IFileManager> mock = new Mock<IFileManager>();
            string path = "/D:\\1.txt";
            int expectedStatusCode = 403;
            mock.Setup(m => m.DeleteFile(path)).Throws(new UnauthorizedAccessException());
            FilesController controller = new FilesController(mock.Object);

            HttpStatusCodeResult result = controller.Delete(path) as HttpStatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedStatusCode, result.StatusCode);
        }

        [TestMethod]
        public void DeleteFolder()
        {
            Mock<IFileManager> mock = new Mock<IFileManager>();
            string path = "/D:\\1";
            mock.Setup(m => m.DeleteFile(path));
            FilesController controller = new FilesController(mock.Object);

            JsonResult result = controller.Delete(path) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(JsonConvert.SerializeObject(path), JsonConvert.SerializeObject(result.Data));
            mock.Verify(x => x.DeleteFile(path));
        }

        [TestMethod]
        public void DeleteFolderException()
        {
            Mock<IFileManager> mock = new Mock<IFileManager>();
            string path = "/D:\\1";
            int expectedStatusCode = 403;
            mock.Setup(m => m.DeleteFile(path)).Throws(new UnauthorizedAccessException());
            FilesController controller = new FilesController(mock.Object);

            HttpStatusCodeResult result = controller.Delete(path) as HttpStatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedStatusCode, result.StatusCode);
        }
    }
}
