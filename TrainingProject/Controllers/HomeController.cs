using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingProject.Interfaces;
using TrainingProject.Models;
using TrainingProject.Services;

namespace TrainingProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFileManager fileManager;

        public HomeController(IFileManager fileManager)
        {
            this.fileManager = fileManager;
        }

        public ActionResult Index(string url)
        {
            if (url == null)
            {
                return View(this.fileManager.GetDrives());
            }

            string path = UrlPathHelper.UrlToPath(url);

            return View(this.fileManager.GetFiles(path));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(CreatedFile file)
        {
            string currentPathTmp = file.Path.Substring(1, file.Path.Length - 1);
            string currentUrl = currentPathTmp.Substring(currentPathTmp.IndexOf('/') + 1, currentPathTmp.Length - currentPathTmp.IndexOf('/') - 1);

            string path = UrlPathHelper.UrlToPath(currentUrl + "/" + file.Name);

            if (file.IsFolder)
            {
                try
                {
                    this.fileManager.CreateFolder(path);
                }
                catch (Exception)
                {
                    return RedirectToRoute("FileSystem", new { url = currentUrl });
                }
            }
            else
            {
                try
                {
                    this.fileManager.CreateFile(path);
                }
                catch (UnauthorizedAccessException)
                {
                    return RedirectToRoute("FileSystem", new { url = currentUrl });
                }
                catch (Exception)
                {
                    return RedirectToRoute("FileSystem", new { url = currentUrl });
                }
            }

            return RedirectToRoute("FileSystem", new { url = currentUrl });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string path)
        {
            string currentPath = path.Substring(0, path.LastIndexOf('\\'));
            string returnToUrl = UrlPathHelper.PathToUrl(currentPath);

            try
            {
                this.fileManager.DeleteFile(path);
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToRoute("FileSystem", new { url = returnToUrl });
            }
            catch (Exception)
            {
                return RedirectToRoute("FileSystem", new { url = returnToUrl });
            }

            return RedirectToRoute("FileSystem", new { url = returnToUrl });
        }
    }
}