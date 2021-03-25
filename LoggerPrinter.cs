using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    class LoggerPrint : IPrint
    {
        private LoggerPrint() { }

        private static LoggerPrint _instance;

        public static LoggerPrint GetInstance()
        {
            if (_instance == null)
                _instance = new LoggerPrint();
            return _instance;
        }

        public int PageNumber 
        { 
            get => PageNumber;
            set => PageNumber = value;
        }

        private int count = 0;
        private Font PrintFont
        {
            get => new Font("Arial", 8);
            set { PrintFont = value; }
        }
        private string _DocToPrint;
        public string DocToPrint
        { 
            get => _DocToPrint; 
            set => _DocToPrint = value;
        }
        
        private PrintDocument _PrintDocument;


        private Bitmap _Bmp;
        public Bitmap Bmp
        {
            get => _Bmp;
            set => _Bmp = value;
        }

        Font IPrint.PrintFont { set => throw new NotImplementedException(); }

        public PrintDocument DocumentToPrint { get => _PrintDocument; set => _PrintDocument = value; }

        public void BeginDocPrint(object sender, PrintEventArgs e)
        {
            PrintDocument pd = (PrintDocument)sender;

            if (pd.PrinterSettings.PrintRange == PrintRange.SomePages)
            {
                //pd.PrinterSettings.FromPage 
                //pd.PrinterSettings.ToPage
            }

            int col = 120;
            if (pd.DefaultPageSettings.Landscape == true)
                col = 180;

            if (pd.PrinterSettings.PrintRange == PrintRange.Selection)
            {

            }

            string[] docToPrintStr = DocToPrint.Split('\n');
            string tempStr = "";
            DocToPrint = "";

            for (int i = 0; i < docToPrintStr.Length; i++)
                {
                tempStr = docToPrintStr[i];
                if (docToPrintStr[i].Length > col)
                    tempStr = docToPrintStr[i].Substring(0, col) + "\n" + docToPrintStr[i].Substring(col, docToPrintStr[i].Length - col);
                DocToPrint += tempStr + "\n";
                }

            }

        public void PrintDocPage(object sender, PrintPageEventArgs ev)
        {
            DocumentToPrint = new PrintDocument();

            float linesPerPage = 0;
            int linesPrinted = 0;
            float yPos;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;

            string logLocation = App.Prj.getLogName(App.Prj.Key, ProjectData.logID);
            int logIndex = logLocation.LastIndexOf(@"\") + 1;

            string header = " Log:  " + App.Prj.Name + "/" + logLocation.Substring(logIndex, logLocation.Length - logIndex) +
                            "     Printed on " + System.DateTime.Now;
            
            setPrintDefaultSettings();

            yPos = topMargin + (linesPrinted *
               PrintFont.GetHeight(ev.Graphics));
            ev.Graphics.DrawString(header, PrintFont, Brushes.Black, leftMargin, yPos, new StringFormat());
            linesPrinted = 3;

            // Calculate the number of lines per page.
            linesPerPage = ev.MarginBounds.Height /
               PrintFont.GetHeight(ev.Graphics);

            string[] lineToPrint = DocToPrint.Split('\n');
            // Print each line of the file.
            while (linesPrinted < linesPerPage && lineToPrint.Length > count)
            {
                //Graphics g = ev.Graphics;
                //g.DrawRectangle(new Pen(Brushes.Black), new Rectangle(new Point(0, 0), new Size(900, 100)));

                yPos = topMargin + (linesPrinted *
                   PrintFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(lineToPrint[count], PrintFont, Brushes.Black,
                   leftMargin, yPos, new StringFormat());
                count++;
                linesPrinted++;
            }

            // If more lines exist, print another page.
            if (lineToPrint.Length > count)
                ev.HasMorePages = true;
            else
                ev.HasMorePages = false;
        }

        private void setPrintDefaultSettings()
        {
            DocumentToPrint.DefaultPageSettings.Landscape = true;

        }

        public void QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
        {
           
        }

        public void pdPreview_PrintPage(object sender, PrintPageEventArgs e)
        {
            PrinterResolution pkResolution;
            PrintDocument printDocument1 = (PrintDocument)sender;

            pkResolution = printDocument1.PrinterSettings.PrinterResolutions[0];
            e.PageSettings.PrinterResolution = pkResolution;
            e.Graphics.DrawImage(Bmp, 0, 0);
        }

        public void pdPreview_BeginPrint(object sender, PrintEventArgs e)
        {

        }

    }
}
