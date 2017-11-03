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

        /// <summary>
        /// Read a file.
        /// </summary>
        [HttpGet("files/Read")]
        public ReadResult Read([FromQuery]string site, [FromQuery]string name)
        {
            var path = new PathFile { Name = name, Site = site };
            return fileService.Read(path);
        }

        /// <summary>
        /// Write a file.
        /// </summary>
        [HttpPost("files/Write")]
        public Result Write([FromBody]SavedFile file)
        {
            return fileService.Write(file);
        }

        /// <summary>
        /// Download a xlsx file.
        /// </summary>
        [HttpGet("files/DownloadXlsx")]
        public ActionResult DownloadXlsx([FromQuery]string site, [FromQuery]string name)
        {
            var path = new PathFile { Name = name, Site = site };
            var content = fileService.Read(path).Content;
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", name);
        }

        /// <summary>
        /// Download a xlsx file and Save As....
        /// </summary>
        [HttpGet("files/DownloadXlsxSaveAs")]
        public ActionResult DownloadXlsxSaveAs([FromQuery]string site, [FromQuery]string name, [FromQuery]string saveAs)
        {
            var path = new PathFile { Name = name, Site = site };
            var content = fileService.Read(path).Content;
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", saveAs);
        }
    }
}