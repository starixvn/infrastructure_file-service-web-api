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
        private static string _password;

        public FileServiceImp(
            IOptions<ServerWrite> serverWrite,
            IOptions<ServerRead> serverRead)
        {
            this.serverWrite = serverWrite;
            this.serverRead = serverRead;
            _password = CryptographyEncoding.CfsDeCrypt(serverRead.Value.Password);
        }

        private static string Password { get { return _password; } }

        public ReadResult Read(PathFile path)
        {
            try
            {
                var networkCredential = new NetworkCredential(serverInfo.Value.UserName, serverInfo.Value.Password);
                using (new NetworkConnection(serverInfo.Value.Path, networkCredential))
                {
                    return ReadFile(path);
                }
            }
            catch (Exception exception)
            {
                return new ReadResult
                {
                    Result = new Result { ErrorCode = 1, Message = exception.Message }
                };
            }
        }

        private ReadResult ReadFile(PathFile path)
        {
            return new ReadResult
            {
                Content = File.ReadAllBytes($"{serverInfo.Value.Path}\\{path}"),
                Result = new Result { Message = "Successful" }
            };
        }

        public Result Write(SavedFile file)
        {
            try
            {
                File.WriteAllBytes($"{serverInfo.Value.Path}\\{file.Path}", file.Content);
                return new Result { Message = "Successful" };
            }
            catch (Exception exception)
            {
                return new Result { ErrorCode = 1, Message = exception.Message };
            }
        }
    }
}