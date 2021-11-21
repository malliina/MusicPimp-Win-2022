using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPimp
{
    public class HealthResponse
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string GitHash { get; set; }
    }

    public class VersionResponse
    {
        public string Version { get; set; }
    }

    public class ReasonResponse
    {
        public string Reason { get; set; }
    }

    public class FolderResponse
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public string Parent { get; set; }
        public string Url { get; set; }
    }

    public class TrackResponse
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Path { get; set; }
        public uint Duration { get; set; }
        public uint Size { get; set; }
        public string Url { get; set; }
    }

    public class LibraryResponse
    {
        public List<FolderResponse> Folders { get; set;  }
        public List<TrackResponse> Tracks { get; set;  }
    }

    public record Credential(string Server, string Username, string Password);
}
