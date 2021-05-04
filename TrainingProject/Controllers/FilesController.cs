using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrainingProject.Interfaces;
using TrainingProject.Models;

namespace TrainingProject.Controllers
{
    [Route("Files")]
    public class FilesController : Controller
    {
        private readonly IFileManager fileManager;

        public FilesController(IFileManager fileManager)
        {
            this.fileManager = fileManager;
        }

        [HttpGet]
        public JsonResult Index(string path)
        {
            if (path == null)
            {
                return Json(this.fileManager.GetDrives(), JsonRequestBehavior.AllowGet);
            }

            return Json(this.fileManager.GetFiles(path), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(CreatedFile file)
        {
            if (file.IsFolder)
            {
                try
                {
                    this.fileManager.CreateFolder(file.Path);
                }
                catch (UnauthorizedAccessException)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                try
                {
                    this.fileManager.CreateFile(file.Path);
                }
                catch (UnauthorizedAccessException)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }

            return Json(file);
        }

        [HttpDelete]
        public ActionResult Delete(string path)
        {   
            if (path == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                this.fileManager.DeleteFile(path);
            }
            catch (UnauthorizedAccessException)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return Json(path);
        }
    }
}