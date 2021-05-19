using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;


namespace Logger
{
    class LoggerPrint : Form, IPrint
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
        private string header;
        private string[] lineToPrint;
        private int currentPage = 0;
        private Font PrintFont
        {
            get => new Font("Courier New", 8);
            set { PrintFont = value; }
        }
        private string _DocToPrint;
        public string DocToPrint
        {
            get => _DocToPrint;
            set => _DocToPrint = value;
        }
        private string _SelToPrint;
        public string SelToPrint
        {
            get => _SelToPrint;
            set => _SelToPrint = value;
        }

        private PrintDocument _PrintDocument;

        private int _CharactersPerLine;


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

            float docWidth = pd.DefaultPageSettings.Bounds.Width;

            Graphics g = CreateGraphics();
            g.PageUnit = GraphicsUnit.Point;
            float docWidthChar = g.MeasureString("@", this.PrintFont).Width;

            int col = (int)(docWidth / docWidthChar);

            //int charPerLine = ev.MarginBounds.Width / (int)ev.Graphics.MeasureString("@", this.PrintFont).Width;


            currentPage = 0;

            //int col = 75;
            //if (pd.DefaultPageSettings.Landscape == true)
            //    col = 100;

            string[] docToPrintStr;
            if (pd.PrinterSettings.PrintRange == PrintRange.Selection)
            {
                SelToPrint = SelToPrint.Replace("\r\n", "\n");
                docToPrintStr = SelToPrint.Split('\n');
            }
            else
                docToPrintStr = DocToPrint.Split('\n');

            string tempStr = "";
            DocToPrint = "";

            for (int i = 0; i < docToPrintStr.Length; i++)
            {
                tempStr = "";
                int docLen = docToPrintStr[i].Length;
                int times = docLen / col;
                int count = 0;

                while (count < times)
                {
                    tempStr += docToPrintStr[i].Substring(count * col, col) + "\n";
                    count++;
                }
                if (docLen != count * col)
                    tempStr += docToPrintStr[i].Substring(count * col, docLen - (count * col)) + "\n";

                DocToPrint += tempStr;

            }
            string logLocation = App.Prj.getLogName(App.Prj.Key, ProjectData.logID);
            int logIndex = logLocation.LastIndexOf(@"\") + 1;

            header = " Log:  " + App.Prj.Name + "/" + logLocation.Substring(logIndex, logLocation.Length - logIndex) +
                            "     Printed on " + System.DateTime.Now;

            lineToPrint = DocToPrint.Split('\n');

        }

        public void PrintDocPage(object sender, PrintPageEventArgs ev)
        {
            // DocumentToPrint = new PrintDocument();

            float linesPerPage = 0;
            int linesPrinted = 0;
            float yPos;
            float xPos = 100;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;

            bool printThisPage = true;
            currentPage++;
            // calculate characters per line for a particular font
            //int charPerLine = ev.MarginBounds.Width / (int)ev.Graphics.MeasureString("@", this.PrintFont).Width;
            PrintDocument pd = (PrintDocument)sender;

            if (pd.PrinterSettings.PrintRange == PrintRange.SomePages)
            {
                if ((currentPage >= pd.PrinterSettings.FromPage &&
                currentPage <= pd.PrinterSettings.ToPage) == false)
                    printThisPage = false;
            }

            // Print header
            yPos = topMargin + PrintFont.GetHeight(ev.Graphics);

            if (pd.DefaultPageSettings.Landscape == true)
                xPos = 130;

            string pageStr = "Page  " + currentPage.ToString();
            xPos = (xPos - pageStr.Length) * PrintFont.Size;

            //xPos = leftMargin + header.Length + PrintFont.Size;
            linesPrinted = 3;

            if (printThisPage)
            {
                ev.Graphics.DrawString(header, PrintFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                ev.Graphics.DrawString(pageStr, PrintFont, Brushes.Black, xPos, yPos, new StringFormat());
            }

            // Calculate the number of lines per page.
            linesPerPage = (ev.MarginBounds.Height /
               PrintFont.GetHeight(ev.Graphics));

            // Print each line of the file.
            while (linesPrinted < linesPerPage && lineToPrint.Length > count)
            {
                //Graphics g = ev.Graphics;
                //g.DrawRectangle(new Pen(Brushes.Black), new Rectangle(new Point(0, 0), new Size(900, 100)));

                yPos = topMargin + (linesPrinted *
                   PrintFont.GetHeight(ev.Graphics));
                if (printThisPage)
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

        //private void setPrintDefaultSettings()
        //{
        //    DocumentToPrint.DefaultPageSettings.Landscape = true;

        //}

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

        internal void EnPrint(object sender, PrintEventArgs e)
        {
            _instance = null;
        }
    }
}
