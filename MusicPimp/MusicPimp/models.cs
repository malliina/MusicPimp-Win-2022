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

    public record Credential(string Server, string Username, string Password);
}
