using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPimp
{
    public class LibraryVM: BindableBase
    {
        private string server;

        public LibraryVM()
        {
            server = "Todo";
        }

        public string Server
        {
            get => server;
        }
    }
}
