namespace FileServiceWebApi.Models
{
    public class ServerWrite
    {
        public string Path { get; set; }
    }
    public class ServerRead : ServerWrite
    {
        public string UserName { get; set; }
        public string Password { get; set; }        
    }
}