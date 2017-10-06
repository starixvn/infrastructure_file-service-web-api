using System.Runtime.InteropServices;

namespace FileServiceWebApi.Network
{
    [StructLayout(LayoutKind.Sequential)]
    public class NetResource
    {
        public ResourceScope Scope { get; set; }
        public ResourceType ResourceType { get; set; }
        public ResourceDisplaytype DisplayType { get; set; }
        public int Usage { get; set; }
        public string LocalName { get; set; }
        public string RemoteName { get; set; }
        public string Comment { get; set; }
        public string Provider { get; set; }
    }
}