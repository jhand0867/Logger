using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;

namespace Logger
{
    public interface IPrint
    {
        int PageNumber { get; set; }
        Font PrintFont { set; }
        string DocToPrint { get; set; }

        void PrintDocPage(object sender, PrintPageEventArgs e);
        void BeginDocPrint(object sender, PrintEventArgs e);
    }
}
