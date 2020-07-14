using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace Logger
{
    public class Utility
    {
        public string dec2hex(string decNumber, int digits)
        {
            int in1 = int.Parse(decNumber);

            string hx1 = in1.ToString("X2");
            return hx1;
        }
        public string dec2bin(string decNumber, int digits)
        {

            int in1 = int.Parse(decNumber);

            string strBinary = Convert.ToString(in1, 2).PadLeft(digits, '0');

            ;
            return strBinary;
        }
        public void CopyDir(string sourceDir, string destinationDir, bool replace, string logPath = @"c:\staging\log\Staging.LOG")
        {
            string src = sourceDir;
            string dst = destinationDir;

            if (destinationDir.Contains("install"))
            {
                destinationDir = @"c:\staging";
            }


            // copy folder structure
            foreach (string dir in Directory.GetDirectories(src, "*", System.IO.SearchOption.AllDirectories))
            {
                try
                {
                    Directory.CreateDirectory(dst + dir.Substring(src.Length));
                    WriteLog(destinationDir + @"\LOG\", logPath, "Creating Directory: " + dst + dir.Substring(src.Length));
                    Console.WriteLine("Creating Directory: " + dst + dir.Substring(src.Length));
                }
                catch (Exception e)
                {
                    WriteLog(destinationDir + @"\LOG\", logPath, "Error Creating Directory: " + dst + dir.Substring(src.Length) + "\n" + e.Message);
                    Console.WriteLine("Creating Directory: " + dst + dir.Substring(src.Length));
                    Environment.Exit(1);
                }
            }

            // copy files to each folder
            foreach (string fileName in Directory.GetFiles(src, "*", System.IO.SearchOption.AllDirectories))
            {
                try
                {
                    File.Copy(fileName, dst + fileName.Substring(src.Length), replace);
                    WriteLog(destinationDir + @"\LOG\", logPath, "Copying File: " + dst + fileName.Substring(src.Length));
                    Console.WriteLine("Copying File: " + dst + fileName.Substring(src.Length));
                }
                catch (Exception e)
                {
                    WriteLog(destinationDir + @"\LOG\", logPath, "Error Copying File: " + dst + fileName.Substring(src.Length) + "\n" + e.Message);
                    Console.WriteLine("Error Copying File: " + dst + fileName.Substring(src.Length) + "\n" + e.Message);
                    Environment.Exit(1);
                }
            }
        }

        public MatchCollection showMatch(string text, string expr)
        {
            Console.WriteLine("The Expression: " + expr);
            MatchCollection mc = Regex.Matches(text, expr);
            return mc;
        }
        public void WriteLog(string filePath, string fileName, string logLine)
        {
            string f = filePath + fileName;

            // does the file exist?
            if (!File.Exists(f))
            {
                // create the file
                using (StreamWriter sw = File.CreateText(f))
                {
                    sw.WriteLine("============ " + DateTime.Now.ToString() + "=============");
                }
            }
            else
            {

                using (StreamWriter sw = File.AppendText(f))
                {
                    string month = String.Format("{0}", DateTime.Now.Month.ToString("D2"));
                    string day = String.Format("{0}", DateTime.Now.Day.ToString("D2"));
                    string year = String.Format("{0}", DateTime.Now.Year.ToString("D2"));
                    string hour = String.Format("{0}", DateTime.Now.Hour.ToString("D2"));
                    string min = String.Format("{0}", DateTime.Now.Minute.ToString("D2"));
                    string sec = String.Format("{0}", DateTime.Now.Second.ToString("D2"));
                    string milisec = String.Format("{0}", DateTime.Now.Millisecond.ToString("D3"));

                    sw.WriteLine(month + day + year + " " + hour + min + sec + milisec + " [ " + logLine);
                }
            }

        }

        public string FindReplace(string source, string first, string second, bool replace)
        {
            string result = "";

            if (replace)
            {
                result = source.Replace(first, second);
            }
            else
            {
                if (source.Contains(first))
                    result = "";
            }

            return result;
        }

        public void ChangeXMLElementValue(string xmlFileName, string xmlElement, string xmlElementValue)
        {
            try
            {
                string fileToLoad = xmlFileName;
                XmlDocument doc = new XmlDocument();
                doc.Load(fileToLoad);
                XmlNode node = doc.SelectSingleNode(xmlElement);
                if (node != null)
                {
                    node.InnerText = xmlElementValue;
                }
                doc.Save(fileToLoad);
                doc = null;

            }
            catch (Exception e)
            {
                WriteLog(@"c:" + @"\LOG\", "ATMSetup.LOG", "Exception setting " + xmlElement + " in ImageWay configuration files " + e.Message);
                Environment.Exit(1);
            }
        }

        public string ReadFile(string fileName, string path)
        {
            string fileToOpen = path + fileName;
            string lines = System.IO.File.ReadAllText(fileToOpen);
            return lines;
        }
        public XmlNodeList LoadXMLValue(string xmlFileName, string xmlElement)
        {
            string fileToLoad = xmlFileName;
            XmlDocument doc = new XmlDocument();
            XmlNodeList nodes = null;
            try
            {
                doc.Load(fileToLoad);
                nodes = doc.SelectNodes(xmlElement);
                if (nodes.Count > 0)
                    return nodes;
                else
                    nodes = null;

            }
            catch (Exception e)
            {
                WriteLog(@"c:" + @"\LOG\", "ATMSetup.LOG", "Exception setting " + xmlElement + " setting up configuration files " + e.Message);
                Environment.Exit(1);
            }

            return nodes;

        }

        public void updateData()
        {

            string connectionString;
            SqlConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();

                SqlCommand command;
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                String sql = "";

                sql = @"UPDATE Table1 SET logkey = 'TEST4' WHERE logline LIKE 'TEST3'";

                command = new SqlCommand(sql, cnn);
                dataAdapter.InsertCommand = new SqlCommand(sql, cnn);
                dataAdapter.InsertCommand.ExecuteNonQuery();

                command.Dispose();
                cnn.Close();

            }
            catch (Exception dbEx)
            {
                Console.WriteLine(dbEx.ToString());
            }

        }
        public void deleteData()
        {

            string connectionString;
            SqlConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();

                SqlCommand command;
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                String sql = "";

                sql = @"DELETE from Table1 WHERE id = 1";

                command = new SqlCommand(sql, cnn);
                dataAdapter.InsertCommand = new SqlCommand(sql, cnn);
                dataAdapter.InsertCommand.ExecuteNonQuery();

                command.Dispose();
                cnn.Close();

            }
            catch (Exception dbEx)
            {
                Console.WriteLine(dbEx.ToString());

            }

        }
    }
}

