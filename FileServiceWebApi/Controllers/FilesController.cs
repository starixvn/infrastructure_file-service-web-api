using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using FileServiceWebApi.Models;
using FileServiceWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileServiceWebApi.Controllers
{
    public class FilesController : Controller
    {
        private readonly FileService fileService;

        public FilesController(FileService fileService)
        {
            this.fileService = fileService;
        }

        [HttpPost]
        public ReadResult Read([FromBody]PathFile path)
        {
            return fileService.Read(path);
        }

        [HttpPost]
        public Result Write([FromBody]SavedFile file)
        {
            return fileService.Write(file);
        }

        public ActionResult DownloadXlsx(string site, string name)
        {
            var path = new PathFile { Name = name, Site = site };
            var content = fileService.Read(path).Content;
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", name);
        }

        public ActionResult DownloadXlsxSaveAs(string site, string name, string saveAs)
        {
            var path = new PathFile { Name = name, Site = site };
            var content = fileService.Read(path).Content;
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", saveAs);
        }
    }
}