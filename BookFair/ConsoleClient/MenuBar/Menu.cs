using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleClient.MenuBar
{
    public interface Menu
    {
        string Title { get; }
        void RunMenu();
    }
}
