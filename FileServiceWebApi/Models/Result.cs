namespace FileServiceWebApi.Models
{
    public class Result
    {
        public int ErrorCode { get; set; }

        public string Message { get; set; }
    }

    public class ReadResult
    {
        public byte[] Content { get; set; }

        public Result Result { get; set; }
    }
}