using FileServiceWebApi.Models;
using FileServiceWebApi.Network;
using Microsoft.Extensions.Options;
using System.IO;
using System.Net;
using System;
using System.Net.Http;

namespace FileServiceWebApi.Services
{
    public interface FileService
    {
        ReadResult Read(PathFile path);

        Result Write(SavedFile file);
    }

    public class FileServiceImp : FileService
    {
        private readonly IOptions<ServerWrite> serverWrite;
        private readonly IOptions<ServerRead> serverRead;

        public FileServiceImp(
            IOptions<ServerWrite> serverWrite,
            IOptions<ServerRead> serverRead)
        {
            this.serverWrite = serverWrite;
            this.serverRead = serverRead;
        }

        public ReadResult Read(PathFile path)
        {
            try
            {                
                return ConnectNetWork(path);                
            }
            catch (Exception exception)
            {
                return new ReadResult
                {
                    Result = new Result { ErrorCode = 1, Message = exception.Message }
                };
            }
        }

        private ReadResult ConnectNetWork(PathFile path)
        {
            if(string.IsNullOrEmpty(serverRead.Value.UserName))
            {
                return ReadFile(path);
            }
            else{
                var networkCredential = new NetworkCredential(serverRead.Value.UserName, serverRead.Value.Password);
                using (new NetworkConnection(serverRead.Value.Path, networkCredential))
                {
                    return ReadFile(path);
                }
            }
        }

        private ReadResult ReadFile(PathFile path)
        {
            return new ReadResult
            {
                Content = File.ReadAllBytes($"{serverRead.Value.Path}\\{path}"),
                Result = new Result { Message = "Successful" }
            };
        }

        public Result Write(SavedFile file)
        {
            try
            {
                CheckFolder(file.Path.Site);
                File.WriteAllBytes($"{serverWrite.Value.Path}\\{file.Path}", file.Content);                
                return new Result { Message = "Successful" };
            }
            catch (Exception exception)
            {
                return new Result { ErrorCode = 1, Message = exception.Message };
            }
        }

        private void CheckFolder(string site)
        {
            var path = $"{serverWrite.Value.Path}\\{site}";
            if(!Directory.Exists(path)){
                Directory.CreateDirectory(path);                
            }
        }
    }
}