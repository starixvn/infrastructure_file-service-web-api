namespace FileServiceWebApi.Models
{
    public class PathFile
    {
        public string Site { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Site}\\{Name}";
        }
    }

    public class SavedFile
    {
        public PathFile Path { get; set; }

        public byte[] Content { get; set; }
    }
}